/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：RoleMap.cs
// 文件功能描述： 领域层实体映射Map
//
// 
// 创建标识：   dxk -- 2017/1/12 14:43:09 
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
    /// RoleMap  领域层实体映射Map
    /// </summary>
    public class RoleMap : DMClassMap<Role>
    {
        public RoleMap()
        {
            Table("t_d_role");
            Id(a => a.Id, "id").Identity();
            Map(a => a.Rolecode, "rolecode");
            Map(a => a.Rolename, "rolename");
            Map(a => a.Summary, "summary");
            Map(a => a.Issuper, "issuper");
            Map(a => a.Createtime, "createtime");
            Map(a => a.Isvalid, "isvalid");
        }
    }
}