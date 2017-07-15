/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：ModuleMenu.cs
// 文件功能描述： 领域层实体定义(Model)
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

namespace JFB.Systems.Domain.Model
{
    /// <summary>
    /// ModuleMenu   领域层实体定义(Model)
    /// </summary>
    public class ModuleMenu
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string Modulename { get; set; }

        /// <summary>
        /// 控制器代码
        /// </summary>
        public virtual string Controlleraction { get; set; }

        /// <summary>
        /// 上级模块代码
        /// </summary>
        public virtual string ParentControlleraction { get; set; }

        /// <summary>
        /// 是否菜单
        /// </summary>
        public virtual int Ismenu { get; set; }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        public virtual int Isleaf { get; set; }

        /// <summary>
        /// CSS
        /// </summary>
        public virtual string Css { get; set; }

        /// <summary>
        /// 排序，倒序
        /// </summary>
        public virtual int Orderbys { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual int Isvalid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime Createtime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Summary { get; set; }


    }
}