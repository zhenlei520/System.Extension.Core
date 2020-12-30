// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools.Enumerations;

namespace EInfrastructure.Core.Tools.Internal
{
    /// <summary>
    ///
    /// </summary>
    public interface IDateTimeProvider: ISingleInstance, IIdentify
    {
        /// <summary>
        /// 时间类型
        /// </summary>
        TimeType Type { get; }

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DateTime GetResult(DateTime date);

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DateTimeOffset GetResult(DateTimeOffset date);
    }
}
