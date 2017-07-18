using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace JFB.Utils
{
    public class AESHelper
    {
        ////默认密钥向量
        //private static byte[] Keys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };
        private static string KeysIV = "E4ghj*Ghg7!rNIfb&*5GUY86KfghUb#er57HBh(u%g6HM($jhWk7&!hg4ui%$hjk".Substring(0,16);
        private static string encryptKey = "bqdhIyip60M0vb6m";
        private static string decryptKey = "8db4715aa37ba370";
        public static string Encode(string encryptString)
        {
            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.GenerateIV();
            rijndaelProvider.GenerateKey();
            int keylength = rijndaelProvider.Key.Length;
            if (encryptKey.Length > keylength)
            {
                encryptKey = encryptKey.Substring(0, keylength);
            }
            else if (encryptKey.Length < keylength)
            {
                encryptKey = encryptKey.PadRight(keylength, ' ');
            }

            int ivlength = rijndaelProvider.IV.Length;
            if (KeysIV.Length > ivlength)
            {
                KeysIV = KeysIV.Substring(0, ivlength);
            }
            else if (KeysIV.Length > ivlength)
            {
                KeysIV = KeysIV.PadRight(ivlength, ' ');
            }

            rijndaelProvider.Key = UTF8Encoding.UTF8.GetBytes(encryptKey);
            rijndaelProvider.IV = UTF8Encoding.UTF8.GetBytes(KeysIV);
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

            byte[] inputData = UTF8Encoding.UTF8.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Convert.ToBase64String(encryptedData);
        }

        public static string Decode(string decryptString)
        {
            try
            {                
                RijndaelManaged rijndaelProvider = new RijndaelManaged();
                rijndaelProvider.GenerateIV();
                rijndaelProvider.GenerateKey();
                int keylength = rijndaelProvider.Key.Length;
                if (decryptKey.Length > keylength)
                {
                    decryptKey = decryptKey.Substring(0, keylength);
                }
                else if(decryptKey.Length < keylength)
                {
                    decryptKey = decryptKey.PadRight(keylength, ' ');
                }            
                
                int ivlength = rijndaelProvider.IV.Length;
                if (KeysIV.Length > ivlength)
                {
                    KeysIV = KeysIV.Substring(0, ivlength);
                }
                else if(KeysIV.Length > ivlength)
                {
                    KeysIV = KeysIV.PadRight(ivlength, ' ');
                }
                rijndaelProvider.Key = UTF8Encoding.UTF8.GetBytes(decryptKey);
                rijndaelProvider.IV = UTF8Encoding.UTF8.GetBytes(KeysIV);

                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return UTF8Encoding.UTF8.GetString(decryptedData);
            }
            catch(Exception ex)
            {
                return "";
            }

        }

        //public static string AESDecrypt(string encryptStr, string key)
        //{
        //   // encryptStr = "Q5XzSxU4NAtqfHcJ75nucw==";

        //    byte[] bKey = UTF8Encoding.UTF8.GetBytes("7b29e94b1b0bbaed");                       
        //    byte[] bIV = UTF8Encoding.UTF8.GetBytes("E4ghj*Ghg7!rNIfb&*5GUY86KfghUb#er57HBh(u%g6HM($jhWk7&!hg4ui%$hjk".Substring(0,16));
        //    byte[] byteArray = Convert.FromBase64String(encryptStr);

        //    string decrypt = null;
        //    Rijndael aes = Rijndael.Create();
        //    try
        //    {
        //        var dector = aes.CreateDecryptor(bKey, bIV);
        //        using (MemoryStream mStream = new MemoryStream())
        //        {
        //            using (CryptoStream cStream = new CryptoStream(mStream, dector, CryptoStreamMode.Write))
        //            {
        //                cStream.Write(byteArray, 0, byteArray.Length);
        //                cStream.FlushFinalBlock();
        //                decrypt = UTF8Encoding.UTF8.GetString(mStream.ToArray());
        //            }
        //        }
        //    }
        //    catch(Exception ex) { }
        //    aes.Clear();

        //    return decrypt;
        //}
        
    }
}
