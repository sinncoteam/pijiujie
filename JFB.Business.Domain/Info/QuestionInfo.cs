using JFB.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JFB.Business.Domain.Info
{
    public class QuestionInfo : Question
    {
        public IList<Answer> AnswerList { get; set; }
    }
}
