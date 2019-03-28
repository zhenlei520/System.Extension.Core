// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Interface.IOC
{
    /// <summary>
    /// Apo拦截器
    /// </summary>
    public interface IAopInterceptor
    {
        /// <summary>
        /// 在什么之前
        /// </summary>
        /// <param name="parameters">参数</param>
        void Before(object[] parameters);

        /// <summary>
        /// 在什么之后
        /// </summary>
        /// <param name="parameters">参数</param>
        void After(object[] parameters);
    }
}