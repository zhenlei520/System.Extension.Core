// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params.Sms
{
    /// <summary>
    /// 发送短信
    /// </summary>
    public class SendSmsParam
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 模板code
        /// </summary>
        [JsonProperty(PropertyName = "template_code")]
        public string TemplateCode { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [JsonProperty(PropertyName = "sign_name")]
        public string SignName { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public List<KeyValuePair<string, string>> Content { get; set; }
    }
}
