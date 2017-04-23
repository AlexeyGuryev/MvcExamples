using Owin.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EmptyAspnetApp
{
    public class SimpleModule
    {
        public static Task ShowMainPage(OwinRequest request, OwinResponse response, Func<Task> next)
        {
            try
            {
                if (request.Path.Equals("/", StringComparison.OrdinalIgnoreCase))
                {
                    return response.WriteAsync("Home page");
                }
            }
            catch(Exception ex)
            {
                var tcs = new TaskCompletionSource<object>();
                tcs.SetException(ex);
                return tcs.Task;
            }

            return next();
        }
    }
}