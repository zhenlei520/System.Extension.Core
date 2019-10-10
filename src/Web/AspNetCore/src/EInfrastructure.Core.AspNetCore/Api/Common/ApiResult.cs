using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore.Api.Common
{
    /// <summary>
    /// 正常响应信息
    /// </summary>
    public class ApiResult<T> where T : struct
    {
        /// <summary>
        ///
        /// </summary>
        public ApiResult()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code"></param>
        /// <param name="data"></param>
        public ApiResult(T code, object data)
        {
            Code = code;
            Data = data;
        }

        /// <summary>
        /// 响应码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public virtual T Code { get; set; }

        /// <summary>
        /// 响应信息
        /// </summary>
        [JsonProperty(PropertyName = "response")]
        public virtual object Data { get; set; } = new { };
    }
}
