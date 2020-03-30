using Newtonsoft.Json;

namespace EInfrastructure.Core.Configuration.Exception.Response
{
    /// <summary>
    /// 异常
    /// </summary>
    public class ExceptionResponse<T, T2>
    {
        /// <summary>
        ///
        /// </summary>
        public ExceptionResponse()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code">异常码</param>
        /// <param name="content">异常描述</param>
        public ExceptionResponse(T code, T2 content)
        {
            Code = code;
            Content = content;
        }


        /// <summary>
        /// 异常码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        internal T Code { get; set; }

        /// <summary>
        /// 异常响应内容
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        internal T2 Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{" + $"\"code\":{GetResult(Code)},\"content\":{GetResult(Content)}" + "}";
        }

        /// <summary>
        ///
        /// </summary>
        private bool Check<TResult>(TResult param)
        {
            return (param is int || param is short || param is byte || param is ushort || param is decimal ||
                    param is float ||
                    param is double);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="param"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        private string GetResult<TResult>(TResult param)
        {
            if (Check(param))
            {
                return param.ToString();
            }

            return $"\"{param?.ToString() ?? ""}\"";
        }
    }
}
