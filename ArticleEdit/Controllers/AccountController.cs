using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;

namespace ArticleEdit.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult LogOn()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return View("LogOn");
        }

        public ActionResult TryAuthenticate(string userName)
        {
            if(userName == "Anton")
            {
                // Hier muss noch das Authentication- Cookie gesetzt werden. Sonst "prallt" die Redirektion
                // am [Authorize] Attribut der geschützen Action wieder ab.
                // Siehe https://stackoverflow.com/questions/54476974/can-we-use-cookie-authentication-provided-by-mvc-5-without-using-asp-net-identit

                // 30.1.2023
                // ToDo: sichere Passwort- Authentifizierung mit Security- Token
                // Idee: 
                //                               zugesandten 🔑 öffentlichen Schlüssel eintragen  
                //             🖵 Browser        ↙                                        |
                //                 +- 🗄 personal id-value store                          |
                //                 |           ↗                                          |
                //                 |      Extern zugesandtes 🔑 kryptoId eintragen--+    |
                //                 |                                                |     |
                //                 +- 🖵 Login Fenster                              |     |           
                //                         +- 🔑 Password ------------------+      |     |
                //                                                           ↓      ↓     ↓
                //                         🎲 Random numbers -----------→ ⎔⎔⎔ Encrypt ⎔⎔⎔
                //                                                                 ↓
                //                                          🔒SecurityToken ←------+                                                       
                //                                           |
                //                                           ↓
                //                                          🖥 Web Server ← 🔑 geheimen Schlüssel
                //                                           ↓
                //                                           ⎔ Decrypt 
                //                                           +→ 🔑 kryptoId
                //                                           |
                //                                           +→ 🔑 Password
                //
                //   Crypto- Lib: https://github.com/bitwiseshiftleft/sjcl/
                //   - https://www.section.io/engineering-education/implementing-public-key-cryptography-in-javascript/
                //   - http://nacl.cr.yp.to/
                //   - https://medium.com/@tikiatua/symmetric-and-asymmetric-encryption-with-javascript-and-go-240043e56daf
                //   - https://github.com/digitalbazaar/forge
                // 
                //   

                var user = new Models.AppUser(1, "Anton");
                var expire = DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes);
                var ticket = new FormsAuthenticationTicket("Anton", false, expire.Minute);
                var hashTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
                //cookie.Expires = expire;
                HttpContext.Response.Cookies.Add(cookie);


                return RedirectToAction("Index", "Editor");
            }
            return View("LogOn");
        }
    }
}