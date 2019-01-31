using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore.Api
{
    /// <summary>
    /// 异常响应信息
    /// </summary>
    public class ApiErrResult
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
        public ApiErrResult(int code, string msg)
        {
            this.Msg = msg;
            this.Code = code;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        public string Msg { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }
        
        /// <summary>
        /// 扩展信息
        /// </summary>
        [JsonProperty(PropertyName = "extend")]
        public object Extend { get; set; }
    }
}
