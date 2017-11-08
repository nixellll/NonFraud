using System.Web;
using System.Web.Mvc;

namespace NonFraud.Web
{
    /// <summary>
    /// Filter config class
    /// </summary>
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
