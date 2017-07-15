/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：Option.cs
// 文件功能描述： 领域层实体定义(Model)
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

namespace JFB.Systems.Domain.Model
{
    /// <summary>
    /// Option   领域层实体定义(Model)
    /// </summary>
    public class Option
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Keytype { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Keytypename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Opname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Opvalue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Opcomment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Isvalid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Ordernum { get; set; }


    }
}