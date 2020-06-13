// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations
{
    /// <summary>
    /// 访问权限
    /// </summary>
    public class Permiss : Enumeration
    {
        /// <summary>
        /// 公开读
        /// </summary>
        public static Permiss Public = new Permiss(0, "公开读");

        /// <summary>
        /// 私有
        /// </summary>
        public static Permiss Private = new Permiss(1, "私有");

        /// <summary>
        /// 公共读写
        /// </summary>
        public static Permiss PublicReadWrite = new Permiss(2, "公共读写");

        /// <summary>
        /// 默认
        /// 阿里云这里为继承父类权限，其他云不支持
        /// </summary>
        public static Permiss Default = new Permiss(3, "默认");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Permiss(int id, string name) : base(id, name)
        {
        }
    }
}
