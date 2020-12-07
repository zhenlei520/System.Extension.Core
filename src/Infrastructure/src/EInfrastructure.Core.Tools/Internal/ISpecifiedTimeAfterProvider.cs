// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools.Internal
{
    /// <summary>
    /// 得到指定时间后
    /// </summary>
    public interface ISpecifiedTimeAfterProvider
    {
        /// <summary>
        /// 时间类型
        /// </summary>
        DurationType Type { get; }

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <param name="date">时间</param>
        /// <param name="duration">时长</param>
        /// <returns></returns>
        public DateTime GetResult(DateTime date, int duration);
    }
}
