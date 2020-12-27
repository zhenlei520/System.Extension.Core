// Copyright (c) zhenlei520 All rights reserved.

namespace EInfrastructure.Core.Tools.Configuration
{
    /// <summary>
    /// 节假日信息
    /// </summary>
    public class WeekHolidayStruct
    {
        /// <summary>
        /// 月
        /// </summary>
        public readonly int Month;

        /// <summary>
        /// 这个月第几周
        /// </summary>
        public readonly int WeekAtMonth;

        /// <summary>
        /// 周N，按照中国时间算，周一到周日为一周
        /// 周一：1，周二：2，周三：3，周四：4，周五：5，周六：6,周一：7
        /// </summary>
        public readonly int WeekDay;

        /// <summary>
        /// 假日名
        /// </summary>
        public readonly string HolidayName;

        /// <summary>
        /// 节假日信息
        /// </summary>
        /// <param name="month"></param>
        /// <param name="weekAtMonth"></param>
        /// <param name="weekDay"></param>
        /// <param name="name"></param>
        public WeekHolidayStruct(int month, int weekAtMonth, int weekDay, string name)
        {
            Month = month;
            WeekAtMonth = weekAtMonth;
            WeekDay = weekDay;
            HolidayName = name;
        }
    }
}
