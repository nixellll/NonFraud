using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonFraud.Data.Models
{
    /// <summary>
    /// Model for map a transaction from the database to the website
    /// </summary>
    public class TransactionModel
    {
        [Key]
        public int TransactionID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }

        [Required]
        public string Type { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Origin Customer")]
        public string NameOrig { get; set; }

        public decimal OldBalanceOrig { get; set; }

        public decimal NewBalanceOrig { get; set; }

        [Required]
        [Display(Name = "Recipient Customer")]
        public string NameDest { get; set; }

        public decimal OldBalanceDest { get; set; }

        public decimal NewBalanceDest { get; set; }

        public bool IsFraud { get; set; }

        public bool IsFlagged { get; set; }
    }
}
