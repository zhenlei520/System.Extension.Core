// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.DateTimes
{
    /// <summary>
    ///  本年末
    /// </summary>
    public class EndYearProvider : IdentifyDefault, IDateTimeProvider
    {
        /// <summary>
        /// 类型
        /// </summary>
        public TimeType Type =>TimeType.EndYear;

        /// <summary>
        /// 得到本年末
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime GetResult(DateTime date)
        {
            return new DateTime(date.Year, 12, 31); //本年年末
        }

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTimeOffset GetResult(DateTimeOffset date)
        {
            var dateTime =  new DateTime(date.Year, 12, 31); //本年年末
            return new DateTimeOffset(dateTime, date.Offset);
        }
    }
}
