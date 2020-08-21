// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 过期策略
    /// </summary>
    public class OverdueStrategy : Enumeration
    {
        /// <summary>
        /// 绝对过期
        /// </summary>
        public static OverdueStrategy AbsoluteExpiration = new OverdueStrategy(1, "AbsoluteExpiration");

        /// <summary>
        /// 滑动过期
        /// </summary>
        public static OverdueStrategy SlidingExpiration = new OverdueStrategy(2, "SlidingExpiration");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public OverdueStrategy(int id, string name) : base(id, name)
        {
        }
    }
}
