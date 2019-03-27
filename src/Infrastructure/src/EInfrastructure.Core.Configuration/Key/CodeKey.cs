namespace EInfrastructure.Core.Configuration.Key
{
    /// <summary>
    /// 错误码
    /// </summary>
    public class CodeKey
    {
        /// <summary>
        /// 重复请求
        /// </summary>
        public const int RepeatRequest = 101;

        /// <summary>
        /// 请求非法
        /// </summary>
        public const int Illegal = 110;

        /// <summary>
        /// 执行成功
        /// </summary>
        public const int Ok = 200;

        /// <summary>
        /// 业务异常，
        /// </summary>
        public const int Err = 201;

        /// <summary>
        /// 未授权，身份认证失败
        /// </summary>
        public const int NoAuthorization = 401;

        /// <summary>
        /// 请求被禁止
        /// </summary>
        public const int Forbid = 403;

        /// <summary>
        /// 请求网页或者方法未找到
        /// </summary>
        public const int NoFound = 404;

        /// <summary>
        /// 系统错误
        /// </summary>
        public const int SystemErr = 500;

        /// <summary>
        /// 询问框
        /// </summary>
        public const int Confirm = 501;

        /// <summary>
        /// 响应超时
        /// </summary>
        public const int Timeout = 504;

        /// <summary>
        /// 系统错误
        /// </summary>
        public const int System = 505;
    }
}