using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NonFraud.Web.Models
{
    /// <summary>
    /// Model for getting the service response
    /// </summary>
    public class ResponseModel
    {
        public bool IsValid { get; set; }
        public string Result { get; set; }
    }
}