// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.WeChat.Enumerations
{
    /// <summary>
    /// 微信类型
    /// </summary>
    public class WebChatType : Enumeration
    {
        /// <summary>
        /// 微信小程序
        /// </summary>
        public static WebChatType WeApp=new WebChatType(0,"微信小程序");

        /// <summary>
        /// 微信H5
        /// </summary>
        public static WebChatType MWeb=new WebChatType(1,"微信H5");

        /// <summary>
        /// 第三方登录
        /// </summary>
        public static WebChatType ThirdPartyLogins=new WebChatType(2,"第三方登录");

        public WebChatType(int id, string name) : base(id, name)
        {
        }
    }
}
