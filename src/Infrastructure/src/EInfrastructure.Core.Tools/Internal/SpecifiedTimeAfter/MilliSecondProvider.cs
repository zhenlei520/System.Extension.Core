﻿// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools.Internal.SpecifiedTimeAfter
{
    /// <summary>
    /// 毫秒
    /// </summary>
    public class MilliSecondProvider:ISpecifiedTimeAfterProvider
    {
        /// <summary>
        /// 类型
        /// </summary>
        public DurationType Type=>DurationType.MilliSecond;

        /// <summary>
        /// 得到duration毫秒后
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="duration">时长</param>
        /// <returns></returns>
        public DateTime GetResult(DateTime date, int duration)
        {
            return date.AddMilliseconds(duration);
        }
    }
}
