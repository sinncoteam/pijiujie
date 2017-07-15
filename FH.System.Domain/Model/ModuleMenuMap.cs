/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：ModuleMenuMap.cs
// 文件功能描述： 领域层实体映射Map
//
// 
// 创建标识：   dxk -- 2017/1/12 14:42:27 
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
    /// ModuleMenuMap  领域层实体映射Map
    /// </summary>
    public class ModuleMenuMap : DMClassMap<ModuleMenu>
    {
        public ModuleMenuMap()
        {
            Table("t_d_module_menu");
            Id(a => a.Id, "id").Identity();
            Map(a => a.Modulename, "modulename");
            Map(a => a.Controlleraction, "controlleraction");
            Map(a => a.ParentControlleraction, "parent_controlleraction");
            Map(a => a.Ismenu, "ismenu");
            Map(a => a.Isleaf, "isleaf");
            Map(a => a.Css, "CSS");
            Map(a => a.Orderbys, "orderbys");
            Map(a => a.Isvalid, "isvalid");
            Map(a => a.Createtime, "createtime");
            Map(a => a.Summary, "summary");
        }
    }
}