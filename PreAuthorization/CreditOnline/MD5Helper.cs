using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CreditOnline
{
    public class MD5Helper
    {
        public static string GetMD5Hash(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            char[] temp = new char[res.Length];
            System.Array.Copy(res, temp, res.Length);
            return new String(temp);
        }
    }
}