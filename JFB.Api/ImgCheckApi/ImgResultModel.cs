using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JFB.Api.ImgCheckApi
{
    public class ImgResultModel
    {
        public MData data { get; set; }
        public int code { get; set; }
        public string Message { get; set; }
    }

    public class MData
    {
        public double similarity { get; set; }
        public string session_id { get; set; }

    }
}
