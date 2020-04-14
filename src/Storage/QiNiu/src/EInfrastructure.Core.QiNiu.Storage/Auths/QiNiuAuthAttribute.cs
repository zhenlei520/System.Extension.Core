// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Qiniu.Util;

namespace EInfrastructure.Core.QiNiu.Storage.Auths
{
    /// <summary>
    /// 七牛回调鉴权
    /// </summary>
    public class QiNiuAuthAttribute : TypeFilterAttribute
    {
        /// <summary>
        ///
        /// </summary>
        public QiNiuAuthAttribute() : base(typeof(ClaimQiNiuRequirementFilter))
        {
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class ClaimQiNiuRequirementFilter : IAuthorizationFilter
    {
        private readonly QiNiuStorageConfig _qiNiuConfig;

        /// <summary>
        ///
        /// </summary>
        /// <param name="qiNiuConfig"></param>
        public ClaimQiNiuRequirementFilter(QiNiuStorageConfig qiNiuConfig)
        {
            _qiNiuConfig = qiNiuConfig;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
                return;
            string qiNiuAuthorization = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(qiNiuAuthorization))
            {
                AuthLose(context);
                return;
            }

            string callbackUrl = _qiNiuConfig.CallbackAuthHost + context.HttpContext.Request.Path.Value;

            byte[] body = null;
            if (_qiNiuConfig.CallbackBodyType == CallbackBodyType.Urlencoded.Id)
            {
                body = GetData(context.HttpContext.Request.Form
                    .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                    .ToList()).ConvertToByteArray();
            }

            string authorization =
                new Auth(_qiNiuConfig.GetMac()).CreateManageToken(callbackUrl, body);

            if (authorization != qiNiuAuthorization)
            {
                AuthLose(context);
            }
        }

        /// <summary>
        /// 鉴权失败
        /// </summary>
        /// <param name="context"></param>
        private void AuthLose(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.StatusCode = HttpStatus.Unauthorized.Id;
            context.Result =
                new JsonResult(new
                {
                    Code = HttpStatus.Unauthorized.Id,
                    Msg = "鉴权失败"
                });
        }

        #region 根据KeyValuePair集合得到对象

        /// <summary>
        /// 根据KeyValuePair集合得到对象
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        private object GetData(List<KeyValuePair<string, string>> keyValuePairs)
        {
            dynamic data = new System.Dynamic.ExpandoObject();
            foreach (var item in keyValuePairs)
            {
                ((IDictionary<string, object>) data).Add(item.Key, item.Value);
            }

            return data;
        }

        #endregion
    }
}
