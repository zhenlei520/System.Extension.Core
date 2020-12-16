// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 时间单位
    /// </summary>
    public class TimeUnit : Enumeration
    {
        /// <summary>
        /// 刻度
        /// </summary>
        public static TimeUnit Ticks = new TimeUnit(1, "Ticks");

        /// <summary>
        /// 毫秒
        /// </summary>
        public static TimeUnit MilliSecond = new TimeUnit(2, "MilliSecond");

        /// <summary>
        /// 秒
        /// </summary>
        public static TimeUnit Second = new TimeUnit(3, "Second");

        /// <summary>
        /// 分钟
        /// </summary>
        public static TimeUnit Minutes = new TimeUnit(4, "Minutes");

        /// <summary>
        /// 小时
        /// </summary>
        public static TimeUnit Hours = new TimeUnit(5, "Hours");

        /// <summary>
        /// 天
        /// </summary>
        public static TimeUnit Days = new TimeUnit(6, "Days");

        /// <summary>
        /// 周
        /// </summary>
        public static TimeUnit Weeks = new TimeUnit(7, "Weeks");

        /// <summary>
        /// 月
        /// </summary>
        public static TimeUnit Month = new TimeUnit(8, "Month");

        /// <summary>
        /// 季度
        /// </summary>
        public static TimeUnit Quarter = new TimeUnit(9, "Quarter");

        /// <summary>
        /// 年
        /// </summary>
        public static TimeUnit Year = new TimeUnit(10, "Year");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">描述</param>
        public TimeUnit(int id, string name) : base(id, name)
        {
        }
    }
}
