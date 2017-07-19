using JFB.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JFB.Wx.Component
{
    public class AuthLoginAttribute : ActionFilterAttribute
    {
        //private static TenPayV3Info _tenPayV3Info;
        //public static TenPayV3Info tenPayV3Info
        //{
        //    get
        //    {
        //        if (_tenPayV3Info == null)
        //        {
        //            _tenPayV3Info =
        //                TenPayV3InfoCollection.Data[ConfigurationManager.AppSettings["mchid"]];
        //        }
        //        return _tenPayV3Info;
        //    }
        //}
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //string controlName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            //string actionName = filterContext.ActionDescriptor.ActionName.ToLower();
            //string caName = controlName + "." + actionName;
            ////不排除，都需登录 V2.0.1(3-10)
            //string[] unAuth = new string[] { "my.sendsmsreg", "my.sendsmsforget" };
            //if (unAuth.Contains(caName))
            //{
            //    return;
            //}

            if (!Authentication.Instance.IsLogin)
            {
                setoAuth(filterContext, "请先登录");
                return;
            }
        }

        private void setoAuth(ActionExecutingContext filterContext, string msg, int type = 0)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                JsonResult jr = new JsonResult();

                jr.Data = new AjaxMsgResult() { Success = false, Msg = msg, Source = type };

                filterContext.Result = jr;
            }
            else if (filterContext.IsChildAction)
            {
                filterContext.Result = new ContentResult() { Content = msg };
            }
            else
            {
                string pq = null;
                //if (filterContext.HttpContext.Request.Url != null)
                //{
                //    pq = filterContext.HttpContext.Request.Url.PathAndQuery;
                //}
                //string host = filterContext.HttpContext.Request.Url.Host;
                //host = filterContext.HttpContext.Server.UrlEncode("http://" + host + "/user/wxlogin");
                //string loginurl = filterContext.HttpContext.Server.UrlEncode("/wx/login?s=" + pq);
                //host = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + tenPayV3Info.AppId + "&redirect_uri=" + host + "&response_type=code&scope=snsapi_base&state=" + loginurl + "#wechat_redirect";
                string host = ConfigurationManager.AppSettings["host"];
                //string host = "xianyunsoft.xicp.cn";
                string hostUrl = "http://wxin2.cqnews.net/authorize.aspx?gp=53f6b7a0975642e9801f0d91d6042a70&ga=0682ed39d45745ae879f43d06e267ed0&opa=";
                 hostUrl += HttpUtility.UrlEncode( "http://"+ host +"/pjj/wx/reclogin?s="+pq);
                
                filterContext.Result = new RedirectResult(hostUrl);
            }
        }
    }
}