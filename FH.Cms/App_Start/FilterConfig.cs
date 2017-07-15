using JFB.Cms.Component;
using System.Web;
using System.Web.Mvc;

namespace JFB.Cms
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthLoginAttribute());
        }
    }
}