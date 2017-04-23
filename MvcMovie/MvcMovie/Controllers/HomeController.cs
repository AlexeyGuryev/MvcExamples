using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dd = new List<string> {
                "nothing",
                "optionOne",
                "optionTwo",
                "optionThree"
            };

            ViewBag.testDD = new SelectList(dd);
            return View();
        }

        [HttpPost]
        public string Index(FormCollection fc)
        {
            var copy = fc;
            return " not found";
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}