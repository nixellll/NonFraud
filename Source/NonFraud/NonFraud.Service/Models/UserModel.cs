using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NonFraud.Service.Models
{
    /// <summary>
    ///  Model for map a User from the database to the website
    /// </summary>
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Display(Name = "Profile")]
        public int ProfileID { get; set; }

        public string Profile { get; set; }

        public string Token { get; set; }
    }
}