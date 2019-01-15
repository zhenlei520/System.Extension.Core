using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore.Api
{
    /// <summary>
    /// 正常响应信息
    /// </summary>
    public class ApiResult
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; } = 200;

        [JsonProperty(PropertyName = "response")]
        public object Data { get; set; } = new { };
    }
}