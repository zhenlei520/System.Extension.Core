// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Enum;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Dto.Sms
{
    /// <summary>
    /// 发送短信记录
    /// </summary>
    public class SendSmsRecordDto
    {
        /// <summary>
        /// 短信内容
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        [JsonProperty(PropertyName = "sms_code")]
        public SmsCode SmsCode { get; set; }

        /// <summary>
        /// 扩展流水id
        /// </summary>
        [JsonProperty(PropertyName = "out_id")]
        public string OutId { get; set; }

        /// <summary>
        /// 接受短信的手机号
        /// </summary>
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 发送日期
        /// </summary>
        [JsonProperty(PropertyName = "send_date")]
        public string SendDate { get; set; }

        /// <summary>
        /// 接受日期
        /// </summary>
        [JsonProperty(PropertyName = "receive_date")]
        public string ReceiveDate { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public SendStatus Status { get; set; }
    }
}
