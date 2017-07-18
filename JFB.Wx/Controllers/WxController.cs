using JFB.Business.Domain.Info;
using JFB.Business.Domain.Service;
using JFB.Utils;
using JFB.Wx.Component;
using JFB.Wx.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ViCore.Logging;

namespace JFB.Wx.Controllers
{
    public class WxController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Login()
        //{
        //    string code = Request.QueryString["code"];
        //    string state = Request.QueryString["state"];
        //    string s = Request.QueryString["s"];
        //    if (!IsLogin)
        //    {
        //        OAuthAccessTokenResult result = OAuthApi.GetAccessToken(tenPayV3Info.AppId, tenPayV3Info.AppSecret, code);
        //        if (result.errcode == Senparc.Weixin.ReturnCode.请求成功)
        //        {
        //            string openid = result.openid;
        //            UserService x_userService = new UserService();
        //            var item = x_userService.Get(a => a.OpenId == openid).FirstOrDefault();
        //            if (item != null)
        //            {
        //                if (item.IsValid == 1)
        //                {
        //                    Authentication.Instance.SetAuth(item, true);
        //                }
        //                else
        //                {
        //                    return Redirect("/");
        //                }
        //            }
        //            else  //获取用户的信息
        //            {
        //                OAuthUserInfo wxUser = OAuthApi.GetUserInfo(result.access_token, code);
        //                if (wxUser != null)
        //                {
        //                    UserInfo uInfo = new UserInfo()
        //                    {
        //                        IsValid = 1,
        //                        CreateTime = DateTime.Now,
        //                        Lastlogintime = DateTime.Now,
        //                        NickName = wxUser.nickname,
        //                        OpenId = wxUser.openid,
        //                        HeadImage = wxUser.headimgurl
        //                    };
        //                    uInfo.ID = Convert.ToInt32(x_userService.Insert(uInfo));
        //                    Authentication.Instance.SetAuth(uInfo, true);
        //                }
        //                else
        //                {
        //                    return Redirect("/");
        //                }
        //            }
        //        }
        //    }
        //    string url = "/";
        //    if (!string.IsNullOrEmpty(s))
        //    {
        //        url = s;
        //    }
        //    return Redirect(url);
        //}

        public ActionResult RecLogin()
        {
            string json = Request.Form["jsondata"];
            string data = AESHelper.Decode(json);
            //Logging4net.WriteInfo(data);
            if (!string.IsNullOrEmpty(data))
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                WxUserInfo wxu = jss.Deserialize<WxUserInfo>(data);

                UserService x_userService = new UserService();
                var item = x_userService.Get(a => a.OpenId == wxu.openid).FirstOrDefault();
                if (item != null)
                {
                    Logging4net.WriteInfo("isvalid:" + item.IsValid);
                    if (item.IsValid == 1)
                    {
                        Authentication.Instance.SetAuth(item, true);
                        return RedirectToAction("index","home");
                    }
                }
                else
                {
                    UserInfo uInfo = new UserInfo()
                    {
                        IsValid = 1,
                        CreateTime = DateTime.Now,
                        //Lastlogintime = DateTime.Now,
                        NickName = wxu.nickname,
                        OpenId = wxu.openid,
                        HeadImage = wxu.headimgurl,
                         SubTime = DateTime.Now.AddYears(-100)
                    };
                    uInfo.ID = Convert.ToInt32(x_userService.Insert(uInfo));
                    Authentication.Instance.SetAuth(uInfo, true);
                    return RedirectToAction("index", "home");
                }
            }
            return RedirectToAction("index","home");
        }
    }
}
