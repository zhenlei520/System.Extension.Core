// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.DateTimes
{
    /// <summary>
    /// 本年初
    /// </summary>
    public class StartYearProvider : IdentifyDefault, IDateTimeProvider
    {
        /// <summary>
        /// 类型
        /// </summary>
        public TimeType Type => TimeType.StartYear;

        /// <summary>
        /// 得到本年初
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime GetResult(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTimeOffset GetResult(DateTimeOffset date)
        {
            var dateTime= new DateTime(date.Year, 1, 1);
            return new DateTimeOffset(dateTime, date.Offset);
        }
    }
}
