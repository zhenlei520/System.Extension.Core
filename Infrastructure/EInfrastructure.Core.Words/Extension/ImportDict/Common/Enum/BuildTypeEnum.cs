using System.ComponentModel;

namespace EInfrastructure.Core.Words.Extension.ImportDict.Common.Enum
{
    public enum BuildTypeEnum
    {
        /// <summary>
        /// 字符串左边包含分隔符
        /// </summary>
        [Description("字符串左边包含分隔符")] LeftContain,

        /// <summary>
        /// 字符串右边包含分隔符
        /// </summary>
        [Description("字符串右边包含分隔符")] RightContain,

        /// <summary>
        /// 字符串两侧都不包含分隔符
        /// </summary>
        [Description("字符串两侧都不包含分隔符")] None,

        /// <summary>
        /// 字符串两侧都有分隔符
        /// </summary>
        [Description("字符串两侧都有分隔符")] FullContain
    }
}