using System;
using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore.Api.Common
{
    /// <summary>
    /// 异常响应信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ApiErrResult<T>
    {
        /// <summary>
        /// 异常响应信息
        /// </summary>
        public ApiErrResult()
        {
            this.CurrentTime = DateTime.Now;
        }

        /// <summary>
        /// 异常响应信息
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="msg">错误信息</param>
        /// <param name="extend">扩展信息</param>
        public ApiErrResult(T code, string msg, object extend = null)
        {
            this.Msg = msg;
            this.Code = code;
            this.Extend = extend;
            this.CurrentTime = DateTime.Now;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="msg">错误信息</param>
        /// <param name="extend">扩展信息</param>
        /// <param name="isReturnCurrentTime">是否返回方式时间</param>
        public ApiErrResult(T code, string msg, object extend, bool isReturnCurrentTime)
        {
            this.Msg = msg;
            this.Code = code;
            this.Extend = extend;
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

        /// <summary>
        /// 当前时间
        /// </summary>
        [JsonProperty(PropertyName = "current_time", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual DateTime? CurrentTime { get; set; }
    }
}
