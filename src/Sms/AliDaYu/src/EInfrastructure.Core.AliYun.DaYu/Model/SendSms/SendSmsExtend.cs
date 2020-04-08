// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Newtonsoft.Json;

namespace EInfrastructure.Core.AliYun.DaYu.Model.SendSms
{
    /// <summary>
    /// 短信验证码扩展信息
    /// </summary>
    public class SendSmsExtend
    {
        /// <summary>
        /// 请求id
        /// </summary>
        [JsonProperty(PropertyName = "request_id")]
        public string RequestId { get; set; }

        /// <summary>
        /// 发送回执ID，可根据该ID在接口QuerySendDetails中查询具体的发送状态。
        /// </summary>
        [JsonProperty(PropertyName = "biz_id")]
        public string BizId { get; set; }

        /// <summary>
        /// 状态码的描述。
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        public string Msg { get; set; }
    }
}
