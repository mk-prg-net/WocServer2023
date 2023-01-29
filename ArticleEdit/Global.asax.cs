using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.Threading;
using System.Web.Security;
using System.Security.Principal;

namespace ArticleEdit
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public override void Init()
        {
            base.Init();

            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
        }

        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if(authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket == null && authTicket.Expired)
                {
                    return;
                }
                else 
                {
                    // Identity laden
                    var appUser = new Models.AppUser(1, authTicket.Name);
                    var myPrincipal = new Models.AccessMgmt.MyPrincipal(appUser);

                    Context.User = myPrincipal;
                    Thread.CurrentPrincipal = myPrincipal;
                }
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
