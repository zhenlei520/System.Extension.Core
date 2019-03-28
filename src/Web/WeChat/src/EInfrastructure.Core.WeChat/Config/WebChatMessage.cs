// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Xml.Serialization;

namespace EInfrastructure.Core.WeChat.Config
{
  /// <summary>
  /// 微信消息
  /// </summary>
  [XmlRoot("xml", Namespace = "")]
  public class WebChatMessage
  {
    /// <summary>
    /// 开发者微信号
    /// </summary>
    public string ToUserName { get; set; }

    /// <summary>
    /// 发送方帐号（一个OpenID）
    /// </summary>
    public string FromUserName { get; set; }

    /// <summary>
    /// 消息创建时间 （整型）
    /// </summary>
    public double CreateTime { get; set; }

    /// <summary>
    /// text
    /// </summary>
    public string MsgType { get; set; }

    #region 普通消息

    /// <summary>
    /// 
    /// </summary>
    public string Content { get; set; } = "";

    #endregion

    #region 其他消息类型

    #region 图片消息

    /// <summary>
    /// 图片链接（由系统生成）
    /// </summary>
    public string PicUrl { get; set; }

    #endregion

    #region 语音消息

    /// <summary>
    /// 语音格式，如amr，speex等
    /// </summary>
    public string Format { get; set; }

    /// <summary>
    /// 语音识别结果，UTF8编码
    /// </summary>
    public string Recognition { get; set; }

    #endregion

    #region 视频消息或小视频消息

    /// <summary>
    /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
    /// </summary>
    public string ThumbMediaId { get; set; }

    #endregion

    #region 地理位置消息

    /// <summary>
    /// 地理位置维度
    /// </summary>
    [XmlElement("Location_X")]
    public string LocationX { get; set; }

    /// <summary>
    /// 地理位置维度
    /// </summary>
    [XmlElement("Location_Y")]
    public string LocationY { get; set; }

    /// <summary>
    /// 地图缩放大小
    /// </summary>
    public string Scale { get; set; }

    /// <summary>
    /// 地理位置信息
    /// </summary>
    public string Label { get; set; }

    #endregion

    #region 链接消息

    /// <summary>
    /// 消息标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 消息描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 消息链接
    /// </summary>
    public string Url { get; set; }

    #endregion

    #region 事件

    /// <summary>
    /// Event 关注：subscribe   取消关注：unsubscribe
    /// </summary>
    public string Event { get; set; }

    #endregion

    /// <summary>
    /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
    /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
    /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
    /// </summary>
    public string MediaId { get; set; }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public string MsgId { get; set; }
  }
}
