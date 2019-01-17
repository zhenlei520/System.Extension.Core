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
    public class QiNiuAuthAttribute : TypeFilterAttribute
    {
        public QiNiuAuthAttribute() : base(typeof(ClaimQiNiuRequirementFilter))
        {
        }
    }

    public class ClaimQiNiuRequirementFilter : IAuthorizationFilter
    {
        private readonly IOptionsSnapshot<QiNiuConfig> _qiNiuConfig;

        public ClaimQiNiuRequirementFilter(IOptionsSnapshot<QiNiuConfig> qiniuConfig)
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

            string callbackUrl = _qiNiuConfig.Value.CallbackAuthHost + context.HttpContext.Request.Path.Value;
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