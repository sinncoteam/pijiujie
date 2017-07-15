using JFB.Api.RedPackApi;
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

            //Authentication.Instance.SetAuth(new UserInfo() { ID = 8 }, true);

          
            return View();
        }

       
    }
}
