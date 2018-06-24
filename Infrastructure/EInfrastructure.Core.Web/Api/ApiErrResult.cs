using Newtonsoft.Json;

namespace EInfrastructure.Core.Web.Api
{
    /// <summary>
    /// 异常响应信息
    /// </summary>
    public class ApiErrResult
    {
        public ApiErrResult()
        {
        }

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
    }
}
