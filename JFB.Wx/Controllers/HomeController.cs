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

namespace JFB.Wx.Controllers
{
    
    public class HomeController : BaseController
    {
        //[AuthLogin]
        public ActionResult Index()
        {
            //string json = @"knhQSJz5RNmB12KbO5Py5FFR+LfDsec5nYSYDDLGx9v4mIaxgtZU8tIGLbnlVUI46H3MF8+4Wp/JFfjYw0tmn/K3hfktUeLFB12/be5RtCaf1aF0lDRvOGcXn7ta69p0T+Gy3e/qdeeadeRu8XyvtpdTAtyy7a39qGQxbSv3tSjkpQtsfobdgg1oMXe5Ve9sNFSSMuBSzo3VqcSkoCEXsUdkKw6G5Jz5ngXTKh+4xA3XD2v3Kos850MYDxqGI79OROgQBgkfy1oaHQ3NQ7MZzZ5HIsM+B4zkmS0BxZaUstgFiAhhE8zMm85j3uxAy5RLjxGi8orb/0QKwBiblzGprhND51xmhs2GUpNguoiyCGuX1drohFidK5B0U5eL+mZTpRQ9FSFuZZBmAEv6zFzn563uZO1i2D08HkrS1AO9IxgyrcC7QW62u/zpjkYgvtqCMODkPJ1BvU/o4ykNLD4A5w==";
            //string data = AESHelper.Decode(json, "7b29e94b1b0bbaed");
            ////json = AESHelper.Encode("123123123", "7b29e94b1b0bbaed");

            //string ss = AESHelper.AESDecrypt(json, "7b29e94b1b0bbaed");

            Authentication.Instance.SetAuth(new UserInfo() { ID = 4 }, true);

            ViewBag.islinked = new UserService().hasLinked(CurrentUser.ID);

            RequestModel rm = new RequestModel();
            var item = rm.getResult("http://imgcache.qq.com/open_proj/proj_qcloud_v2/gateway/event/pc/ci-identify/css/img/face_01.png", "http://imgcache.qq.com/open_proj/proj_qcloud_v2/gateway/event/pc/ci-identify/css/img/face_01.png", "123121231a");
            return View();
        }

        [AuthLogin]
        public JsonResult SetLink()
        {
            AjaxMsgResult result = new AjaxMsgResult();
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
            }
            result.Success = true;
            result.Source = list;
            result.Code = (x_upService.GetMyTop(CurrentUser.ID)+1).ToString();
            return Json(result);

        }

        public JsonResult getLast()
        {
            AjaxMsgResult result = new AjaxMsgResult();
            UserPhotoService x_upService = new UserPhotoService();
            var item = x_upService.GetMyLast(CurrentUser.ID);
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
    }
}
