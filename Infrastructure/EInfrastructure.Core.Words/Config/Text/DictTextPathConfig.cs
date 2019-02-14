using EInfrastructure.Core.Exception;

namespace EInfrastructure.Core.Words.Config.Text
{
    /// <summary>
    /// 可修改的字典配置文件地址
    /// </summary>
    public class DictTextPathConfig
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

        /// <summary>
        /// 字典词库配置
        /// </summary>
        private static DictTextPathConfig Config;

        /// <summary>
        /// 设置字典词库
        /// </summary>
        /// <param name="config"></param>
        internal static void Set(DictTextPathConfig config)
        {
            Config = config;
        }

        /// <summary>
        /// 读取字典词库
        /// </summary>
        /// <returns></returns>
        internal static DictTextPathConfig Get()
        {
            return Config ?? (Config = new DictTextPathConfig());
        }
    }
}