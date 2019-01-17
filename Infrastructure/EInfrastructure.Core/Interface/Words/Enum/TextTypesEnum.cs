using System.ComponentModel;

namespace EInfrastructure.Core.Interface.Words.Enum
{
    /// <summary>
    /// 文字类型
    /// </summary>
    public enum TextTypesEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")] Unknow = 0,

        /// <summary>
        /// 简体
        /// </summary>
        [Description("简体")] Simplified = 1,

        /// <summary>
        /// 繁体
        /// </summary>
        [Description("繁体")] Traditional = 2,

        /// <summary>
        /// 日文
        /// </summary>
        [Description("日文")] Japanese = 3,
    }
}