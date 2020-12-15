// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.DateTimes
{
    /// <summary>
    /// 本季末
    /// </summary>
    public class EndQuarterProvider : IdentifyDefault, IDateTimeProvider
    {
        /// <summary>
        /// 时间类型
        /// </summary>
        public TimeType Type => TimeType.EndQuarter;

        /// <summary>
        /// 得到本季末时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime GetResult(DateTime date)
        {
            return date.AddMonths(0 - (date.Month - 1) % 3).AddDays(1 - date.Day).AddMonths(3)
                .AddDays(-1);
        }
    }
}
