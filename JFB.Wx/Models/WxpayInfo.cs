using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PJJ.Wx.Models
{
    public class WxpayInfo
    {
        public string AppId {get;set;}
        public string        Noncestr {get;set;}
          public string      Timestamp {get;set;}
          public string Signature { get; set; }
    }
}