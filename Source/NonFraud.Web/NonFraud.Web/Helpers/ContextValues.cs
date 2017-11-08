using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace NonFraud.Web.Helpers
{
    /// <summary>
    ///Context values to use around the application 
    /// </summary>
    public static class ContextValues
    {
        public static HttpRequest Request
        {
            get { return HttpContext.Current.Request; }
        }

        public static HttpResponse Response
        {
            get { return HttpContext.Current.Response; }
        }

        public static IPrincipal User
        {
            get { return HttpContext.Current.User; }
            set { HttpContext.Current.User = value; }
        }

        public static string UserName
        {
            get { return User != null ? User.Identity.Name : string.Empty; }
        }

        public static string ApiUrl
        {
            get { return ConfigurationManager.AppSettings["apiUrl"].ToString(); }
        }        
    }
}