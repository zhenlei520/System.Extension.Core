using EInfrastructure.Core.AutoConfig.Interface;

namespace EInfrastructure.Core.Words.Config.PinYin
{
    /// <summary>
    /// 文字拼音
    /// </summary>
    public class DictPinYinPathConfig : IScopedConfigModel
    {
        /// <summary>
        /// 拼音下表
        /// </summary>
        public string PinYinIndexPath { get; set; }

        /// <summary>
        /// 拼音数据
        /// </summary>
        public string PinYinDataPath { get; set; }

        /// <summary>
        /// 文字全拼
        /// </summary>
        public string PinYinNamePath { get; set; }

        /// <summary>
        /// 文字信息
        /// </summary>
        public string WordPath { get; set; }

        /// <summary>
        /// 文字拼音
        /// </summary>
        public string WordPinYinPath { get; set; }
    }
}