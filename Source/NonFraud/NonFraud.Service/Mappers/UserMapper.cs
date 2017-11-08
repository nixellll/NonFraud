using NonFraud.Data.Entities;
using NonFraud.Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NonFraud.Service.Mappers
{
    /// <summary>
    /// Mapper class for User entity and model
    /// </summary>
    public class UserMapper
    {
        /// <summary>
        /// Maps BD data to user model  
        /// </summary>
        /// <param name="user">User from DB</param>        
        public UserModel Map(User user)
        {
            if (user == null)
                return new UserModel();

            UserModel userModel = new UserModel
            {
                UserId = user.UserID,
                UserName = user.UserName,
                Password = user.Password,
                ProfileID = user.ProfileID
            };

            if (user.Profile != null)
                userModel.Profile = user.Profile.ProfileName;

            return userModel;
        }

        /// <summary>
        /// Maps model to DB entity
        /// </summary>
        /// <param name="user">user model</param>
        public User Map(UserModel user)
        {
            return new User
            {
                UserName = user.UserName,
                Password = user.Password,
                ProfileID = user.ProfileID,
                CreationDate = DateTime.Now
            };
        }
    }
}