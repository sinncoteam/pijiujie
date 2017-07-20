using JFB.Business.Domain.Info;
using JFB.Business.Domain.Model;
using JFB.Business.Domain.Service;
using JFB.Utils;
using JFB.Wx.Component;
using JFB.Wx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PJJ.Wx.Models;
using JFB.Api.ImgCheckApi;
using System.IO;
using System.Configuration;
using Senparc.Weixin.MP.TenPayLibV3;
using Senparc.Weixin.MP.Entities;
using ViCore.Caching;
using Senparc.Weixin.MP.CommonAPIs;

namespace JFB.Wx.Controllers
{
    
    public class HomeController : BaseController
    {

        [AuthLogin]
        public ActionResult Index()
        {
            //string json = @"knhQSJz5RNmB12KbO5Py5FFR+LfDsec5nYSYDDLGx9v4mIaxgtZU8tIGLbnlVUI46H3MF8+4Wp/JFfjYw0tmn/K3hfktUeLFB12/be5RtCaf1aF0lDRvOGcXn7ta69p0T+Gy3e/qdeeadeRu8XyvtpdTAtyy7a39qGQxbSv3tSjkpQtsfobdgg1oMXe5Ve9sNFSSMuBSzo3VqcSkoCEXsUdkKw6G5Jz5ngXTKh+4xA3XD2v3Kos850MYDxqGI79OROgQBgkfy1oaHQ3NQ7MZzZ5HIsM+B4zkmS0BxZaUstgFiAhhE8zMm85j3uxAy5RLjxGi8orb/0QKwBiblzGprhND51xmhs2GUpNguoiyCGuX1drohFidK5B0U5eL+mZTpRQ9FSFuZZBmAEv6zFzn563uZO1i2D08HkrS1AO9IxgyrcC7QW62u/zpjkYgvtqCMODkPJ1BvU/o4ykNLD4A5w==";
            //string data = AESHelper.Decode(json, "7b29e94b1b0bbaed");
            ////json = AESHelper.Encode("123123123", "7b29e94b1b0bbaed");

            //string ss = AESHelper.AESDecrypt(json, "7b29e94b1b0bbaed");

            //Authentication.Instance.SetAuth(new UserInfo() { ID = 4 }, true);

            string host = ConfigurationManager.AppSettings["host"];
            ViewBag.islinked = new UserService().hasLinked(CurrentUser.ID);
            ViewBag.isInTime = isInTime();
            var wx = wxjsApiPay();
            ViewBag.AppId = wx.AppId;
            ViewBag.Timestamp = wx.Timestamp;
            ViewBag.Noncestr = wx.Noncestr;
            ViewBag.Signature = wx.Signature;
            string p =  Request.QueryString["p"];
            if (string.IsNullOrEmpty(p))
            {
                p = "1";
            }
            ViewBag.P = p;
            int uid = 0;
            if(!string.IsNullOrEmpty(Request.QueryString["u"]))
            {
                uid = Convert.ToInt32(Request.QueryString["u"]);
            }
            ViewBag.U = uid;
            ViewBag.ShareUrl = "http://"+ host+"/pjj?p=3&u="+ CurrentUser.ID;
            ViewBag.Logo = "http://"+ host +"/pjj/images/logo.png";

            //RequestModel rm = new RequestModel();
            //var item = rm.getResult("http://imgcache.qq.com/open_proj/proj_qcloud_v2/gateway/event/pc/ci-identify/css/img/face_01.png", "http://imgcache.qq.com/open_proj/proj_qcloud_v2/gateway/event/pc/ci-identify/css/img/face_01.png", "123121231a");
            return View();
        }

        [AuthLogin]
        public JsonResult SetLink()
        {
            AjaxMsgResult result = new AjaxMsgResult();
            if (!isInTime())
            {
                result.Msg = "活动已过期";
                return Json(result);
            }
            string realname = Request.Form["x_realname"];
            string phone = Request.Form["x_phone"];
            int ages = Convert.ToInt32(Request.Form["x_ages"]);
            string jobon = Request.Form["x_jobon"];

            UserService x_uService = new UserService();
            int i = x_uService.Update(() => new UserInfo() { RealName = realname, Phone = phone, Ages = ages, JobOn = jobon, SubTime = DateTime.Now }, a => a.ID == CurrentUser.ID);
            result.Success = true;
            return Json(result);

        }

