using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NonFraud.Web.Helpers
{
    /// <summary>
    /// Helper to generate token for avoid unathorized request in Service
    /// </summary>
    public class EncryptionHelper
    {
        private const string _algorithm = "HmacSHA256";
        private const string _salt = "ywDEiNR7cjMS1sUmZiPu7fgQWAyW99OaHsaYx1vgSGk50HwuRV";

        public string GenerateToken(string hash)
        {            
            string firstPart = string.Empty;
            string secondPart = string.Empty;

            using (HMAC hmac = HMACSHA256.Create(_algorithm))
            {
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                firstPart = Convert.ToBase64String(hmac.Hash);
                secondPart = string.Join(":", new string[] { hash });
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", firstPart, secondPart)));
        }
    }
}