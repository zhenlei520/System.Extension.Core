// Copyright (c) zhenlei520 All rights reserved.

namespace EInfrastructure.Core.Configuration.Enumeration
{
    /// <summary>
    /// 时间类型
    /// </summary>
    public class TimeType : SeedWork.Enumeration
    {
        /// <summary>
        /// 月初
        /// </summary>
        public static TimeType StartMonth = new TimeType(1, "月初");

        /// <summary>
        /// 月末
        /// </summary>
        public static TimeType EndMonth = new TimeType(2, "月末");

        /// <summary>
        /// 本周周一
        /// </summary>
        public static TimeType StartWeek = new TimeType(3, "本周周一");

        /// <summary>
        /// 本周周日
        /// </summary>
        public static TimeType EndWeek = new TimeType(4, "本周周日");

        /// <summary>
        /// 本季初
        /// </summary>
        public static TimeType StartQuarter = new TimeType(5, "本季初");

        /// <summary>
        /// 本季末
        /// </summary>
        public static TimeType EndQuarter = new TimeType(6, "本季末");

        /// <summary>
        /// 年初
        /// </summary>
        public static TimeType StartYear = new TimeType(7, "年初");

        /// <summary>
        /// 年末
        /// </summary>
        public static TimeType EndYear = new TimeType(8, "年末");

        public TimeType(int id, string name) : base(id, name)
        {
        }
    }
}
