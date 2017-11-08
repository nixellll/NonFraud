namespace NonFraud.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Customer Balance generated class
    /// </summary>
    [Table("CustomerBalance")]
    public partial class CustomerBalance
    {
        public int CustomerBalanceID { get; set; }

        public int CustomerID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal OldBalance { get; set; }

        [Column(TypeName = "numeric")]
        public decimal NewBalance { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
