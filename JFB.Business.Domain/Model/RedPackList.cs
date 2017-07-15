using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFB.Business.Domain.Model
{
    public class RedPackList
    {
        public int ID { get; set; }
        public int PackId { get; set; }
        public DateTime GetTime { get; set; }
        public int UserId { get; set; }
        public int PackMoney { get; set; }
        public int PackStatus { get; set; }
        public string Noncestr { get; set; }
        public string PaySign { get; set; }
    }
}
