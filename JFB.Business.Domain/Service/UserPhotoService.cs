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
            string sql = "select top " + topn + " up.*, u.headimage, u.nickname from t_d_user u inner join (select  max(pervalue) as pervalue, user_id from t_d_user_photo where pervalue > 0 and isvalid = 1 group by user_id) up on u.id = up.user_id  order by pervalue desc, id asc";
            return DataHelper.Fill<UserPhotoInfo>(sql);
        }

        public int GetMyTop(int userId)
        {
            var list = this.GetTopList(20);
            int i = 1;
            foreach (var item in list)
            {
                if (item.UserId == userId)
                {
                    return i;
                }
            }
            string sql = "select count(0) from t_d_user_photo where  pervalue>=(select max(pervalue) from t_d_user_photo where user_id = " + userId + " group by user_id)";
            return Convert.ToInt32(DataHelper.ExcuteScalar(sql));
        }

        public UserPhotoInfo GetMyLast(int userId)
        {
            string sql = "select top 1 * from t_d_user_photo where user_id = "+ userId +" order by id desc ";
            return DataHelper.Fill<UserPhotoInfo>(sql).FirstOrDefault();
        }

        public IList<UserPhotoInfo> GetAllList()
        {
            string sql = "select u.*, upo.fatherphoto, upo.childphoto, upo.pervalue from t_d_user u inner join t_d_user_photo upo on u.ID = upo.user_id inner join (select  MIN(up2.id) id from (select  max(up.pervalue) pev, user_id from t_d_user_photo as up group by user_id) as a inner join t_d_user_photo up2 on a.user_id = up2.user_id and up2.pervalue = a.pev group by a.pev, a.user_id) c on c.id = upo.ID where upo.isvalid = 1 and upo.pervalue > 0 and u.isvalid = 1 order by upo.pervalue desc";
            return DataHelper.Fill<UserPhotoInfo>(sql);
        }
    }
}
