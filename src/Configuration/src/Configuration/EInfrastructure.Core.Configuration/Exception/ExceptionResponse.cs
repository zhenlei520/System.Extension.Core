using Newtonsoft.Json;

namespace EInfrastructure.Core.Configuration.Exception
{
    /// <summary>
    /// 异常
    /// </summary>
    public class ExceptionResponse<T, T2>
    {
        /// <summary>
        /// 异常码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public T Code { get; set; }

        /// <summary>
        /// 异常响应内容
        /// </summary>
        public T2 Content { get; set; }
    }
}
