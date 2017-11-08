using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NonFraud.Service.Models
{
    /// <summary>
    /// Model for map a transaction from the database to the website
    /// </summary>
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [Required]
        public string Type { get; set; }

        [Display(Name = "Transaction Type")]
        public int TypeId { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Origin Customer")]
        public string NameOrig { get; set; }

        [Display(Name = "Old Balance Origin")]
        public decimal OldBalanceOrig { get; set; }

        [Display(Name = "New Balance Origin")]
        public decimal NewBalanceOrig { get; set; }

        [Required]
        [Display(Name = "Recipient Customer")]
        public string NameDest { get; set; }

        [Display(Name = "Old Balance Recipient")]
        public decimal OldBalanceDest { get; set; }

        [Display(Name = "New Balance Recipient")]
        public decimal NewBalanceDest { get; set; }

        [Display(Name = "Is Fraud")]
        public bool IsFraud { get; set; }

        [Display(Name = "Is Flagged")]
        public bool IsFlagged { get; set; }

        public List<SelectListItem> TrxTypes { get; set; }

        public string Token { get; set; }
    }
}