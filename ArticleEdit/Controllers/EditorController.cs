using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArticleEdit.Controllers
{
    [Authorize]
    public class EditorController : Controller
    {
        // GET: Editor
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult EditText()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }


    }
}