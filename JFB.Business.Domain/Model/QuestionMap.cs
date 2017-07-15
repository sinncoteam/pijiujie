using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViData;

namespace JFB.Business.Domain.Model
{
    public class QuestionMap : DMClassMap<Question>
    {
        public QuestionMap()
        {
            Table("t_d_question");
            Id(a => a.Id, "ID").Identity();
            Map(a => a.QTitle, "qtitle");
            Map(a => a.QType, "qtype");
            Map(a => a.QAnswer, "qanswer");
            Map(a => a.IsValid, "isvalid");
            Map(a => a.CreateTime, "createtime");
        }
    }
}
