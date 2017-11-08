using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NonFraud.Service.Helpers
{
    /// <summary>
    /// Helper to generate token for avoid unathorized request in Service
    /// </summary>
    public class EncryptionHelper
    {
        public bool ValidateToken(string token, string hash)
        {
            if (token == "")
                return false;

            string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            string[] keys = key.Split(':');

            if (keys.Length != 2)
                return false;

            if (keys[1] != hash)
                return false;

            return true;
        }
    }
}