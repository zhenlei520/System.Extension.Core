using EInfrastructure.Core.AutoConfig.Interface;

namespace EInfrastructure.Core.Words.Config.PinYin
{
    /// <summary>
    /// 文字拼音
    /// </summary>
    public class DictPinYinPathConfig : ISingletonConfigModel
    {
        /// <summary>
        /// 拼音下表
        /// </summary>
        public string PinYinIndexPath { get; set; } = "Dict/PinYin/pinyinIndex.txt";

        /// <summary>
        /// 拼音数据
        /// </summary>
        public string PinYinDataPath { get; set; } = "Dict/PinYin/pinyinData.txt";

        /// <summary>
        /// 文字全拼
        /// </summary>
        public string PinYinNamePath { get; set; } = "Dict/PinYin/pinyinName.txt";

        /// <summary>
        /// 文字信息
        /// </summary>
        public string WordPath { get; set; } = "Dict/PinYin/pinyinWord.txt";

        /// <summary>
        /// 文字拼音
        /// </summary>
        public string WordPinYinPath { get; set; } = "Dict/PinYin/wordPinyin.txt";
    }
}