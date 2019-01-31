
using EInfrastructure.Core.Configuration.Key;

namespace EInfrastructure.Core.Exception
{
    /// <summary>
    /// 权限不足异常信息
    /// </summary>
    public class AuthException : System.Exception
    {
        /// <summary>
        /// 权限校验
        /// </summary>
        /// <param name="msg">提示信息</param>
        public AuthException(string msg = HttpStatusMessageKey.NoAuthorization)
           : base(msg)
        {

        }
    }
}
