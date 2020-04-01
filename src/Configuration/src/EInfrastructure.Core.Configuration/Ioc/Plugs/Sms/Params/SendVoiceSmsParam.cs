// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params
{
    /// <summary>
    /// 发送语音短信
    /// </summary>
    public class SendVoiceSmsParam
    {
        /// <summary>
        ///
        /// </summary>
        public SendVoiceSmsParam()
        {
            this.PlatTimes = 3;
            this.Volume = 100;
        }
        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 被叫显号，必须是已购买的号码
        /// </summary>
        [JsonProperty(PropertyName = "called_show_number")]
        public string CalledShowNumber { get; set; }

        /// <summary>
        /// 模板code
        /// </summary>
        [JsonProperty(PropertyName = "template_code")]
        public string TemplateCode { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public List<KeyValuePair<string, string>> Content { get; set; }

        /// <summary>
        /// 播放次数
        /// </summary>
        [JsonProperty(PropertyName = "plat_times")]
        public int PlatTimes { get; set; }

        /// <summary>
        /// 播放音量
        /// </summary>
        [JsonProperty(PropertyName = "volume")]
        public int Volume { get; set; }
    }
}