        [AuthLogin]
        public JsonResult GetList()
        {
            AjaxMsgResult result = new AjaxMsgResult();
            UserPhotoService x_upService = new UserPhotoService();
           
            
            List<UPInfo> list = new List<UPInfo>();
            var upl = x_upService.GetTopList(20);
            int i = 1;
            foreach (var item in upl)
            {
                UPInfo info = new UPInfo()
                {
                    I = i,
                     HeadImage = item.HeadImage,
                      NickName = item.NickName,
                       PerValue = item.PerValue
                };
                list.Add(info);
                i++;
            }
            result.Success = true;
            result.Source = list;
            result.Code = x_upService.GetMyTop(CurrentUser.ID).ToString();
            return Json(result);

        }
        [AuthLogin]
        public JsonResult getLast()
        {

            AjaxMsgResult result = new AjaxMsgResult();
            if (!isInTime())
            {
                result.Msg = "活动已过期";
                return Json(result);
            }
            int userid = 0;
            if (!string.IsNullOrEmpty(Request.Form["uid"]))
            {
                userid = Convert.ToInt32(Request.Form["uid"]);
            }
            if (userid == 0)
            {
                userid = CurrentUser.ID;
            }
            UserPhotoService x_upService = new UserPhotoService();
            var item = x_upService.GetMyLast(userid);
            item.FatherPhoto += "_1.jpg";
            item.ChildPhoto += "_1.jpg";
            if (item != null)
            {
                result.Success = true;
                result.Source = item;
            }
            else
            {
                result.Success = false;
                result.Msg = "请先上传照片";
            }
            return Json(result);
        }
        [AuthLogin]
        public JsonResult upFile()
        {
            AjaxMsgResult reuslt = new AjaxMsgResult();
            if (!isInTime())
            {
                reuslt.Msg = "活动已过期";
                return Json(reuslt);
            }
            HttpPostedFileBase file = Request.Files[0];
            string skey = "x_photo_up";
            UpFileTypeInfo uftype = new UpFileTypeInfo();
            if (Session[skey] != null)
            {
                uftype = Session[skey] as UpFileTypeInfo;
            }
            else
            {
                Session[skey] = uftype;
            }
            if (file != null)
            {
                string oripath = "/uploads/" + CurrentUser.ID + "/";
                string path = Server.MapPath(oripath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filename = DateTime.Now.ToString("yyMMddhhmmssfff") + file.FileName;
                file.SaveAs(path + filename);
                ThumbnailHelper.MakeThumbnailImage(path + filename, path + filename + "_1.jpg", 400, 400);
                string dbpath = oripath + filename;
                if (string.IsNullOrEmpty(uftype.fatherp))
                {
                    uftype.fatherp = dbpath;
                }
                else if (string.IsNullOrEmpty(uftype.childp))
                {
                    uftype.childp = dbpath;
                }
                if (!string.IsNullOrEmpty(uftype.fatherp) && !string.IsNullOrEmpty(uftype.childp))
                {
                    UserPhotoService x_upService = new UserPhotoService();
                    UserPhotoInfo uinfo = new UserPhotoInfo()
                    {
                        UserId = CurrentUser.ID,
                         FatherPhoto = uftype.fatherp,
                          ChildPhoto = uftype.childp,
                           CreateTime = DateTime.Now,
                            IsValid = 1,
                             PerValueTime = DateTime.Now.AddYears(-100)
                    };
                    x_upService.Insert(uinfo);
                    uftype.fatherp = null;
                    uftype.childp = null;
                }
                reuslt.Success = true;
                reuslt.Source = dbpath + "_1.jpg";
            }
            return Json(reuslt);
        }

        bool isInTime()
        {
            string timestr = ConfigurationManager.AppSettings["gametime"];
            string[] timearr = timestr.Split('-');
            DateTime t1 = Convert.ToDateTime(timearr[0]);
            DateTime t2 = Convert.ToDateTime(timearr[1]);
            if (DateTime.Now >= t1 && DateTime.Now <= t2)
            {
                return true;
            }
            return false;
        }

        string Appid = ConfigurationManager.AppSettings["appid"];
        string Appkey = ConfigurationManager.AppSettings["appkey"];
        protected WxpayInfo wxjsApiPay()
        {
            string wxkey = "s_wx_key_ticket";
            string timeStamp = TenPayV3Util.GetTimestamp();
            string nonceStr = TenPayV3Util.GetNoncestr();
            JsApiTicketResult wxTicket = CacheManager.Instance.Get<JsApiTicketResult>(wxkey);
            if (wxTicket == null)
            {
                wxTicket = CommonApi.GetTicket(Appid, Appkey);
                CacheManager.Instance.Set(wxkey, wxTicket, 0, wxTicket.expires_in - 120);
            }
            string url = Request.Url.AbsoluteUri;
            Senparc.Weixin.MP.TenPayLib.RequestHandler nativeHandler = new Senparc.Weixin.MP.TenPayLib.RequestHandler(null);
            nativeHandler.SetParameter("jsapi_ticket", wxTicket.ticket);
            nativeHandler.SetParameter("noncestr", nonceStr);
            nativeHandler.SetParameter("timestamp", timeStamp);
            nativeHandler.SetParameter("url", url);
            string sign = nativeHandler.CreateSHA1Sign();
            return new WxpayInfo()
            {
                AppId = Appid,
                Noncestr = nonceStr,
                Timestamp = timeStamp,
                Signature = sign
            };
        }

        public ActionResult xTolist()
        {
            UserPhotoService x_upService = new UserPhotoService();
            var list = x_upService.GetAllList();
            XListInfo xt = new XListInfo();
            xt.upList = list;
            xt.count = list.Count;
            return View(xt);
        }

        public JsonResult outit()
        {
            AjaxMsgResult result = new AjaxMsgResult();
            UserService x_uService = new UserService();
            int uid = Convert.ToInt32(Request.Form["d"]);
            x_uService.Update(() => new UserInfo() { IsValid = 0 }, a => a.ID == uid);
            result.Success = true;
            return Json(result);
        }
    }
}
