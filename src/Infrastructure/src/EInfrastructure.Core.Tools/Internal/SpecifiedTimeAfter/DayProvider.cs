// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Tools.Internal.SpecifiedTimeAfter
{
    /// <summary>
    /// 天
    /// </summary>
    public class DayProvider : IdentifyDefault, ISpecifiedTimeAfterProvider
    {
        /// <summary>
        /// 类型
        /// </summary>
        public DurationType Type => DurationType.Day;

        /// <summary>
        /// 得到duration天后
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="duration">时长</param>
        /// <returns></returns>
        public DateTime GetResult(DateTime date, int duration)
        {
            return date.AddDays(duration);
        }
    }
}
