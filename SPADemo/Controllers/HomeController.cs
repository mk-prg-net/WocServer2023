using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPADemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var tools = new Tools.UrlTools();
            return Redirect($"{tools.ParseOrigin(Request.Url)}/start");
        }
    }
}
