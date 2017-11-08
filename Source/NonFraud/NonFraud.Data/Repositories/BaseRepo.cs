using NonFraud.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonFraud.Data.Repositories
{
    /// <summary>
    /// Dynamic CRUD operations based on T object
    /// </summary>
    /// <typeparam name="T">Specific entity for operations in DB</typeparam>
    public class BaseRepo<T> where T : class
    {
        BaseContext<NonFraudContext> _db;

        public BaseRepo()
        {
            _db = new BaseContext<NonFraudContext>();
        }

        /// <summary>
        /// Dynamic Insert operation
        /// </summary>
        /// <param name="entity">DB entity for operation</param>
        public T Insert(T entity)
        {
            _db.GetContext(db =>
            {
                db.Set<T>().Add(entity);
                db.SaveChanges();
            });

            return entity;
        }

        /// <summary>
        /// Dynamic Update operation
        /// </summary>
        /// <param name="entity">DB entity for operation</param>
        public void Update(T entity)
        {
            _db.GetContext(db =>
            {
                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            });
        }

        /// <summary>
        /// Dynamic Delete operation
        /// </summary>
        /// <param name="entity">DB entity for operation</param>
        public void Delete(T entity)
        {
            _db.GetContext(db =>
            {
                db.Set<T>().Attach(entity);
                db.Set<T>().Remove(entity);
                db.SaveChanges();
            });
        }

        /// <summary>
        /// Return all data from a table according to the expression provided
        /// </summary>
        /// <param name="includeTable">Table(s) for include in returning data</param>
        public virtual IEnumerable<T> GetAll(string[] includeTable = null)
        {
            return GetData(itm => itm.ToList(), includeTable);
        }

        /// <summary>
        /// Execute the lambda expression
        /// </summary>
        /// <param name="get">Lambda expression</param>
        /// <param name="includeTable">Table(s) for include in returning data</param>
        private Y GetData<Y>(Func<IQueryable<T>, Y> get, string[] includeTable = null)
        {
            return _db.GetContext(db =>
            {
                IQueryable<T> set = db.Set<T>();
                if (includeTable != null)
                {
                    foreach (string item in includeTable)
                    {
                        set = set.Include(item);
                    }
                }

                return get(set);
            });
        }
    }
}
