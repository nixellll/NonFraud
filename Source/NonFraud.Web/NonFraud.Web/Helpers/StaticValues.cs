using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NonFraud.Web.Helpers
{
    /// <summary>
    /// Static values for use around the application and no 'hardcode' in the classes.
    /// </summary>
    public class StaticValues
    {
        public static string TrxServiceUrl
        {
            get { return "Transaction"; }
        }

        public static string TrxTypeServiceUrl
        {
            get { return "TransactionType"; }
        }

        public static string ProfileServiceUrl
        {
            get { return "Profile"; }
        }

        public static string UserServiceUrl
        {
            get { return "User"; }
        }

        public static string UserSignServiceUrl
        {
            get { return UserServiceUrl + "/{0}/{1}/SignUser"; }
        }

        public static string UserByIdServiceUrl
        {
            get { return UserServiceUrl + "/{0}"; }
        }

        public static string LoginErrorMsg
        {
            get { return "El nombre de usuario o contraseña no son correctas."; }
        }
    }
}