using EInfrastructure.Core.AutoConfig.Interface;

namespace EInfrastructure.Core.Words.Config.Text
{
    /// <summary>
    /// 可修改的字典配置文件地址
    /// </summary>
    public class DictTextPathConfig : ISingletonConfigModel
    {
        /// <summary>
        /// 中文简体
        /// </summary>
        public string SimplifiedPath { get; set; } = "Dict/Text/simplified.txt";

        /// <summary>
        /// 中文繁体
        /// </summary>
        public string TraditionalPath { get; set; } = "Dict/Text/traditional.txt";

        /// <summary>
        /// 简拼
        /// </summary>
        public string InitialPath { get; set; } = "Dict/Text/initial.txt";

        /// <summary>
        /// 特殊数字符号
        /// </summary>
        public string SpecialNumberPath { get; set; } = "Dict/Text/specialNumber.txt";

        /// <summary>
        /// 转义后的数字
        /// </summary>
        public string TranscodingNumberPath { get; set; } = "Dict/Text/transcodingNumber.txt";
    }
}