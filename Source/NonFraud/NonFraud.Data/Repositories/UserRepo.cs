using NonFraud.Data.Contexts;
using NonFraud.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonFraud.Data.Repositories
{
    /// <summary>
    /// Repo for User entity
    /// </summary>
    public class UserRepo
    {
        BaseContext<NonFraudContext> _db;

        public UserRepo()
        {
            _db = new BaseContext<NonFraudContext>();
        }

        /// <summary>
        /// Get an user by Id
        /// </summary>
        /// <param name="id">user Id</param>
        public User GetUserById(int id)
        {
            return _db.GetContext(db => (
                from table in db.User.Include("Profile")
                where table.UserID.Equals(id)
                select table).FirstOrDefault());
        }

        /// <summary>
        /// Get an user by password and username
        /// </summary>
        /// <param name="userName">username</para>
        /// <param name="password">user password</param>
        /// <returns></returns>
        public User SignUser(string userName, string password)
        {
            return _db.GetContext(db => (
                from table in db.User.Include("Profile")
                where table.UserName.Equals(userName)
                where table.Password.Equals(password)
                select table).FirstOrDefault());
        }
    }
}
