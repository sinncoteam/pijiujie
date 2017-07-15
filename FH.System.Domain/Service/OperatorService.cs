/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。  
//
// 文件名：OperatorService.cs
// 文件功能描述： 领域层服务实现
//
// 
// 创建标识：   dxk -- 2017/1/12 14:42:35 
//
// 修改标识：   
// 修改描述：   
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViData;
using JFB.Systems.Domain.Model;
using JFB.Systems.Domain.Info;


namespace JFB.Systems.Domain.Service
{
    /// <summary>
    /// OperatorService  领域层服务实现
    /// </summary>
    public class OperatorService : Repository<OperatorInfo, Operator>
    {
        /// <summary>
        /// 禁用管理员
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int DelOp(string ids, int type)
        {
            string[] arr = ids.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in arr)
            {
                Convert.ToInt32(s);
            }
            ids = ids.Trim(',');
            string sql = "update t_d_operator set isvalid = " + type + " where id in(" + ids + ")";
            int i = DataHelper.ExcuteNonQuery(sql);
            return i;
        }

        /// <summary>
        /// 重设密码（CMS）
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public int Resetpwd(string ids, string pwd)
        {
            string[] arr = ids.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in arr)
            {
                Convert.ToInt32(s);
            }
            ids = ids.Trim(',');
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("pwd", pwd);
            string sql = "update t_d_operator set userPass = @pwd where id in(" + ids + ")";
            int i = DataHelper.ExcuteNonQuery2(sql, dict);
            return i;
        }

        public OperatorInfo GetbyPwd(string username, string pwd)
        {
            var item = this.Get(a => a.Loginname == username && a.Userpass == pwd).FirstOrDefault();
            return item;
        }
    }
}
