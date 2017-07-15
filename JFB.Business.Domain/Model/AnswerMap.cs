using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViData;

namespace JFB.Business.Domain.Model
{
    public class AnswerMap : DMClassMap<Answer>
    {
        public AnswerMap()
        {
            Table("t_d_answer");
            Id(a => a.Id, "ID").Identity();
            Map(a => a.QAnswer, "qanswer");
            Map(a => a.QAnswerCode, "qanswercode");
            Map(a => a.QuestionId, "question_id");
            Map(a => a.IsValid, "isvalid");
            Map(a => a.IsRight, "isright");
            Map(a => a.CreateTime, "createtime");
        }
    }
}
