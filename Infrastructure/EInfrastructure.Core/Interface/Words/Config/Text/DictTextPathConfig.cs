using EInfrastructure.Core.Configuration.Interface.Config;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Interface.Words.Config.Text
{
    /// <summary>
    /// 可修改的字典配置文件地址
    /// </summary>
    public class DictTextPathConfig : IScopedConfigModel
    {
        /// <summary>
        /// 中文简体
        /// </summary>
        [JsonProperty(PropertyName = "simplified_path")]
        public string SimplifiedPath { get; set; }

        /// <summary>
        /// 中文繁体
        /// </summary>
        [JsonProperty(PropertyName = "traditional_path")]
        public string TraditionalPath { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        [JsonProperty(PropertyName = "initial_path")]
        public string InitialPath { get; set; }

        /// <summary>
        /// 特殊数字符号
        /// </summary>
        [JsonProperty(PropertyName = "special_number_path")]
        public string SpecialNumberPath { get; set; }

        /// <summary>
        /// 转义后的数字
        /// </summary>
        [JsonProperty(PropertyName = "transcoding_number_path")]
        public string TranscodingNumberPath { get; set; }
    }
}