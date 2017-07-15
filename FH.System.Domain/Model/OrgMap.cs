/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：OrgMap.cs
// 文件功能描述： 领域层实体映射Map
//
// 
// 创建标识：   dxk -- 2017/1/12 14:43:03 
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
    /// OrgMap  领域层实体映射Map
    /// </summary>
    public class OrgMap : DMClassMap<Org>
    {
        public OrgMap()
        {
            Table("t_d_org");
            Id(a => a.Id, "id").Identity();
            Map(a => a.Orgcode, "orgcode");
            Map(a => a.Orgname, "orgname");
            Map(a => a.ParentCode, "parent_code");
            Map(a => a.Level, "level");
            Map(a => a.Summary, "summary");
            Map(a => a.Createtime, "createtime");
            Map(a => a.Isvalid, "isvalid");
        }
    }
}