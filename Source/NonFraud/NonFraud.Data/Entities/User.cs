namespace NonFraud.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// User generated class
    /// </summary>
    [Table("User")]
    public partial class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        public int ProfileID { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
