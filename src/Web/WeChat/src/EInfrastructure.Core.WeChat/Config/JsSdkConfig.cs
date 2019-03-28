// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Newtonsoft.Json;

namespace EInfrastructure.Core.WeChat.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class JsSdkConfig
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("appId")]
        public string AppId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("timestamp")]
        public long TimeStamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("nonceStr")]
        public string NonceStr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}