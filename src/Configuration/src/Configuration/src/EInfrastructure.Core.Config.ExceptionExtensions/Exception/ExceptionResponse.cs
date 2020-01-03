using Newtonsoft.Json;

namespace EInfrastructure.Core.Config.ExceptionExtensions.Exception
{
    /// <summary>
    /// 异常
    /// </summary>
    internal class ExceptionResponse<T, T2>
    {
        public ExceptionResponse()
        {
        }

        public ExceptionResponse(T code, T2 content)
        {
            Code = code;
            Content = content;
        }


        /// <summary>
        /// 异常码
        /// </summary>
        internal T Code { get; set; }

        /// <summary>
        /// 异常响应内容
        /// </summary>
        internal T2 Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{\"code\":{0},\"content\":{1}}", GetResult(Code), GetResult(Content));
        }

        /// <summary>
        ///
        /// </summary>
        private bool Check<TResult>(TResult param)
        {
            return (Code is int || Code is short || Code is byte || Code is ushort || Code is decimal ||
                    Code is float ||
                    Code is double);
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
