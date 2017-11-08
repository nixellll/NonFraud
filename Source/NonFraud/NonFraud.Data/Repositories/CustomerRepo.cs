
using NonFraud.Data.Contexts;
using NonFraud.Data.Entities;
using NonFraud.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonFraud.Data.Repositories
{
    /// <summary>
    /// Repo for Customer entity
    /// </summary>
    public class CustomerRepo
    {
        BaseContext<NonFraudContext> _db;

        public CustomerRepo()
        {
            _db = new BaseContext<NonFraudContext>();
        }

        /// <summary>
        /// Get a customer by name
        /// </summary>
        /// <param name="name">Customer name</param>
        public Customer GetCustByName(string name)
        {
            return _db.GetContext(db => (
                 from table in db.Customer
                 where table.CustomerName == name
                 select table).FirstOrDefault());
        }       
    }
}
