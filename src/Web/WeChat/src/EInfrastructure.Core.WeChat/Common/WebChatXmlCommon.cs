using System;
using EInfrastructure.Core.HelpCommon;

namespace EInfrastructure.Core.WeChat.Common
{
    /// <summary>
    /// 微信xml消息帮助
    /// </summary>
    public class WebChatXmlCommon
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="fromUserName"></param>
        /// <param name="events"></param>
        public WebChatXmlCommon(string toUserName, string fromUserName, string events = "")
        {
            ToUserName = toUserName;
            FromUserName = fromUserName;
        }

        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string ToUserName { get; private set; }

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string FromUserName { get; private set; }

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public double CreateTime { get; private set; } = TimeCommon.ConvertDateTimeInt(DateTime.UtcNow);

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; private set; }

        #region 得到响应的文本消息

        /// <summary>
        /// 得到响应的文本消息
        /// </summary>
        /// <param name="content">文本内容</param>
        /// <returns></returns>
        public string GetText(string content)
        {
            MsgType = "text";
            return
                $"<xml><ToUserName><![CDATA[{ToUserName}]]></ToUserName><FromUserName><![CDATA[{FromUserName}]]></FromUserName> <CreateTime>{CreateTime}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{content}]]></Content></xml>";
        }

        #endregion
    }
}