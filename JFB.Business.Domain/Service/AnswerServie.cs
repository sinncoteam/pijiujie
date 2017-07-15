using JFB.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViData;

namespace JFB.Business.Domain.Service
{
    public class AnswerServie : Repository<Answer, Answer>
    {
        /// <summary>
        /// 查询是否正确答案
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="answercode"></param>
        /// <returns></returns>
        public Answer IsRightAnswer(int questionId, string answercode)
        {
            var item = this.Get(a => a.QuestionId == questionId && a.QAnswerCode == answercode && a.IsRight == 1).FirstOrDefault();
            return item;
        }
        
    }
}
