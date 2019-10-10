using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore.Api.Common
{
    /// <summary>
    /// 异常响应信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiErrResult<T> where T : struct
    {
        /// <summary>
        /// 异常响应信息
        /// </summary>
        public ApiErrResult()
        {
        }

        /// <summary>
        /// 异常响应信息
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="msg">错误信息</param>
        public ApiErrResult(T code, string msg, object extend = null)
        {
            this.Msg = msg;
            this.Code = code;
            this.Extend = extend;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        public virtual string Msg { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public virtual T Code { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [JsonProperty(PropertyName = "extend", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object Extend { get; set; }
    }
}
