namespace NonFraud.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Transaction generated class
    /// </summary>
    [Table("Transaction")]
    public partial class Transaction
    {
        public int TransactionID { get; set; }

        public int TransactionTypeID { get; set; }

        public bool IsFraud { get; set; }

        public bool IsFlagged { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        public int CustomerOrigID { get; set; }

        public int CustomerDestID { get; set; }

        public DateTime TransactionDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Customer Customer1 { get; set; }

        public virtual TransactionType TransactionType { get; set; }
    }
}
