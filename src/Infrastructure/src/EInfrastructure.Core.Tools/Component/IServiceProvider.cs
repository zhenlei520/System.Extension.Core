// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace EInfrastructure.Core.Tools.Component
{
    /// <summary>
    /// 服务提供者
    /// </summary>
    public interface IServiceProvider
    {
        /// <summary>
        /// 得到服务
        /// </summary>
        /// <param name="serviceType">服务类</param>
        /// <returns></returns>
        IEnumerable<object> GetService(Type serviceType);

        /// <summary>
        /// 得到服务
        /// </summary>
        /// <returns></returns>
        IEnumerable<TService> GetService<TService>();
    }
}
