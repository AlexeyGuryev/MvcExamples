using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EmptyAspnetApp
{
    using System.Text;
using AppFunc = Func<IDictionary<string, object>, System.Threading.Tasks.Task>;
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApiRoute", "{controller}");
            app.UseWebApi(config);

            app.UseHandler(SimpleModule.ShowMainPage);

            app.Use((env, context) =>
            {
                var headers = env.Response.Headers;
                if (!headers.ContainsKey("Content-Type"))
                    headers.Add("Content-Type", new string[] { "text/plain, charset=utf-8" });
                else
                    headers["Content-Type"] = "text/plain, charset=utf-8";

                var outStream = env.Response.Body;
                var buffer = Encoding.UTF8.GetBytes("Все нормуль!");
                return outStream.WriteAsync(buffer, 0, buffer.Length);
            });            
        }
    }
}