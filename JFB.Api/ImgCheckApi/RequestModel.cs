using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using ViCore.Logging;
using System.Web.Script.Serialization;
using FH.Utils;

namespace JFB.Api.ImgCheckApi
{
    public class RequestModel
    {
        private string _authrize;
        public string Authrize
        {
            get 
            {
                if (string.IsNullOrEmpty(_authrize))
                {
                    _authrize = string.Format("a={0}&b={1}&k={2}&e={3}&t={4}&r={5}&u=0&f=", "1252343337", "pjj0", "AKIDCRbdVCW5IJDyYDtFbmss00paFpHXehWC", TimeHelper.GetTimeStamp(DateTime.Now.AddDays(30), 10), TimeHelper.GetTimeStamp(DateTime.Now, 10), new Random().Next(4, 100000000));
                    //_authrize = "a=1252821871&b=tencentyun&k=AKIDgaoOYh2kOmJfWVdH4lpfxScG2zPLPGoK&e=1438669115&t=1436077115&r=11162&u=0&f=";
                }
                return _authrize;
            }
        }

        public const string AuthrizeKey = "mx21pT4Ys0ynV7e3IPL5e1DpLk3JQM7z";
        //public const string AuthrizeKey = "nwOKDouy5JctNOlnere4gkVoOUz5EYAb";


        public string getAuthrizeStr()
        {
            HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(AuthrizeKey), true);

            var dataBuffer = Encoding.UTF8.GetBytes(Authrize);
            byte[] hashBytes = hmac.ComputeHash(dataBuffer);
            byte[] allBytes = hashBytes.Concat(dataBuffer).ToArray();

            string str = Convert.ToBase64String(allBytes);
            return str;
        }

        public ImgResultModel getResult(string img1, string img2, string imgId  = null)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string url = "http://service.image.myqcloud.com/face/compare";
            string authkey = getAuthrizeStr();
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            //webRequest.Credentials = new NetworkCredential("AKIDCRbdVCW5IJDyYDtFbmss00paFpHXehWC", AuthrizeKey);
            webRequest.Headers.Add("Authorization", authkey);
            webRequest.ContentType = "application/json";
            string jsonData = jss.Serialize(new { appid = "1252343337", urlA = img1, urlB = img2 });
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonData);
            webRequest.ContentLength = byteArray.Length;
            using (Stream newStream = webRequest.GetRequestStream())//创建一个Stream,赋值是写入HttpWebRequest对象提供的一个stream里面
            {
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();
                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        string content = reader.ReadToEnd();
                        ///Logging4net.WriteInfo("content: " + content);

                        ImgResultModel model = jss.Deserialize<ImgResultModel>(content);
                        return model;
                    }
                }
            }
        }
    }


}
