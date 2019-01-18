using Newtonsoft.Json;

namespace EInfrastructure.Core.Words.Config.Text
{
    /// <summary>
    /// 字典配置信息
    /// </summary>
    internal class DictTextConfig
    {
        /// <summary>
        /// 中文简体
        /// </summary>
        [JsonProperty(PropertyName = "simplified")]
        public string Simplified { get; set; }

        /// <summary>
        /// 中文繁体
        /// </summary>
        [JsonProperty(PropertyName = "traditional")]
        public string Traditional { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        [JsonProperty(PropertyName = "initial")]
        public string Initial { get; set; }

        /// <summary>
        /// 特殊数字符号
        /// </summary>
        [JsonProperty(PropertyName = "special_number")]
        public string SpecialNumber { get; set; }

        /// <summary>
        /// 转义后的数字
        /// </summary>
        [JsonProperty(PropertyName = "transcoding_number")]
        public string TranscodingNumber { get; set; }
    }
}