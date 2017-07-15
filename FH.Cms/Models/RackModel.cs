using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JFB.Cms.Models
{
    public class RackDataDto
    {
        public IList<RackPoint> Points { get; set; }

        public class RackPoint
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}