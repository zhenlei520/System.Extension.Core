// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using EInfrastructure.Core.Configuration.Key;
using EInfrastructure.Core.QiNiu.Storage.Config;
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

        public ClaimQiNiuRequirementFilter(QiNiuStorageConfig qiNiuConfig)
        {
            _qiNiuConfig = qiNiuConfig;
        }

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
            string authorization = new Auth(new BaseStorageProvider(_qiNiuConfig).Mac).CreateManageToken(callbackUrl);

            if (authorization != qiNiuAuthorization)
            {
                AuthLose(context);
                return;
            }
        }

        /// <summary>
        /// 鉴权失败
        /// </summary>
        /// <param name="context"></param>
        private void AuthLose(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.StatusCode = (int) CodeKey.NoAuthorization;
            context.Result =
                new JsonResult(new
                {
                    Code = (int) CodeKey.NoAuthorization,
                    Msg = "鉴权失败"
                });
        }
    }
}