using System.Linq;
using EInfrastructure.Core.Configuration.Key;
using EInfrastructure.Core.QiNiu.Storage.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
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
    internal class ClaimQiNiuRequirementFilter : IAuthorizationFilter
    {
        private readonly QiNiuConfig _qiNiuConfig;

        internal ClaimQiNiuRequirementFilter(QiNiuConfig qiniuConfig)
        {
            this._qiNiuConfig = qiniuConfig;
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