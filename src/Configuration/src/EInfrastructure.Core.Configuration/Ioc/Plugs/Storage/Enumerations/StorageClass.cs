// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations
{
    /// <summary>
    /// 存储类型
    /// 标准存储
    /// 低频访问
    /// 归档存储
    /// </summary>
    public class StorageClass : Enumeration
    {
        /// <summary>
        /// 标准存储
        /// </summary>
        public static StorageClass Standard = new StorageClass(1, "标准存储");

        /// <summary>
        /// 低频存储
        /// </summary>
        public static StorageClass IA = new StorageClass(2, "低频存储");

        /// <summary>
        /// 归档存储
        /// </summary>
        public static StorageClass Archive = new StorageClass(3, "归档存储");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public StorageClass(int id, string name) : base(id, name)
        {
        }
    }
}
