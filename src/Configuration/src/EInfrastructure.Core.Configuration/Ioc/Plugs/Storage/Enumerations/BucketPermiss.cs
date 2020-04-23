// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations
{
    /// <summary>
    /// 空间访问权限
    /// </summary>
    public class BucketPermiss : Enumeration
    {
        /// <summary>
        /// 公开
        /// </summary>
        public static BucketPermiss Public = new BucketPermiss(0, "公开");

        /// <summary>
        /// 私有
        /// </summary>
        public static BucketPermiss Private = new BucketPermiss(1, "私有");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public BucketPermiss(int id, string name) : base(id, name)
        {
        }
    }
}
