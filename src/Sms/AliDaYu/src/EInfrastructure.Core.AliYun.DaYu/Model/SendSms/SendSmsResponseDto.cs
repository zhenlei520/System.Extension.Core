// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Newtonsoft.Json;

namespace EInfrastructure.Core.AliYun.DaYu.Model.SendSms
{
    /// <summary>
    /// 短信响应信息
    /// </summary>
    internal class SendSmsResponseDto : BaseReponseDto
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
    internal class SendSmsSuccessResponseDto
    {
        /// <summary>
        ///发送回执ID
        /// </summary>
        [JsonProperty(PropertyName = "BizId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string BizId { get; set; }
    }
}
