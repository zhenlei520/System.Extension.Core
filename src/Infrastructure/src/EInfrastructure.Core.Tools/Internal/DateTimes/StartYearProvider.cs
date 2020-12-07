// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Tools.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.DateTimes
{
    /// <summary>
    /// 本年初
    /// </summary>
    public class StartYearProvider : IDateTimeProvider
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
    }
}
