/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。  
//
// 文件名：RoleService.cs
// 文件功能描述： 领域层服务实现
//
// 
// 创建标识：   dxk -- 2017/1/12 14:43:09 
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
    /// RoleService  领域层服务实现
    /// </summary>
    public class RoleService : Repository<RoleInfo, Role>
    {
        /// <summary>
        /// 设置角色状态
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
            string sql = "update t_d_role set isvalid = " + type + " where id in(" + ids + ")";
            int i = DataHelper.ExcuteNonQuery(sql);
            return i;
        }
    }
}
