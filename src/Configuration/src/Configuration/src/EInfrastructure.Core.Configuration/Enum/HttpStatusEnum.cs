// Copyright (c) zhenlei520 All rights reserved.

using System.ComponentModel;

namespace EInfrastructure.Core.Configuration.Enum
{
    /// <summary>
    /// httpstatus
    /// </summary>
    public enum HttpStatusEnum
    {
        /// <summary>
        /// success
        /// </summary>
        [Description("成功")] Ok = 200,

        /// <summary>
        /// error
        /// </summary>
        [Description("错误")] Err = 201,

        /// <summary>
        /// unauthorized
        /// </summary>
        [Description("未授权")] Unauthorized = 401,

        /// <summary>
        /// nofind
        /// </summary>
        [Description("未发现信息")] NoFind = 404,

        /// <summary>
        /// illegal request
        /// </summary>
        [Description("请求非法")] IllegalRequest = 407,

        /// <summary>
        /// request limit
        /// </summary>
        [Description("访问受限")] RequestLimit = 408,

        /// <summary>
        /// system busy
        /// </summary>
        [Description("系统繁忙")] TimeOutException = 503
    }
}
