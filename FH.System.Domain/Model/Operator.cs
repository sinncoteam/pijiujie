/*----------------------------------------------------------------
// Copyright (C) 2016 联康洪 版权所有。 
//
// 文件名：Operator.cs
// 文件功能描述： 领域层实体定义(Model)
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

namespace JFB.Systems.Domain.Model
{
    /// <summary>
    /// Operator   领域层实体定义(Model)
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string Username { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public virtual string Loginname { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Userpass { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime Createtime { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public virtual DateTime Lastlogintime { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual int Isvalid { get; set; }


    }
}