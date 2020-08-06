// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.AspNetCore.Ioc
{
    /// <summary>
    ///
    /// </summary>
    public interface ISessionProvider : IPerRequest
    {
        /// <summary>
        /// 清空Session
        /// </summary>
        void Clear();

        /// <summary>
        /// 移除Session
        /// </summary>
        /// <param name="name">键</param>
        void Remove(string name);

        /// <summary>
        /// 得到Session
        /// </summary>
        /// <param name="name">键</param>
        /// <returns></returns>
        object Get(string name);

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        void Set(string name, string value);
    }
}
