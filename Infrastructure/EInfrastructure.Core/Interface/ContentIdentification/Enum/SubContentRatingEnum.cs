using System.ComponentModel;

namespace EInfrastructure.Core.Interface.ContentIdentification.Enum
{
    /// <summary>
    /// 内容详细分级
    /// </summary>
    public enum SubContentRatingEnum
    {
        /// <summary>
        /// 未知的
        /// </summary>
        [Description("未知的")] UnKnow = -1,

        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")] Normal = 0,
    }
}