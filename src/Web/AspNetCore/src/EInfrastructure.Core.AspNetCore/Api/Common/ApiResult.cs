using System;
using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore.Api.Common
{
    /// <summary>
    /// 正常响应信息
    /// </summary>
    public abstract class ApiResult<T> where T : struct
    {
        /// <summary>
        ///
        /// </summary>
        public ApiResult()
        {
            CurrentTime = DateTime.Now;
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
            CurrentTime = DateTime.Now;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="data">数据</param>
        /// <param name="isReturnCurrentTime">是否返回方式时间</param>
        public ApiResult(T code, object data, bool isReturnCurrentTime)
        {
            Code = code;
            Data = data;
            if (isReturnCurrentTime)
            {
                CurrentTime = DateTime.Now;
            }
            else
            {
                CurrentTime = null;
            }
        }

        /// <summary>
        /// 响应码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public abstract T Code { get; set; }

        /// <summary>
        /// 响应信息
        /// </summary>
        [JsonProperty(PropertyName = "response")]
        public abstract object Data { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        [JsonProperty(PropertyName = "current_time", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public abstract DateTime? CurrentTime { get; set; }
    }
}
