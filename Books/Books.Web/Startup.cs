using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Books.Web.Startup))]
namespace Books.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // This method dot need a comment
            ConfigureAuth(app);
        }
    }
}
