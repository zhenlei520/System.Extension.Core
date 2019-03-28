// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.WeChat.Enum
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventTypeEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("")] Unknow = 0,

        /// <summary>
        /// 订阅
        /// </summary>
        [Description("subscribe")] Subscribe = 1,

        /// <summary>
        /// 取消订阅
        /// </summary>
        [Description("unsubscribe")] Unsubscribe = 2,
    }
}