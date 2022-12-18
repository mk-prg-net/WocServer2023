using System.Web;
using System.Web.Mvc;

namespace MKPRG.Naming.SVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
