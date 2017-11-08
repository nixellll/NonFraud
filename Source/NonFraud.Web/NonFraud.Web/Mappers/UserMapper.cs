using NonFraud.Service.Models;
using NonFraud.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NonFraud.Web.Mappers
{
    /// <summary>
    /// Mapper class for User model
    /// </summary>
    public class UserMapper
    {
        /// <summary>
        /// Maps view model to user model
        /// </summary>
        /// <param name="register">register model</param>
        public UserModel Map(RegisterViewModel register)
        {
            return new UserModel
            {
                UserName = register.UserName,
                Password = register.Password,
                ProfileID = register.ProfileId
            };
        }
    }
}