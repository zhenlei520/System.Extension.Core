using System.ComponentModel;

namespace EInfrastructure.Core.Configuration.Enum
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum GenderEnum
    {
        /// <summary>
        /// 未知 
        /// </summary>
        [Description("未知")] Unknow = 0,

        /// <summary>
        /// 男
        /// </summary>
        [Description("男")] Boy = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")] Girl = 2,
    }
}