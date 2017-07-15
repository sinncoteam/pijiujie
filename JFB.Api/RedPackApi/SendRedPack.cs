using JFB.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using ViCore.Logging;

namespace JFB.Api.RedPackApi
{
    public class SendRedPack
    {
        /// <summary>
        /// 发送红包
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string SendTo(RequestModel model)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(model);

            AESHelper Aes = new AESHelper();
            string jsondata = AESHelper.Encode(json);
            string postData = "par=" + HttpUtility.UrlEncode(jsondata);
            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            string url = "http://wxin2.cqnews.net/yxhb_grp/pay_qxbbc_interface.aspx?"+ postData;

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            
            webRequest.ContentType = "application/x-www-form-urlencoded";
            //webRequest.ContentLength = byteArray.Length;

            //using (Stream newStream = webRequest.GetRequestStream())//创建一个Stream,赋值是写入HttpWebRequest对象提供的一个stream里面
            //{
            //    newStream.Write(byteArray, 0, byteArray.Length);
            //    newStream.Close();
                //4． 读取服务器的返回信息
                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        string content = reader.ReadToEnd();
                    Logging4net.WriteInfo("content: " + content);
                    return content;
                    }
                }
            //}
        }
    }
}
