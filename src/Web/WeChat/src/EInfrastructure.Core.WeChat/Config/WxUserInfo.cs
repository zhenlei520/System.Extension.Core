// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace EInfrastructure.Core.WeChat.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class WxUserInfo
    {
        /// <summary>
        /// OpenId
        /// </summary>
        [JsonProperty(PropertyName = "openid")]
        public string Openid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "sex")]
        public int Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "province")]
        public string Province { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "headimgurl")]
        public string Headimgurl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "privilege")]
        public List<string> Privilege { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "unionid")]
        public string Unionid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public string Errcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "openid")]
        public string Errmsg { get; set; }
    }
}