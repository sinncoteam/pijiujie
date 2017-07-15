using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JFB.Business.Domain.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string QTitle { get; set; }
        public int QType { get; set; }
        public string QAnswer { get; set; }
        public DateTime CreateTime { get; set; }
        public int IsValid { get; set; }
    }
}
