/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：Role.cs
// 文件功能描述： 领域层实体定义(Model)
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

namespace JFB.Systems.Domain.Model
{
    /// <summary>
    /// Role   领域层实体定义(Model)
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 角色代码
        /// </summary>
        public virtual string Rolecode { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual string Rolename { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Summary { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public virtual int Issuper { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime Createtime { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual int Isvalid { get; set; }


    }
}