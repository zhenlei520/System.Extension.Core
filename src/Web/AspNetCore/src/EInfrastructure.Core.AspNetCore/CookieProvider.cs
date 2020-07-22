// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using EInfrastructure.Core.AspNetCore.Ioc;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Systems;
using Microsoft.AspNetCore.Http;

namespace EInfrastructure.Core.AspNetCore
{
    /// <summary>
    ///
    /// </summary>
    public class CookieProvider : ICookieProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public CookieProvider(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        #region 清空cookie

        /// <summary>
        /// 清空cookie
        /// </summary>
        public void Clear()
        {
            foreach (var name in _httpContextAccessor.HttpContext.Request.Cookies.Keys)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(name);
            }
        }

        #endregion

        #region 移除指定Cookie

        /// <summary>
        /// 移除指定Cookie
        /// </summary>
        /// <param name="name">键</param>
        public void Remove(string name)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.Keys.Any(x => x == name))
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(name);
            }
        }

        #endregion

        #region 得到Cookie

        /// <summary>
        /// 得到Cookie
        /// </summary>
        /// <param name="name">键</param>
        /// <returns></returns>
        public string Get(string name)
        {
            _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(name, out string cookie);
            return cookie.SafeString();
        }

        #endregion

        #region 设置cookie

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <param name="cookieOptions"></param>
        public void Set(string name, string value, CookieOptions cookieOptions)
        {
            Remove(name);
            if (cookieOptions != null)
            {
                this._httpContextAccessor.HttpContext.Response.Cookies.Append(name, value, cookieOptions);
            }
            else
            {
                this._httpContextAccessor.HttpContext.Response.Cookies.Append(name, value);
            }
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <param name="timeType">时间类型</param>
        /// <param name="duration">时长</param>
        public void Set(string name, string value, DurationType timeType, int duration)
        {
            Remove(name);
            this._httpContextAccessor.HttpContext.Response.Cookies.Append(name, value, new CookieOptions()
            {
                Expires = TimeCommon.GetScheduleTime(timeType, duration),
            });
        }

        #endregion
    }
}
