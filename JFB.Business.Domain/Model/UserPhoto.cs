using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JFB.Business.Domain.Model
{
    public class UserPhoto
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string FatherPhoto { get; set; }
        public string ChildPhoto { get; set; }
        public int PerValue { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime PerValueTime { get; set; }
        public int IsValid { get; set; }

    }
}
