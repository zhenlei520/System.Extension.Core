using EInfrastructure.Core.AutoConfig.Interface;

namespace EInfrastructure.Core.Words.Config.Text
{
    /// <summary>
    /// 可修改的字典配置文件地址
    /// </summary>
    public class DictTextPathConfig : IScopedConfigModel
    {
        /// <summary>
        /// 中文简体
        /// </summary>
        public string SimplifiedPath { get; set; }

        /// <summary>
        /// 中文繁体
        /// </summary>
        public string TraditionalPath { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        public string InitialPath { get; set; }

        /// <summary>
        /// 特殊数字符号
        /// </summary>
        public string SpecialNumberPath { get; set; }

        /// <summary>
        /// 转义后的数字
        /// </summary>
        public string TranscodingNumberPath { get; set; }
    }
}