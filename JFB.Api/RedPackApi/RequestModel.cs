using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JFB.Api.RedPackApi
{
    public class RequestModel
    {
        public string openid { get; set; }
        public string amount { get; set; }
        public string clientip { get; set; }
        public string clientport { get; set; }
        public string hdclass { get; set; }
        public string sendtxt { get; set; }
        public string timecontrol { get; set; }
    }
}
