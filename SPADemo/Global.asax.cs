using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SPADemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        public const string ncDictKey = "0x8F7A44B80A87FA";


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var ntools = new MKPRG.Naming.Tools();

            var getNcDict = ntools.GetNamingContainers("MKPRG.Naming");
            if (getNcDict.succeded)
            {
                try
                {
                    Application.Lock();
                    Application[ncDictKey] = getNcDict.ncDict;
                }
                finally
                {
                    Application.UnLock();
                }
            }
            else
            {
                if (getNcDict.duplicates.Any())
                {
                    var duplicateList = string.Join("\n", getNcDict
                                                             .duplicates
                                                             .Select(r => $"{r.CNT}, nid={r.ID};")
                                                             .ToArray());

                    throw new Exception($"Get Naming Container in Global.asax failed!\nDuplicate NamingContainers detected:\n{duplicateList}");
                }
                else
                {
                    throw new Exception($"Get Naming Container in Global.asax failed!\nOther Error");
                }
                
            }
        }
    }
}
