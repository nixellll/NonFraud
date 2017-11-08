using NonFraud.Web.Services.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NonFraud.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        FormsAuthenticationBase _auth;
        public static string currentUser;
        public static string[] profiles;
        public static string currentPet;

        public MvcApplication()
        {
            _auth = new FormsAuthenticationBase();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(currentUser))
                return;

            _auth.SetRolsToUsersPrincipal(currentUser);
        }
    }
}
