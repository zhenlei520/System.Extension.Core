// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.WeChat.Enum
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MsgTypeEnum
    {
        /// <summary>
        /// 文本
        /// </summary>
        [Description("text")] Text = 0,

        /// <summary>
        /// 图文
        /// </summary>
        [Description("image")] Image = 1,

        /// <summary>
        /// 语音
        /// </summary>
        [Description("voice")] Voice = 2,

        /// <summary>
        /// 视频
        /// </summary>
        [Description("video")] Video = 3,

        /// <summary>
        /// 定位
        /// </summary>
        [Description("location")] Location = 4,

        /// <summary>
        /// 链接
        /// </summary>
        [Description("link")] Link = 5,

        /// <summary>
        /// 事件
        /// </summary>
        [Description("event")] Event = 6,
    }
}