// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Tools.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.DateTimes
{
    /// <summary>
    /// 月初
    /// </summary>
    public class StartMonthProvider : IDateTimeProvider
    {
        /// <summary>
        /// 类型
        /// </summary>
        public TimeType Type =>TimeType.StartMonth;

        /// <summary>
        /// 得到本月初
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime GetResult(DateTime date)
        {
            return date.AddDays(1 - date.Day); //本月月初
        }
    }
}
