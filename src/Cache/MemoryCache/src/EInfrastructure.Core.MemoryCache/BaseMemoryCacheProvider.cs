// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.MemoryCache
{
    /// <summary>
    ///
    /// </summary>
    public class BaseMemoryCacheProvider
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="overdueStrategy">缓存过期策略</param>
        /// <param name="absoluteFunc">绝对过期方法</param>
        /// <param name="slidingFunc">滑动过期方法</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual T Execute<T>(OverdueStrategy overdueStrategy, Func<T> absoluteFunc, Func<T> slidingFunc)
        {
            if (overdueStrategy == null || overdueStrategy.Equals(OverdueStrategy.AbsoluteExpiration))
            {
                return absoluteFunc.Invoke();
            }

            return slidingFunc.Invoke();
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="overdueStrategy">缓存过期策略</param>
        /// <param name="absoluteFunc">绝对过期方法</param>
        /// <param name="slidingFunc">滑动过期方法</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual async Task<T> Execute<T>(OverdueStrategy overdueStrategy, Func<Task<T>> absoluteFunc, Func<Task<T>> slidingFunc)
        {
            if (overdueStrategy == null || overdueStrategy.Equals(OverdueStrategy.AbsoluteExpiration))
            {
                return await absoluteFunc.Invoke();
            }

            return await slidingFunc.Invoke();
        }
    }
}
