using JFB.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JFB.Business.Domain.Info
{
    public class RedPackListInfo : RedPackList
    {
        public string NickName { get; set; }
        public string OpenId { get; set; }
        public string RbName { get; set; }

    }
}
