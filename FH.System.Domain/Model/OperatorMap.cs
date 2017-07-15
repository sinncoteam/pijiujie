/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：OperatorMap.cs
// 文件功能描述： 领域层实体映射Map
//
// 
// 创建标识：   dxk -- 2017/1/12 14:42:35 
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
    /// OperatorMap  领域层实体映射Map
    /// </summary>
    public class OperatorMap : DMClassMap<Operator>
    {
        public OperatorMap()
        {
            Table("t_d_operator");
            Id(a => a.Id, "id").Identity();
            Map(a => a.Username, "username");
            Map(a => a.Loginname, "loginname");
            Map(a => a.Userpass, "userpass");
            Map(a => a.Createtime, "createtime");
            Map(a => a.Lastlogintime, "lastlogintime");
            Map(a => a.Isvalid, "isvalid");
        }
    }
}