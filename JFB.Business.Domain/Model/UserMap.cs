using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViData;

namespace JFB.Business.Domain.Model
{
    public class UserMap : DMClassMap<User>
    {
        public UserMap()
        {
            Table("t_d_user");
            Id(a => a.ID, "ID").Identity();
            Map(a => a.OpenId, "openid");
            Map(a => a.NickName, "nickname");
            Map(a => a.HeadImage, "headimage");
            Map(a => a.CreateTime, "createtime");
            Map(a => a.IsValid, "isvalid");

            Map(a => a.RealName, "realname");
            Map(a => a.Phone, "phone");
            Map(a => a.Ages, "ages");
            Map(a => a.JobOn, "jobon");
            Map(a => a.SubTime, "subtime");
        }
    }
}
