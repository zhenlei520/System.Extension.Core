using Newtonsoft.Json;

namespace EInfrastructure.Core.WebChat.Config
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