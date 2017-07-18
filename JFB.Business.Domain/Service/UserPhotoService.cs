using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JFB.Business.Domain.Model;
using ViData;
using JFB.Business.Domain.Info;

namespace JFB.Business.Domain.Service
{
    public class UserPhotoService : Repository<UserPhotoInfo, UserPhoto>
    {
        public IList<UserPhotoInfo> GetTopList(int topn)
        {
            string sql = "select top " + topn + " up.*, u.headimage, u.nickname from t_d_user u inner join (select  max(pervalue) as pervalue, user_id from t_d_user_photo where pervalue > 0 group by user_id) up on u.id = up.user_id  order by pervalue desc, id desc;c";
            return DataHelper.Fill<UserPhotoInfo>(sql);
        }

        public int GetMyTop(int userId)
        {
            string sql = "select count(0) from t_d_user_photo where  pervalue>(select max(pervalue) from t_d_user_photo where user_id = " + userId + " group by user_id)";
            return Convert.ToInt32(DataHelper.ExcuteScalar(sql));
        }

        public UserPhotoInfo GetMyLast(int userId)
        {
            string sql = "select top 1 * from t_d_user_photo where user_id = "+ userId +" order by id desc ";
            return DataHelper.Fill<UserPhotoInfo>(sql).FirstOrDefault();
        }
    }
}
