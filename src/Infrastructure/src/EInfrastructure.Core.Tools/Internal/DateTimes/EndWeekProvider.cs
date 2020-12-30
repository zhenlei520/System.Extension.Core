// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.DateTimes
{
    /// <summary>
    /// 本周末
    /// </summary>
    public class EndWeekProvider : IdentifyDefault, IDateTimeProvider
    {
        /// <summary>
        ///
        /// </summary>
        public TimeType Type => TimeType.EndWeek;

        /// <summary>
        /// 得到本周末
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime GetResult(DateTime date)
        {
            int count = date.DayOfWeek - DayOfWeek.Sunday;
            if (count != 0) count = 7 - count;
            return new DateTime(date.Year, date.Month, date.Day).AddDays(count); //本周周日
        }

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTimeOffset GetResult(DateTimeOffset date)
        {
            int count = date.DayOfWeek - DayOfWeek.Sunday;
            if (count != 0) count = 7 - count;
            var dateTime = new DateTime(date.Year, date.Month, date.Day).AddDays(count); //本周周日
            return new DateTimeOffset(dateTime, date.Offset);
        }
    }
}
