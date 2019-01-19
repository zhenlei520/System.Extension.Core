using EInfrastructure.Core.AutoConfig.Interface;

namespace EInfrastructure.Core.Words.Config.PinYin
{
    /// <summary>
    /// 文字拼音
    /// </summary>
    public class DictPinYinPathConfig : ISingletonConfigModel
    {
        #region 不可修改

        /// <summary>
        /// 文字全拼 ××
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

        #endregion


        #region 肯定修改

        /// <summary>
        /// 文字信息（需要重写，已确定）
        /// </summary>
        public string WordPath { get; set; } = "Dict/PinYin/pinyinWord.txt";

        /// <summary>
        /// 文字拼音（需要重写）
        /// </summary>
        public string WordPinYinPath { get; set; } = "Dict/PinYin/wordPinyin.txt";

        #endregion
    }
}