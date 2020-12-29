// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.DateTimes
{
    /// <summary>
    /// 本月月末
    /// </summary>
    public class EndMonthProvider : IdentifyDefault, IDateTimeProvider
    {
        /// <summary>
        /// 时间类型
        /// </summary>
        public TimeType Type => TimeType.EndMonth;

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime GetResult(DateTime date)
        {
            var lastDay = GlobalConfigurations.Calendar.GetDaysInMonth(date.Year, date.Month);
            return new DateTime(date.Year, date.Month, lastDay);
        }

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTimeOffset GetResult(DateTimeOffset date)
        {
            var lastDay = GlobalConfigurations.Calendar.GetDaysInMonth(date.Year, date.Month);
            DateTime dateTime = new DateTime(date.Year, date.Month, lastDay);
            return new DateTimeOffset(dateTime, date.Offset);
        }
    }
}
