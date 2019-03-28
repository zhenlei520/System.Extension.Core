// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.WeChat.Enum
{
    /// <summary>
    /// 微信类型
    /// </summary>
    public enum WebChatTypeEnum
    {
        /// <summary>
        /// 微信小程序
        /// </summary>
        [Description("微信小程序")] WeApp = 0,

        /// <summary>
        /// 微信H5
        /// </summary>
        [Description("微信H5")] Mweb = 1,

        /// <summary>
        /// 第三方登录
        /// </summary>
        [Description("第三方登录")] ThirdPartyLogins = 2
    }
}