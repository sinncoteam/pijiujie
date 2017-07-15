/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：OptionMap.cs
// 文件功能描述： 领域层实体映射Map
//
// 
// 创建标识：   dxk -- 2017/1/12 14:42:57 
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
    /// OptionMap  领域层实体映射Map
    /// </summary>
    public class OptionMap : DMClassMap<Option>
    {
        public OptionMap()
        {
            Table("t_d_option");
            Map(a => a.Keytype, "keytype");
            Map(a => a.Keytypename, "keytypename");
            Map(a => a.Opname, "opname");
            Map(a => a.Opvalue, "opvalue");
            Map(a => a.Opcomment, "opcomment");
            Map(a => a.Isvalid, "isvalid");
            Map(a => a.Ordernum, "ordernum");
        }
    }
}