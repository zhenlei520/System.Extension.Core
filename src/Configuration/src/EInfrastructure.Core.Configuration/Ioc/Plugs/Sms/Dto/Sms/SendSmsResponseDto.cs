// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Dto.Sms
{
    /// <summary>
    /// 发送短信响应信息
    /// </summary>
    public class SendSmsResponseDto
    {
        /// <summary>
        /// 发送短信响应信息
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="data">扩展信息</param>
        public SendSmsResponseDto(string mobile, object data)
        {
            Mobile = mobile;
            Data = data;
        }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; private set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty(PropertyName = "mobile")]
        public string Mobile { get; private set; }
    }

    /// <summary>
    /// 发送多个短信响应信息
    /// </summary>
    public class SendMultSmsResponseDto
    {
        /// <summary>
        /// 发送短信响应信息
        /// </summary>
        /// <param name="mobiles">手机号集合</param>
        /// <param name="data">扩展信息</param>
        public SendMultSmsResponseDto(List<string> mobiles, object data)
        {
            Mobiles = mobiles;
            Data = data;
        }

        /// <summary>
        /// 扩展信息
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; private set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty(PropertyName = "mobiles")]
        public List<string> Mobiles { get; private set; }
    }
}
