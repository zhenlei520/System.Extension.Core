// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using EInfrastructure.Core.AspNetCore.Ioc;
using Microsoft.AspNetCore.Http;

namespace EInfrastructure.Core.AspNetCore
{
    /// <summary>
    ///
    /// </summary>
    public class SessionProvider : ISessionProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public SessionProvider(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        #region 清空Session

        /// <summary>
        /// 清空Session
        /// </summary>
        public void Clear()
        {
            this._httpContextAccessor.HttpContext.Session.Clear();
        }

        #endregion

        #region 移除Session

        /// <summary>
        /// 移除Session
        /// </summary>
        /// <param name="name">键</param>
        public void Remove(string name)
        {
            if (this._httpContextAccessor.HttpContext.Session.Keys.Any(x => x == name))
            {
                this._httpContextAccessor.HttpContext.Session.Remove(name);
            }
        }

        #endregion

        #region 得到Session

        /// <summary>
        /// 得到Session
        /// </summary>
        /// <param name="name">键</param>
        /// <returns></returns>
        public object Get(string name)
        {
            return this._httpContextAccessor.HttpContext.Session.GetString(name);
        }

        #endregion

        #region 设置Session

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        public void Set(string name, string value)
        {
            Remove(name);
            this._httpContextAccessor.HttpContext.Session.SetString(name, value);
        }

        #endregion
    }
}
