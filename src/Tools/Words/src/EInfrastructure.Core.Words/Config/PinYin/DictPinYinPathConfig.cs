// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Words.Config.PinYin
{
    /// <summary>
    /// 文字拼音
    /// </summary>
    public class DictPinYinPathConfig
    {
        /// <summary>
        /// 文字全拼
        /// </summary>
        public string PinYinNamePath { get; set; } = "Dict/PinYin/pinyinName.txt";

        /// <summary>
        /// 拼音下表 
        /// </summary>
        public string PinYinIndexPath { get; set; } = "Dict/PinYin/pinyinIndex.txt";

        /// <summary>
        /// 拼音数据
        /// </summary>
        public string PinYinDataPath { get; set; } = "Dict/PinYin/pinyinData.txt";

        /// <summary>
        /// 文字信息
        /// </summary>
        public string WordPath { get; set; } = "Dict/PinYin/pinyinWord.txt";

        /// <summary>
        /// 文字拼音
        /// </summary>
        public string WordPinYinPath { get; set; } = "Dict/PinYin/wordPinyin.txt";

        /// <summary>
        /// 文字拼音配置
        /// </summary>
        private static DictPinYinPathConfig Config;

        /// <summary>
        /// 设置文字拼音词库
        /// </summary>
        /// <param name="pathConfig">文字拼音词库</param>
        public void Set(DictPinYinPathConfig pathConfig)
        {
            if (pathConfig == null)
            {
                pathConfig = new DictPinYinPathConfig();
            }

            pathConfig.PinYinNamePath = string.IsNullOrEmpty(pathConfig.PinYinNamePath)
                ? "Dict/PinYin/pinyinName.txt"
                : pathConfig.PinYinNamePath;
            pathConfig.PinYinIndexPath = string.IsNullOrEmpty(pathConfig.PinYinIndexPath)
                ? "Dict/PinYin/pinyinIndex.txt"
                : pathConfig.PinYinIndexPath;
            pathConfig.PinYinDataPath = string.IsNullOrEmpty(pathConfig.PinYinDataPath)
                ? "Dict/PinYin/pinyinData.txt"
                : pathConfig.PinYinDataPath;
            pathConfig.WordPath = string.IsNullOrEmpty(pathConfig.WordPath)
                ? "Dict/PinYin/pinyinWord.txt"
                : pathConfig.WordPath;
            pathConfig.WordPinYinPath = string.IsNullOrEmpty(pathConfig.WordPinYinPath)
                ? "Dict/PinYin/wordPinyin.txt"
                : pathConfig.WordPinYinPath;
        }

        /// <summary>
        /// 读取文字拼音词库
        /// </summary>
        /// <returns></returns>
        internal static DictPinYinPathConfig Get()
        {
            return Config ?? (Config = new DictPinYinPathConfig());
        }
    }
}