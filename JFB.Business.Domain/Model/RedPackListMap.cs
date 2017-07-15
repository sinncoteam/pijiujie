using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViData;

namespace JFB.Business.Domain.Model
{
    public class RedPackListMap : DMClassMap<RedPackList>
    {
        public RedPackListMap()
        {
            Table("t_d_redpack_list");
            Id(a => a.ID, "ID").Identity();
            Map(a => a.PackId, "pack_id");
            Map(a => a.GetTime, "gettime");
            Map(a => a.UserId, "user_id");
            Map(a => a.PackMoney, "packmoney");
            Map(a => a.PackStatus, "packstatus");
            Map(a => a.Noncestr, "noncestr");
            Map(a => a.PaySign, "paysign");
        }
    }
}
