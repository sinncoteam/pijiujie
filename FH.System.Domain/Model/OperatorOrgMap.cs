/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：OperatorOrgMap.cs
// 文件功能描述： 领域层实体映射Map
//
// 
// 创建标识：   dxk -- 2017/1/12 14:42:42 
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
    /// OperatorOrgMap  领域层实体映射Map
    /// </summary>
    public class OperatorOrgMap : DMClassMap<OperatorOrg>
    {
        public OperatorOrgMap()
        {
            Table("t_d_operator_org");
            Id(a => a.Id, "id").Identity();
            Map(a => a.OperatorId, "operator_id");
            Map(a => a.OrgCode, "org_code");
        }
    }
}