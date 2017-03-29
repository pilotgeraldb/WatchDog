using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WatchDog.Hash
{
    public class WatchDogHashHelper
    {
        public static string Compute(FileStream stream)
        {
            string hash = "";

            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(stream);
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

        public static string Compute(byte[] input)
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

        public static string Compute(string input)
        {
            string hash = "";

            if (input != null)
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] bytes = md5.ComputeHash(Encoding.Default.GetBytes(input));

                    bool uppercase = false;

                    StringBuilder result = new StringBuilder(bytes.Length * 2);

                    for (int i = 0; i < bytes.Length; i++)
                    {
                        result.Append(bytes[i].ToString(uppercase ? "X2" : "x2"));
                    }

                    hash = result.ToString();
                }
            }

            return hash;
        }

        public static string Compute(string input, Encoding encoding)
        {
            string hash = "";

            if (input != null)
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] bytes = md5.ComputeHash(encoding.GetBytes(input));

                    bool uppercase = false;

                    StringBuilder result = new StringBuilder(bytes.Length * 2);

                    for (int i = 0; i < bytes.Length; i++)
                    {
                        result.Append(bytes[i].ToString(uppercase ? "X2" : "x2"));
                    }

                    hash = result.ToString();
                }
            }

            return hash;
        }
    }
}
