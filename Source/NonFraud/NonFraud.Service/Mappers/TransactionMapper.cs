using NonFraud.Data.Entities;
using NonFraud.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NonFraud.Service.Mappers
{
    /// <summary>
    /// Mapper class for Transaction entity and model
    /// </summary>
    public class TransactionMapper
    {
        /// <summary>
        /// Maps a new customer
        /// </summary>
        /// <param name="name">Customer name</param>
        public Customer Map(string name)
        {
            return new Customer { CustomerName = name, CreationDate = DateTime.Now };
        }

        /// <summary>
        ///  Maps a new Customer Balance
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldBalance">Balance before transaction</param>
        /// <param name="newBalance">Balance after transaction</param>
        public CustomerBalance Map(int id, decimal oldBalance, decimal newBalance)
        {
            return new CustomerBalance
            {
                CustomerID = id,
                OldBalance = oldBalance,
                NewBalance = newBalance
            };
        }

        /// <summary>
        /// Maps transaction model to DB entity
        /// </summary>
        /// <param name="transaction">Transaction model</param>
        /// <param name="custOrigId">Id of origin customer</param>
        /// <param name="custDestId">Id of recipient customer</param>       
        public Transaction Map(TransactionModel transaction, int custOrigId, int custDestId)
        {
            return new Transaction
            {
                TransactionID = transaction.TransactionId,
                TransactionTypeID = transaction.TypeId,
                IsFraud = transaction.IsFraud,
                IsFlagged = transaction.IsFlagged,
                Amount = transaction.Amount,
                CustomerOrigID = custOrigId,
                CustomerDestID = custDestId,
                TransactionDate = transaction.TransactionDate
            };
        }
    }
}