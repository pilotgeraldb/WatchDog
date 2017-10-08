using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WatchDog.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string Hash(this byte[] input)
        {
            string hash = "";

            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(input);
                bool uppercase = false;

                StringBuilder result = new StringBuilder(bytes.Length * 2);

                for (int i = 0; i < bytes.Length; i++)
                {
                    result.Append(bytes[i].ToString(uppercase ? "X2" : "x2"));
                }

                hash = result.ToString();
            }

            return hash;
        }
    }
}
