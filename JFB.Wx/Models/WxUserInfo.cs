using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JFB.Wx.Models
{
    public class WxUserInfo
    {
        public string openid { get; set; }
        public string unionid { get; set; }
        public string nickname { get; set; }
        public string headimgurl { get; set; }
        public string subscribe_state { get; set; }
    }
}