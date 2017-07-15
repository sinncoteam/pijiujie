using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViData;

namespace JFB.Business.Domain.Model
{
    public class RedPackMap : DMClassMap<RedPack>
    {
        public RedPackMap()
        {
            Table("t_d_redpack");
            Id(a => a.ID, "ID").Identity();
            Map(a => a.RbName, "rbname");
            Map(a => a.RbTotal, "rbtotal");
            Map(a => a.RbCount, "rbcount");
            Map(a => a.RbMoney, "rbmoney");
            Map(a => a.CreateOperator_Id, "createoperator_id");
            Map(a => a.CreateTime, "createtime");
            Map(a => a.IsValid, "isvalid");
            Map(a => a.GetPercent, "getpercent");
        }
    }
}
