using NonFraud.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NonFraud.Service.Mappers
{
    /// <summary>
    /// Mapper class for Transaction Type entity and model
    /// </summary>
    public class TransactionTypeMapper
    {
        /// <summary>
        /// Maps DB data to SelectListItem web component
        /// </summary>
        /// <param name="trxTypes">List of trx types from DB</param        
        public List<SelectListItem> Map(List<TransactionType> trxTypes)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var trxType in trxTypes)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = trxType.TransactionTypeName,
                    Value = trxType.TransactionTypeID.ToString()
                };

                items.Add(item);
            }

            return items;
        }
    }
}