using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArticleEdit.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult LogOn()
        {
            return View();
        }

        public ActionResult TryAuthenticate(string userName)
        {
            if(userName == "Anton")
            {
                // Hier muss noch das Authentication- Cookie gesetzt werden. Sonst "prallt" die Redirektion
                // am [Authorize] Attribut der geschützen Action wieder ab.
                // Siehe https://stackoverflow.com/questions/54476974/can-we-use-cookie-authentication-provided-by-mvc-5-without-using-asp-net-identit

                return RedirectToAction("Index", "Editor");
            }
            return View("LogOn");
        }
    }
}