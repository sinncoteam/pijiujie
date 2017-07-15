/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。  
//
// 文件名：RoleModuleService.cs
// 文件功能描述： 领域层服务实现
//
// 
// 创建标识：   dxk -- 2017/1/12 14:43:16 
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
    /// RoleModuleService  领域层服务实现
    /// </summary>
    public class RoleModuleService : Repository<RoleModuleInfo, RoleModule>
    {

        /// <summary>
        /// 更新用户权限
        /// </summary>
        /// <param name="rolecode"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public int UpdateRoleModule(string rolecode, string modules)
        {
            int o = this.Delete(a => a.RoleCode == rolecode);
            if (!string.IsNullOrEmpty(modules))
            {
                string[] moduleArr = modules.Split(',');
                StringBuilder sb = new StringBuilder();
                foreach (string mid in moduleArr)
                {
                    sb.Append("insert into t_d_role_module(role_code, module_controlleraction) values('" + rolecode + "','" + mid + "'); ");
                }
                o = DataHelper.ExcuteNonQuery(sb.ToString());
            }
            return o;
        }
    }
}
