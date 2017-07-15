using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JFB.Utils
{
    public class zTreeData
    {
        public string id { get; set; }
        public string pId { get; set; }
        public string name { get; set; }
        public bool open { get; set; }
        public bool @checked { get; set; }
    }
}
