using NonFraud.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonFraud.Data.Contexts
{
    /// <summary>
    /// Base Context for Database operations
    /// </summary>
    /// <typeparam name="T">Specific context for operations</typeparam>
    public class BaseContext<T> where T : IDisposable, new()
    {
        /// <summary>
        /// Method for Update,Delete and Create operations
        /// </summary>
        /// <param name="execute">Action to execute</param>
        public void GetContext(Action<T> execute)
        {
            try
            {
                using (var db = new T())
                {
                    execute(db);
                }
            }
            catch (DbEntityValidationException e)
            {
                var err = new FormattedDbEntityValidationException(e);
                throw err;
            }
        }

        /// <summary>
        /// Method for getting data from DB
        /// </summary>
        /// <param name="execute">Action to execute</param>
        public Y GetContext<Y>(Func<T, Y> execute)
        {
            try
            {
                using (var db = new T())
                {
                    return execute(db);
                }
            }
            catch (DbEntityValidationException e)
            {
                throw new FormattedDbEntityValidationException(e);
            }
        }
    }
}
