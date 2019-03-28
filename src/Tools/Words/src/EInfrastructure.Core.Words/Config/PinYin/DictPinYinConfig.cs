// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Words.Config.PinYin
{
    /// <summary>
    /// 字典拼音配置
    /// </summary>
    internal class DictPinYinConfig
    {
        /// <summary>
        /// 拼音下表
        /// </summary>
        [JsonProperty(PropertyName = "pinyin_index")]
        public short[] PinYinIndex { get; set; }

        /// <summary>
        /// 拼音数据
        /// </summary>
        [JsonProperty(PropertyName = "pinyin_data")]
        public short[] PinYinData { get; set; }

        /// <summary>
        /// 文字全拼
        /// </summary>
        [JsonProperty(PropertyName = "pinyin_name")]
        public List<string> PinYinName { get; set; }

        /// <summary>
        /// 文字信息
        /// </summary>
        [JsonProperty(PropertyName = "word")]
        public string Word { get; set; }

        /// <summary>
        /// 文字拼音
        /// </summary>
        [JsonProperty(PropertyName = "word_pinyin")]
        public short[] WordPinYin { get; set; }
    }
}