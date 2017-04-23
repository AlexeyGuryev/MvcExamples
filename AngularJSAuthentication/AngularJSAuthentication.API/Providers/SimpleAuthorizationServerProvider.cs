using AngularJSAuthentication.API.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AngularJSAuthentication.API.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.Validated();
                return Task.FromResult<object>(null);
            }

            using (var repo = new AuthRepository())
            {
                client = repo.FindClient(context.ClientId);
            }
            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client {0} is not registered", context.ClientId));
                return Task.FromResult<object>(null);
            }
            if (client.ApplicationType == Models.ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret should be sent");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());
            context.OwinContext.Set<string>("as:client_id", clientId);

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin") ?? "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            
            using (var authRepository = new AuthRepository())
            {
                IdentityUser user = await authRepository.FindUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            //identity.AddClaim(new Claim("sub", context.UserName));
            //identity.AddClaim(new Claim("role", "user"));

            var properties = new AuthenticationProperties(new Dictionary<string, string>()
            {
                { "as:client_id", (context.ClientId ?? string.Empty) }
                , // добавляем userName - чтоб он приходил клиенту в ответе на refresh
                { "userName", context.UserName }
            });

            var ticket = new AuthenticationTicket(identity, properties);
            context.Validated(ticket);
        }

        /// <summary>
        /// проброс доп.данных клиенту в ответе на refresh (в т.ч. userName)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns> 
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var pair in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(pair.Key, pair.Value);
            }
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Сюда приходим, когда идет запрос на refresh
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.OwinContext.Get<string>("as:client_id");
            //var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.Rejected();
                return;
                //context.SetError("invalid_clientid", "Refresh token to a different clientId");
                //return Task.FromResult<object>(null);
            }

            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            // в этом месте можно поменять claim'ы
            newIdentity.AddClaim(new Claim("newClaim", "refreshToken"));

            var ticket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(ticket);

            //return Task.FromResult<object>(null);
        }
    }
}