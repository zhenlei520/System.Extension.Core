// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.DateTimes
{
    /// <summary>
    /// 本周周一
    /// </summary>
    public class StartWeekProvider : IdentifyDefault, IDateTimeProvider
    {
        /// <summary>
        /// 类型
        /// </summary>
        public TimeType Type => TimeType.StartWeek;

        /// <summary>
        /// 得到本周周一
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime GetResult(DateTime date)
        {
            int count = date.DayOfWeek - DayOfWeek.Monday;
            if (count == -1) count = 6;
            return new DateTime(date.Year, date.Month, date.Day).AddDays(-count);
        }
    }
}
