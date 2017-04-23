using AngularJSAuthentication.API.Entities;
using AngularJSAuthentication.API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AngularJSAuthentication.API
{
    public class AuthRepository : IDisposable
    {
        private AuthContext context;

        private UserManager<IdentityUser> userManager;

        public AuthRepository()
        {
            context = new AuthContext();
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser identityUser = new IdentityUser()
            {
                UserName = userModel.UserName
            };

            var result = await userManager.CreateAsync(identityUser, userModel.Password);
            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser identityUser = await userManager.FindAsync(userName, password);
            return identityUser;
        }

        public Client FindClient(string clientId)
        {
            return context.Clients.Find(clientId);
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var existingToken = context.RefreshTokens.Where(c => c.Subject == token.Subject && c.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            context.RefreshTokens.Add(token);
            return await context.SaveChangesAsync() > 0;

        }

        public async Task<bool> RemoveRefreshToken(RefreshToken existingToken)
        {
            context.RefreshTokens.Remove(existingToken);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string tokenId)
        {
            var refreshToken = await FindRefreshToken(tokenId);
            if (refreshToken != null)
            {
                context.RefreshTokens.Remove(refreshToken);
            }
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await context.RefreshTokens.FindAsync(refreshTokenId);
            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return context.RefreshTokens.ToList();
        }

        public void Dispose()
        {
            context.Dispose();
            userManager.Dispose();
        }
    }
}