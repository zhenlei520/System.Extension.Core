// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Enum;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Dto
{
    /// <summary>
    /// 短信响应信息
    /// </summary>
    public class SmsResponseDto<T>
    {
        /// <summary>
        /// 响应结果
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public SmsCode Code { get; set; }

        /// <summary>
        /// 响应信息
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        public string Msg { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [JsonProperty(PropertyName = "extend")]
        public T Extend { get; set; }
    }
}
