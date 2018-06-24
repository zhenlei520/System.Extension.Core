namespace EInfrastructure.Core.Key
{
    /// <summary>
    /// Http请求错误信息key
    /// </summary>
    public class HttpStatusMessageKey
    {
        /// <summary>
        /// 服务器异常
        /// </summary>
        public const string InternalServerError = "服务器链接异常，稍后再试吧";

        /// <summary>
        /// 请求超时
        /// </summary>
        public const string RequestTimeout = "请求超时";

        /// <summary>
        /// 授权失败
        /// </summary>
        public const string NoAuthorization = "授权失败";

        /// <summary>
        /// 数据库异常
        /// </summary>
        public const string DbEntityValidationException = "数据库查询异常";

        /// <summary>
        /// 请求禁止
        /// </summary>
        public const string RequestForbit = "请求禁止";

        /// <summary>
        /// 未知异常
        /// </summary>
        public const string UnknowException = "未知错误";
    }
}
