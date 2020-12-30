﻿// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Tools.Internal.SpecifiedTimeAfter
{
    /// <summary>
    /// 季度
    /// </summary>
    public class QuarterProvider : IdentifyDefault, ISpecifiedTimeAfterProvider
    {
        /// <summary>
        /// 类型
        /// </summary>
        public DurationType Type => DurationType.Quarter;

        /// <summary>
        /// 得到duration季后
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="duration">时长</param>
        /// <returns></returns>
        public DateTime GetResult(DateTime date, int duration)
        {
            return date.AddMonths(3 * duration);
        }

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="duration">时长</param>
        /// <returns></returns>
        public DateTimeOffset GetResult(DateTimeOffset date, int duration)
        {
            return date.AddMonths(3 * duration);
        }
    }
}
