/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：RoleModuleMap.cs
// 文件功能描述： 领域层实体映射Map
//
// 
// 创建标识：   dxk -- 2017/1/12 14:43:16 
//
// 修改标识：   
// 修改描述：   
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViData;

namespace JFB.Systems.Domain.Model
{
    /// <summary>
    /// RoleModuleMap  领域层实体映射Map
    /// </summary>
    public class RoleModuleMap : DMClassMap<RoleModule>
    {
        public RoleModuleMap()
        {
            Table("t_d_role_module");
            Id(a => a.Id, "id").Identity();
            Map(a => a.RoleCode, "role_code");
            Map(a => a.ModuleControlleraction, "module_controlleraction");
        }
    }
}