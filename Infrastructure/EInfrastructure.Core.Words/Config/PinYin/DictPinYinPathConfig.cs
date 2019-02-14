using EInfrastructure.Core.Exception;

namespace EInfrastructure.Core.Words.Config.PinYin
{
    /// <summary>
    /// 文字拼音
    /// </summary>
    public class DictPinYinPathConfig
    {
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

        /// <summary>
        /// 文字信息（需要重写，已确定）
        /// </summary>
        public string WordPath { get; set; } = "Dict/PinYin/pinyinWord.txt";

        /// <summary>
        /// 文字拼音（需要重写）
        /// </summary>
        public string WordPinYinPath { get; set; } = "Dict/PinYin/wordPinyin.txt";

        /// <summary>
        /// 文字拼音配置
        /// </summary>
        private static DictPinYinPathConfig Config;

        /// <summary>
        /// 设置文字拼音词库
        /// </summary>
        /// <param name="config"></param>
        internal static void Set(DictPinYinPathConfig config)
        {
            Config = config;
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