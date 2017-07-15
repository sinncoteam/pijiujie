using JFB.Business.Domain.Info;
using JFB.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViData;

namespace JFB.Business.Domain.Service
{
    public class QuestionService : Repository<QuestionInfo, Question>
    {
        /// <summary>
        /// 获取随机问题列表
        /// </summary>
        /// <param name="topn"></param>
        /// <returns></returns>
        public IList<QuestionInfo> GetRandomList(int topn)
        {
            string sql = "select top "+ topn +" * from t_d_question where isvalid = 1 order by newid() ";
            var list = DataHelper.Fill<QuestionInfo>(sql);
            AnswerServie x_aService = new AnswerServie();
            var anlist = x_aService.Get(a => a.IsValid == 1);
            foreach(var item in list)
            {
                item.AnswerList = anlist.Where(a => a.QuestionId == item.Id).ToList();
            }
            return list;
        }

        
    }
}
