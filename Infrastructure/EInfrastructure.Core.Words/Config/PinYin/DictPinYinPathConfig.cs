using EInfrastructure.Core.Configuration.Interface.Config;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Words.Config.PinYin
{
    /// <summary>
    /// 文字拼音
    /// </summary>
    public class DictPinYinPathConfig: IScopedConfigModel
    {
        /// <summary>
        /// 拼音下表
        /// </summary>
        [JsonProperty(PropertyName = "pinyin_index_path")]
        public string PinYinIndexPath { get; set; }

        /// <summary>
        /// 拼音数据
        /// </summary>
        [JsonProperty(PropertyName = "pinyin_data_path")]
        public string PinYinDataPath { get; set; }

        /// <summary>
        /// 文字全拼
        /// </summary>
        [JsonProperty(PropertyName = "pinyin_name_path")]
        public string PinYinNamePath { get; set; }

        /// <summary>
        /// 文字信息
        /// </summary>
        [JsonProperty(PropertyName = "word_path")]
        public string WordPath { get; set; }

        /// <summary>
        /// 文字拼音
        /// </summary>
        [JsonProperty(PropertyName = "word_pinyin_path")]
        public string WordPinYinPath { get; set; }
    }
}