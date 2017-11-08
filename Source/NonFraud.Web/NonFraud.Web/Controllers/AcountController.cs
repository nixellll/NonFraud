using Microsoft.AspNet.Identity.Owin;
using NonFraud.Service.Models;
using NonFraud.Web.Helpers;
using NonFraud.Web.Mappers;
using NonFraud.Web.Models;
using NonFraud.Web.Services;
using NonFraud.Web.Services.Login;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NonFraud.Web.Controllers
{
    /// <summary>
    /// Account controller of the application
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private FormsAuthenticationBase _auth;
        BaseService<Service.Models.UserModel> _baseService;
        UserMapper _userMapper;
        EncryptionHelper _encrypHelper;

        public AccountController()
        {
            _auth = new FormsAuthenticationBase();
            _baseService = new BaseService<Service.Models.UserModel>();
            _userMapper = new UserMapper();
            _encrypHelper = new EncryptionHelper();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel user, string returnUrl)
        {
            if (ModelState.IsValid && _auth.Authenticate(user.UserName, user.Password, user.RememberMe))
            {
                Session["userId"] = GetUserId(user.UserName, user.Password);
                MvcApplication.currentUser = user.UserName;

                if (user.UserName == null)
                    MvcApplication.currentUser = "New User";

                if (!String.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else if (ModelState.IsValid)
                ModelState.AddModelError(string.Empty, StaticValues.LoginErrorMsg);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            MvcApplication.currentUser = null;
            MvcApplication.profiles = new string[] { };
            Session["userId"] = null;

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            model.Profiles = GetProfiles();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                ResponseModel responseModel = new ResponseModel();
                var userModel = _userMapper.Map(user);
                userModel.Token = _encrypHelper.GenerateToken(userModel.UserName);
                responseModel = _baseService.Create(userModel, StaticValues.UserServiceUrl);
                Session["userId"] = GetUserId(user.UserName, user.Password);
                MvcApplication.currentUser = user.UserName;
                MvcApplication.profiles = new string[] { GetUserById(Convert.ToInt32(Session["userId"])).Profile };

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        public ActionResult Details(int id)
        {
            return View(GetUserById(id));
        }

        /// <summary>
        /// Get profiles list
        /// </summary>        
        private List<SelectListItem> GetProfiles()
        {
            string path = String.Format(StaticValues.ProfileServiceUrl);
            string profiles = _baseService.Get(path);
            var serializedProfiles = new JavaScriptSerializer().Deserialize<List<SelectListItem>>(profiles);

            return serializedProfiles;
        }

        /// <summary>
        /// Get userId by username and password
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        private int GetUserId(string userName, string password)
        {
            string path = String.Format(StaticValues.UserSignServiceUrl, userName, password);
            string user = _baseService.Get(path);
            var serializedUser = new JavaScriptSerializer().Deserialize<UserModel>(user);

            return serializedUser.UserId;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User Id</param>
        private UserModel GetUserById(int id)
        {
            string path = String.Format(StaticValues.UserByIdServiceUrl, id);
            string user = _baseService.Get(path);
            var serializedUser = new JavaScriptSerializer().Deserialize<UserModel>(user);

            return serializedUser;
        }
    }
}