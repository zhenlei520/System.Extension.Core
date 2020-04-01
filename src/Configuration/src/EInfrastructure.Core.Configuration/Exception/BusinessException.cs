using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception.Response;

namespace EInfrastructure.Core.Configuration.Exception
{
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
        public BusinessException(string content) :
            base(content, HttpStatus.Err.Id)
        {
        }

        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="content">异常详情</param>
        /// <param name="extend">扩展信息</param>
        public BusinessException(string content, int? code = null, object extend = null) :
            base(content, code ?? HttpStatus.Err.Id, extend)
        {
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessException<T> : System.Exception
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="content">异常详情</param>
        public BusinessException(string content, T code = default) :
            base(new ExceptionResponse<T>(code, content).ToString())
        {
        }

        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="content">异常详情</param>
        /// <param name="extend">扩展信息</param>
        public BusinessException(string content, T code = default(T), object extend = null) :
            base(new ExceptionResponse<T>(code, content, extend).ToString())
        {
        }
    }
}
