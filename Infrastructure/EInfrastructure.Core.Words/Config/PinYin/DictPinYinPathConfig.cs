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
        public string PinYinNamePath { get; private set; } = "Dict/PinYin/pinyinName.txt";

        /// <summary>
        /// 拼音下表 
        /// </summary>
        public string PinYinIndexPath { get; private set; } = "Dict/PinYin/pinyinIndex.txt";

        /// <summary>
        /// 拼音数据
        /// </summary>
        public string PinYinDataPath { get; private set; } = "Dict/PinYin/pinyinData.txt";

        /// <summary>
        /// 文字信息
        /// </summary>
        public string WordPath { get; private set; } = "Dict/PinYin/pinyinWord.txt";

        /// <summary>
        /// 文字拼音
        /// </summary>
        public string WordPinYinPath { get; private set; } = "Dict/PinYin/wordPinyin.txt";

        /// <summary>
        /// 文字拼音配置
        /// </summary>
        private static DictPinYinPathConfig Config;

        /// <summary>
        /// 设置文字拼音词库
        /// </summary>
        /// <param name="pinYinNamePath">文字全拼</param>
        /// <param name="pinYinIndexPath">拼音下表</param>
        /// <param name="pinYinDataPath">拼音数据</param>
        /// <param name="wordPath">文字信息</param>
        /// <param name="wordPinYinPath">文字拼音</param>
        public void Set(string pinYinNamePath = "", string pinYinIndexPath = "", string pinYinDataPath = "",
            string wordPath = "",
            string wordPinYinPath = "")
        {
            Config = new DictPinYinPathConfig()
            {
                PinYinNamePath = string.IsNullOrEmpty(pinYinNamePath) ? "Dict/PinYin/pinyinName.txt" : pinYinNamePath,
                PinYinIndexPath = string.IsNullOrEmpty(pinYinIndexPath)
                    ? "Dict/PinYin/pinyinIndex.txt"
                    : pinYinIndexPath,
                PinYinDataPath = string.IsNullOrEmpty(pinYinDataPath) ? "Dict/PinYin/pinyinData.txt" : PinYinDataPath,
                WordPath = string.IsNullOrEmpty(wordPath) ? "Dict/PinYin/pinyinWord.txt" : WordPath,
                WordPinYinPath = string.IsNullOrEmpty(wordPinYinPath) ? "Dict/PinYin/wordPinyin.txt" : WordPinYinPath
            };
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