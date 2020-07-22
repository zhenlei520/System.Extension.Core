// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc;
using Microsoft.AspNetCore.Http;

namespace EInfrastructure.Core.AspNetCore.Ioc
{
    /// <summary>
    ///
    /// </summary>
    public interface ICookieProvider : IPerRequest
    {
        /// <summary>
        /// 清空cookie
        /// </summary>
        void Clear();

        /// <summary>
        /// 移除指定Cookie
        /// </summary>
        /// <param name="name">键</param>
        void Remove(string name);

        /// <summary>
        /// 得到Cookie
        /// </summary>
        /// <param name="name">键</param>
        /// <returns></returns>
        string Get(string name);

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <param name="cookieOptions"></param>
        void Set(string name, string value, CookieOptions cookieOptions);

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <param name="timeType">时间类型</param>
        /// <param name="duration">时长</param>
        void Set(string name, string value, DurationType timeType, int duration);
    }
}
