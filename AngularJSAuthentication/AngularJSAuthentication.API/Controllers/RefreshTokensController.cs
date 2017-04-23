using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AngularJSAuthentication.API.Controllers
{
    public class RefreshTokensController : ApiController
    {
        private AuthRepository repo = null;

        public RefreshTokensController()
        {
            repo = new AuthRepository();
        }

        [Authorize(Users="Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(repo.GetAllRefreshTokens());
        }

        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await repo.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token id not found");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
