// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 区间模式
    /// </summary>
    public class RangeMode : Enumeration
    {
        /// <summary>
        /// 开区间
        /// </summary>
        public static RangeMode Open = new RangeMode(1, "Open");

        /// <summary>
        /// 闭区间
        /// </summary>
        public static RangeMode Close = new RangeMode(2, "Close");

        /// <summary>
        /// 左开右闭
        /// </summary>
        public static RangeMode OpenClose = new RangeMode(3, "OpenClose");

        /// <summary>
        /// 左闭右开
        /// </summary>
        public static RangeMode CloseOpen = new RangeMode(4, "CloseOpen");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public RangeMode(int id, string name) : base(id, name)
        {
        }
    }
}
