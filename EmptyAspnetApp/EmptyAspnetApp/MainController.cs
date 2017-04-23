using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EmptyAspnetApp
{
    public class MainController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new List<string> { "alex", "blex", "julia" };
        }
    }
}