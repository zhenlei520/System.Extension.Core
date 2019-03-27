using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore.Api
{
    /// <summary>
    /// 正常响应信息
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 响应码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; } = 200;

        /// <summary>
        /// 响应信息
        /// </summary>
        [JsonProperty(PropertyName = "response")]
        public object Data { get; set; } = new { };
    }
}