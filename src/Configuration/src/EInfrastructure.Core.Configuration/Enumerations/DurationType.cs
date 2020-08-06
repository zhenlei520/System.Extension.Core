// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 时间类型
    /// </summary>
    public class DurationType : Enumeration
    {
        /// <summary>
        /// 毫秒
        /// </summary>
        public static DurationType MilliSecond = new DurationType(1, "毫秒");

        /// <summary>
        /// 秒
        /// </summary>
        public static DurationType Second = new DurationType(2, "秒");

        /// <summary>
        /// 分钟
        /// </summary>
        public static DurationType Minutes = new DurationType(3, "分钟");

        /// <summary>
        /// 小时
        /// </summary>
        public static DurationType Hour = new DurationType(4, "小时");

        /// <summary>
        /// 天
        /// </summary>
        public static DurationType Day = new DurationType(5, "天");

        /// <summary>
        /// 周
        /// </summary>
        public static DurationType Weeks = new DurationType(6, "周");

        /// <summary>
        /// 月
        /// </summary>
        public static DurationType Month = new DurationType(7, "月");

        /// <summary>
        /// 季
        /// </summary>
        public static DurationType Quarter = new DurationType(8, "季");

        /// <summary>
        /// 年
        /// </summary>
        public static DurationType Year = new DurationType(9, "年");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public DurationType(int id, string name) : base(id, name)
        {
        }
    }
}
