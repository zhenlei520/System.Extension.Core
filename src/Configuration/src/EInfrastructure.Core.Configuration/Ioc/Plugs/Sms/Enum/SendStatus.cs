// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Enum
{
    /// <summary>
    /// 短信状态
    /// </summary>
    public enum SendStatus
    {
        /// <summary>
        /// 等待回执
        /// </summary>
        [Description("Await")] Await = 1,

        /// <summary>
        /// 发送失败
        /// </summary>
        [Description("Lose")] Lose = 2,

        /// <summary>
        /// 发送成功
        /// </summary>
        [Description("Success")] Success = 3
    }
}
