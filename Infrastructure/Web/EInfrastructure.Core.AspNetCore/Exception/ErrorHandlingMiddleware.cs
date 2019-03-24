using System;
using System.Threading.Tasks;
using EInfrastructure.Core.AspNetCore.Api;
using EInfrastructure.Core.Exception;
using EInfrastructure.Core.HelpCommon.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EInfrastructure.Core.AspNetCore.Exception
{
    /// <summary>
    /// 错误异常
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 异常委托
        /// </summary>
        public static Func<HttpContext, System.Exception, bool> ExceptionAction = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                if (ExceptionAction != null)
                {
                    var isStop = ExceptionAction.Invoke(context, ex);
                    if (isStop)
                    {
                        return;
                    }
                }

                var statusCode = context.Response.StatusCode;
                string msg = "";

                if (ex is BusinessException)
                {
                    var data = new JsonCommon().Deserialize<dynamic>(ex.Message);
                    statusCode = data.code;
                    msg = data.content;
                }
                else if (ex is AuthException)
                {
                    statusCode = 401;
                    msg = ex.Message;
                }
                else
                {
                    msg = "未知错误";
                }

                await HandleExceptionAsync(context, statusCode, msg);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                var msg = "";
                if (statusCode == 401)
                {
                    msg = "未授权";
                }
                else if (statusCode == 404)
                {
                    msg = "未找到服务";
                }
                else if (statusCode == 502)
                {
                    msg = "请求错误";
                }
                else if (statusCode == 500)
                {
                    msg = "未知错误";
                }

                if (!string.IsNullOrWhiteSpace(msg) && statusCode != 401)
                {
                    await HandleExceptionAsync(context, statusCode, msg);
                }
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            var data = new ApiErrResult(statusCode, msg);

            var result = new JsonCommon().Serializer(data);

            context.Response.ContentType = "application/json;charset=utf-8";

            return context.Response.WriteAsync(result);
        }
    }

    /// <summary>
    /// 异常委托扩展
    /// </summary>
    public static class ErrorHandlingExtensions
    {
        /// <summary>
        /// 使用异常处理
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="exceptionAction">异常委托方法</param>
        /// <returns></returns>
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder,
            Func<HttpContext, System.Exception, bool> exceptionAction = null)
        {
            ErrorHandlingMiddleware.ExceptionAction = exceptionAction;
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}