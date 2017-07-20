using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JFB.Business.Domain.Info;

namespace PJJ.Wx.Models
{
    public class XListInfo
    {
        public int count { get; set; }
        public IList<UserPhotoInfo> upList { get; set; }
    }
}