// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Xml.Serialization;

namespace EInfrastructure.Core.AliYun.DaYu.Model.SendSms
{
    /// <summary>
    /// 发送短信成功响应信息
    /// </summary>
    [XmlRoot(ElementName = "SendSmsResponse")]
    public class SendSmsSuccessResponse
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        [XmlElement]
        public string Message { get; set; }

        /// <summary>
        /// 请求id
        /// </summary>
        [XmlElement]
        public string RequestId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [XmlElement(IsNullable=true)]
        public string BizId { get; set; }

        /// <summary>
        /// 响应Code
        /// </summary>
        [XmlElement]
        public string Code { get; set; }
    }

    /// <summary>
    /// 错误响应信息
    /// </summary>
    [XmlRoot(ElementName = "Error")]
    public class SendSmsErrorResponse
    {
        /// <summary>
        /// 请求id
        /// </summary>
        [XmlElement]
        public string RequestId { get; set; }

        /// <summary>
        /// 域id
        /// </summary>
        [XmlElement]
        public string HostId { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        [XmlElement]
        public string Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [XmlElement]
        public string Message { get; set; }

        /// <summary>
        /// 推荐
        /// </summary>
        [XmlElement]
        public string Recommend { get; set; }

    }
}
