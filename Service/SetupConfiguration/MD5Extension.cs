using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Service
{
    public static class MD5Extension
    {
        public static string ToMD5(this string value)
        {
            if (value == null)
                return string.Empty;

            using (var md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                var stringBuilder = new StringBuilder();

                foreach (var byteData in data)
                {
                    stringBuilder.Append(byteData.ToString("x2"));
                }

                return stringBuilder.ToString().ToLower();
            }
        }
    }
}
