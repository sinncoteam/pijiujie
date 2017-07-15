/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。  
//
// 文件名：OperatorRoleService.cs
// 文件功能描述： 领域层服务实现
//
// 
// 创建标识：   dxk -- 2017/1/12 14:42:49 
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
    /// OperatorRoleService  领域层服务实现
    /// </summary>
    public class OperatorRoleService : Repository<OperatorRoleInfo, OperatorRole>
    {
        /// <summary>
        /// 更新用户角色
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public int UpdateRoleModule(int operatorId, string roles)
        {
            int o = this.Delete(a => a.OperatorId == operatorId);
            if (!string.IsNullOrEmpty(roles))
            {
                string[] moduleArr = roles.Split(',');
                StringBuilder sb = new StringBuilder();
                foreach (string mid in moduleArr)
                {
                    sb.Append("insert into t_d_operator_role(operator_id, role_code) values(" + operatorId + ",'" + mid + "'); ");
                }
                o = DataHelper.ExcuteNonQuery(sb.ToString());
            }
            return o;
        }
    }
}
