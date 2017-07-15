using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFB.Business.Domain.Model
{
    public class RedPack
    {
        public int ID { get; set; }
        public string RbName { get; set; }
        public int RbTotal { get; set; }
        public int RbCount { get; set; }
        public int RbMoney { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int CreateOperator_Id { get; set; }
        public int IsValid { get; set; }
        public int GetPercent { get; set; }
    }
}
