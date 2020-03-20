// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Reflection;

namespace EInfrastructure.Core.Tools.Component
{
    /// <summary>
    /// 服务提供者
    /// </summary>
    public interface IServiceProvider
    {
        /// <summary>
        /// 得到服务集合
        /// </summary>
        /// <param name="assblemyArray">应用程序集（如果为null则获取当前应用的程序集）</param>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>得到继承serviceType的实现类</returns>
        IEnumerable<TService> GetServices<TService>(Assembly[] assblemyArray = null) where TService : class;

        /// <summary>
        /// 得到服务
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns></returns>
        TService GetService<TService>() where TService : class;
    }
}
