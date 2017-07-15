/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：RoleModule.cs
// 文件功能描述： 领域层实体定义(Model)
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

namespace JFB.Systems.Domain.Model
{
    /// <summary>
    /// RoleModule   领域层实体定义(Model)
    /// </summary>
    public class RoleModule
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 角色代码
        /// </summary>
        public virtual string RoleCode { get; set; }

        /// <summary>
        /// 功能模块代码
        /// </summary>
        public virtual string ModuleControlleraction { get; set; }


    }
}