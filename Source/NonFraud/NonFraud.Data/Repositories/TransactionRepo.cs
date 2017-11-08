using NonFraud.Data.Contexts;
using NonFraud.Data.Entities;
using NonFraud.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonFraud.Data.Repositories
{
    /// <summary>
    /// Repo for Transaction entity
    /// </summary>
    public class TransactionRepo
    {
        BaseContext<NonFraudContext> _db;

        public TransactionRepo()
        {
            _db = new BaseContext<NonFraudContext>();
        }

        /// <summary>
        /// Get all transactions from a DB stored procedure
        /// </summary>
        public List<TransactionModel> GetTransactions()
        {
            return _db.GetContext(db => db.Database.SqlQuery<TransactionModel>("GetTransactions").ToList());
        }

        /// <summary>
        /// Update isFraud field of a transaction
        /// </summary>
        /// <param name="transactionId">Id of the transaction</param>
        /// <param name="isFraud">Is fraud transaction</param>
        public void UpdateIsFraudTrx(int transactionId, bool isFraud)
        {
            var trx = _db.GetContext(db => (
                 from table in db.Transaction
                 where table.TransactionID == transactionId
                 select table).FirstOrDefault());

            trx.IsFraud = isFraud;

            _db.GetContext(db =>
            {
                db.Set<Transaction>().Attach(trx);
                db.Entry(trx).State = EntityState.Modified;
                db.SaveChanges();
            });
        }
    }
}
