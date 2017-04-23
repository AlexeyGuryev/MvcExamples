﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AngularJSAuthentication.API
{
    public class Helper
    {
        public static string GetHash(string input)
        {
            var hashAlgoritm = new SHA256CryptoServiceProvider();
            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hashValue = hashAlgoritm.ComputeHash(byteValue);
            return Convert.ToBase64String(hashValue);
        }
    }
}