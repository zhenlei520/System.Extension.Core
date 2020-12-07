// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Tools.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.DateTimes
{
    /// <summary>
    /// 本月月末
    /// </summary>
    public class EndMonthProvider : IDateTimeProvider
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
            return date.AddDays(1 - date.Day).AddMonths(1).AddDays(-1);
        }
    }
}
