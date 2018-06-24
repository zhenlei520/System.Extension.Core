using System;
using System.Threading.Tasks;
using EInfrastructure.Core.Exception;
using EInfrastructure.Core.HelpCommon.Serialization;
using EInfrastructure.Core.Web.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EInfrastructure.Core.Web.Exception
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public static Action<System.Exception> ExceptionAction = null;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                ExceptionAction?.Invoke(ex);
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
            var data = new ApiErrResult() { Code = statusCode, Msg = msg };

            var result = new JsonCommon().Serializer(data);

            context.Response.ContentType = "application/json;charset=utf-8";

            return context.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder, Action<System.Exception> exceptionAction = null)
        {
            ErrorHandlingMiddleware.ExceptionAction = exceptionAction;
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}