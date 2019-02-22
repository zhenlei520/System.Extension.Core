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
        public string SimplifiedPath { get;private set; } = "Dict/Text/simplified.txt";

        /// <summary>
        /// 中文繁体
        /// </summary>
        public string TraditionalPath { get;private set; } = "Dict/Text/traditional.txt";

        /// <summary>
        /// 简拼
        /// </summary>
        public string InitialPath { get;private set; } = "Dict/Text/initial.txt";

        /// <summary>
        /// 特殊数字符号
        /// </summary>
        public string SpecialNumberPath { get;private set; } = "Dict/Text/specialNumber.txt";

        /// <summary>
        /// 转义后的数字
        /// </summary>
        public string TranscodingNumberPath { get;private set; } = "Dict/Text/transcodingNumber.txt";

        /// <summary>
        /// 字典词库配置
        /// </summary>
        private static DictTextPathConfig Config;

        /// <summary>
        /// 设置字典词库
        /// </summary>
        /// <param name="simplifiedPath">中文简体</param>
        /// <param name="traditionalPath">中文繁体</param>
        /// <param name="initialPath">简拼</param>
        /// <param name="specialNumberPath">特殊数字符号</param>
        /// <param name="transcodingNumberPath">转义后的数字</param>
        internal static void Set(string simplifiedPath, string traditionalPath, string initialPath,
            string specialNumberPath, string transcodingNumberPath)
        {
            Config = new DictTextPathConfig()
            {
                SimplifiedPath = string.IsNullOrEmpty(simplifiedPath) ? "Dict/Text/simplified.txt" : simplifiedPath,
                TraditionalPath = string.IsNullOrEmpty(traditionalPath) ? "Dict/Text/traditional.txt" : traditionalPath,
                InitialPath = string.IsNullOrEmpty(initialPath) ? "Dict/Text/initial.txt" : initialPath,
                SpecialNumberPath = string.IsNullOrEmpty(specialNumberPath)
                    ? "Dict/Text/specialNumber.txt"
                    : specialNumberPath,
                TranscodingNumberPath = string.IsNullOrEmpty(transcodingNumberPath)
                    ? "Dict/Text/transcodingNumber.txt"
                    : transcodingNumberPath,
            };
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