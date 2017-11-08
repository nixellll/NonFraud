using NonFraud.Service.Models;
using NonFraud.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace NonFraud.Web.Services.Login
{
    /// <summary>
    /// Class for authentication module provided my Microsoft
    /// </summary>
    public class FormsAuthenticationBase
    {
        const char _separator = ',';
        BaseService<UserModel> _baseService;

        public FormsAuthenticationBase()
        {
            _baseService = new BaseService<UserModel>();
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public bool Authenticate(string userName, string password, bool rememberMe)
        {
            MvcApplication.profiles = new string[] { };
            var result = GetUser(userName, password);

            if (result.Profile == null)
                return false;

            MvcApplication.profiles = new string[] { result.Profile };
            SignIn(userName, rememberMe);
            return true;
        }

        private void SignIn(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SetRolsToUsersPrincipal(string user)
        {
            ContextValues.User = new GenericPrincipal(new GenericIdentity(user), MvcApplication.profiles);
        }

        private UserModel GetUser(string userName, string password)
        {
            string path = String.Format(StaticValues.UserSignServiceUrl, userName, password);
            string user = _baseService.Get(path);
            var serializedUser = new JavaScriptSerializer().Deserialize<UserModel>(user);
            
            return serializedUser;
        }
    }
}