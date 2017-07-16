using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JFB.Business.Domain.Model;

namespace JFB.Business.Domain.Info
{
    public class UserPhotoInfo : UserPhoto
    {
        public string HeadImage { get; set; }
        public string NickName { get; set; }

    }
}
