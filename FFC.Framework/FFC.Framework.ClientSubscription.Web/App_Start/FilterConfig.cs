using System.Web;
using System.Web.Mvc;

namespace FFC.Framework.ClientSubscription.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}