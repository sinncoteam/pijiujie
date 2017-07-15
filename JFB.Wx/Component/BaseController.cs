using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ViCore.Caching;
using System.Configuration;
using JFB.Business.Domain.Info;

namespace JFB.Wx.Component
{
    [ValidateInput(false)]
    public class BaseController : Controller
    {
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin
        {
            get { return Authentication.Instance.IsLogin; }
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public UserInfo CurrentUser
        {
            get { return Authentication.Instance.CurrentUser; }
        }
        public RedirectResult ToNote(string n)
        {
            return Redirect("/home/note?n=" + Server.UrlEncode(n));
        }

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

        ///// <summary>
        ///// 微信支付
        ///// </summary>
        ///// <returns></returns>
        //protected WxpayInfo wxjsApiPay()
        //{
        //    string timeStamp = TenPayV3Util.GetTimestamp();
        //    string nonceStr = TenPayV3Util.GetNoncestr();
        //    JsApiTicketResult wxTicket = CacheManager.Instance.Get<JsApiTicketResult>(ConstHelper.WxTicketKey);
        //    if (wxTicket == null)
        //    {
        //        wxTicket = CommonApi.GetTicket(tenPayV3Info.AppId, tenPayV3Info.AppSecret);
        //        CacheManager.Instance.Set(ConstHelper.WxTicketKey, wxTicket, 0, wxTicket.expires_in - 120);
        //    }
        //    string url = Request.Url.AbsoluteUri;
        //    Senparc.Weixin.MP.TenPayLib.RequestHandler nativeHandler = new Senparc.Weixin.MP.TenPayLib.RequestHandler(null);
        //    nativeHandler.SetParameter("jsapi_ticket", wxTicket.ticket);
        //    nativeHandler.SetParameter("noncestr", nonceStr);
        //    nativeHandler.SetParameter("timestamp", timeStamp);
        //    nativeHandler.SetParameter("url", url);
        //    string sign = nativeHandler.CreateSHA1Sign();
        //    return new WxpayInfo()
        //    {
        //        AppId = tenPayV3Info.AppId,
        //        Noncestr = nonceStr,
        //        Timestamp = timeStamp,
        //        Signature = sign
        //    };
        //}
    }
}
