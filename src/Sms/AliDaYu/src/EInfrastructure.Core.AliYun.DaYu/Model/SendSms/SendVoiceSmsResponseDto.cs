// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Newtonsoft.Json;

namespace EInfrastructure.Core.AliYun.DaYu.Model.SendSms
{
    /// <summary>
    /// 发送语音短信信息
    /// </summary>
    internal class SendVoiceSmsResponseDto : BaseReponseDto
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// 发送成功消息
    /// </summary>
    internal class SendVoiceSmsSuccessResponseDto : SendVoiceSmsResponseDto
    {
        /// <summary>
        ///发送回执ID
        /// </summary>
        [JsonProperty(PropertyName = "CallId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CallId { get; set; }
    }
}
