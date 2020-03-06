using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception.Response;

namespace EInfrastructure.Core.Configuration.Exception
{
    /// <inheritdoc />
    /// <summary>
    /// 业务异常,可以将Exception消息直接返回给用户
    /// </summary>
    public class BusinessException : BusinessException<int>
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="content">异常详情</param>
        public BusinessException(string content, int? code = null) :
            base(content, code ?? HttpStatus.Err.Id)
        {
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessException<T> : BusinessException<T, string>
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="content">异常详情</param>
        public BusinessException(string content, T code = default(T)) :
            base(content, code)
        {
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class BusinessException<T, T2> : System.Exception
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="content">异常详情</param>
        public BusinessException(T2 content, T code = default(T)) :
            base(new ExceptionResponse<T, T2>(code,content).ToString())
        {
        }
    }
}
