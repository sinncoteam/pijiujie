using JFB.Business.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViData;

namespace JFB.Business.Domain.Service
{
    public class RedPackService : Repository<RedPack, RedPack>
    {
        /// <summary>
        /// 重置状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ResetStatus(string ids, int type)
        {
            string[] arr = ids.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in arr)
            {
                Convert.ToInt32(s);
            }
            ids = ids.Trim(',');
            string sql = "update t_d_redpack set isvalid = " + type + " where id in(" + ids + ")";
            int i = DataHelper.ExcuteNonQuery(sql);
            return i;
        }
    }
}
