// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Config.EnumerationExtensions.SeedWork;

namespace EInfrastructure.Core.Config.EnumerationExtensions
{
    /// <summary>
    /// 性别
    /// </summary>
    public class Gender : Enumeration
    {
        /// <summary>
        /// 未知
        /// </summary>
        public static Gender Unknow = new Gender(0, "未知");

        /// <summary>
        /// 男
        /// </summary>
        public static Gender Boy = new Gender(1, "男");

        /// <summary>
        /// 女
        /// </summary>
        public static Gender Girl = new Gender(2, "女");

        public Gender(int id, string name) : base(id, name)
        {
        }
    }
}
