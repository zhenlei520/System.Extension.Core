// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.WeChat.Enumerations
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public class MsgType : Core.Config.EntitiesExtensions.SeedWork.Enumeration
    {
        /// <summary>
        /// 文本
        /// </summary>
        public static MsgType Text = new MsgType(0, "text", "文本");

        /// <summary>
        /// 图文
        /// </summary>
        public static MsgType Image = new MsgType(1, "image", "图文");

        /// <summary>
        /// 语音
        /// </summary>
        public static MsgType Voice = new MsgType(2, "voice", "语音");

        /// <summary>
        /// 视频
        /// </summary>
        public static MsgType Video = new MsgType(3, "video", "视频");

        /// <summary>
        /// 定位
        /// </summary>
        public static MsgType Location = new MsgType(4, "location", "定位");

        /// <summary>
        /// 链接
        /// </summary>
        public static MsgType Link = new MsgType(5, "link", "链接");

        /// <summary>
        /// 事件
        /// </summary>
        public static MsgType Event = new MsgType(6, "event", "事件");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        public MsgType(int id, string name, string desc) : base(id, name)
        {
            Desc = desc;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; private set; }
    }
}
