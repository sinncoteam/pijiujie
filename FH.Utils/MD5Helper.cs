using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace JFB.Utils
{
    public class MD5Helper
    {
        public static string Md5(string source)
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = MD5.Create();
            byte[] tmp_source = System.Text.Encoding.UTF8.GetBytes(source);
            byte[] tmp_hash = md5.ComputeHash(tmp_source, 0, tmp_source.Length);
            foreach (byte b in tmp_hash)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }//end method

        public static string Md5small(string source)
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = MD5.Create();
            byte[] tmp_source = System.Text.Encoding.UTF8.GetBytes(source);
            byte[] tmp_hash = md5.ComputeHash(tmp_source, 0, tmp_source.Length);
            foreach (byte b in tmp_hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
