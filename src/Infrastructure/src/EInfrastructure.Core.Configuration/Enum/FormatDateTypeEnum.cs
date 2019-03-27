using System.ComponentModel;

namespace EInfrastructure.Core.Configuration.Enum
{
    /// <summary>
    /// 格式化时间类型
    /// </summary>
    public enum FormatDateTypeEnum
    {
        /// <summary>
        /// yyyy-MM-dd
        /// </summary>
        [Description("yyyy-MM-dd")]Zero = 0,

        /// <summary>
        /// 格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        [Description("yyyy-MM-dd HH:mm:ss")] One = 1,

        /// <summary>
        /// 格式：yyyy/MM/dd
        /// </summary>
        [Description("yyyy/MM/dd")] Two = 2,

        /// <summary>
        /// 格式：yyyy年MM月dd日
        /// </summary>
        [Description("yyyy年MM月dd日")] Three = 3,

        /// <summary>
        /// 格式：MM-dd
        /// </summary>
        [Description("MM-dd")] Four = 4,

        /// <summary>
        /// 格式：MM/dd
        /// </summary>
        [Description("MM/dd")] Five = 5,

        /// <summary>
        /// 格式：MM月dd日
        /// </summary>
        [Description("MM月dd日")] Six = 6,

        /// <summary>
        /// 格式：yyyy-MM
        /// </summary>
        [Description("yyyy-MM")] Seven = 7,

        /// <summary>
        /// 格式：yyyy/MM
        /// </summary>
        [Description("yyyy/MM")] Eight = 8,

        /// <summary>
        /// yyyy年MM月
        /// </summary>
        [Description("yyyy年MM月")] Nine = 9
    }
}