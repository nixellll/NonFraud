using NonFraud.Data.Entities;
using NonFraud.Data.Repositories;
using NonFraud.Service.Helpers;
using NonFraud.Service.Mappers;
using NonFraud.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NonFraud.Service.Controllers
{
    /// <summary>
    /// User controller of application
    /// </summary>
    public class UserController : ApiController
    {
        BaseRepo<User> _baseUserRepo;
        UserRepo _userRepo;
        UserMapper _userMapper;
        EncryptionHelper _encrypHelper;

        public UserController()
        {
            _baseUserRepo = new BaseRepo<User>();
            _userRepo = new UserRepo();
            _userMapper = new UserMapper();
            _encrypHelper = new EncryptionHelper();
        }

        /// <summary>
        /// Get an user by id
        /// </summary>
        /// <param name="id">User id</param>        
        [HttpGet]
        public UserModel Get(int id)
        {
            return _userMapper.Map(_userRepo.GetUserById(id));
        }

        /// <summary>
        /// Returns an user by password and username
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>        
        [HttpGet]
        [Route("api/User/{userName}/{password}/SignUser")]
        public UserModel SignUser(string userName, string password)
        {
            return _userMapper.Map(_userRepo.SignUser(userName, password));
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post(NonFraud.Service.Models.UserModel user)
        {
            var response = new HttpResponseMessage();
            
            if (user.Token == null || !_encrypHelper.ValidateToken(user.Token, user.UserName))
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.ReasonPhrase = "Invalid Request. Please try again";
                return response;
            }

            _baseUserRepo.Insert(_userMapper.Map(user));
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }
    }
}
