using System.ComponentModel;

namespace EInfrastructure.Core.HelpCommon.Enums
{
    /// <summary>
    /// 星座
    /// </summary>
    public enum ConstellationEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")] Unknow = -2,

        /// <summary>
        /// 白羊座
        /// </summary>
        [Description("白羊座")] Aries = 1,

        /// <summary>
        /// 金牛座
        /// </summary>
        [Description("金牛座")] Taurus = 2,

        /// <summary>
        /// 双子座
        /// </summary>
        [Description("双子座")] Gemini = 3,

        /// <summary>
        /// 巨蟹座
        /// </summary>
        [Description("巨蟹座")] Cancer = 4,

        /// <summary>
        /// 狮子座
        /// </summary>
        [Description("狮子座")] Leo = 5,

        /// <summary>
        /// 处女座
        /// </summary>
        [Description("处女座")] Virgo = 6,

        /// <summary>
        /// 天秤座
        /// </summary>
        [Description("天秤座")] Libra = 7,

        /// <summary>
        /// 天蝎座
        /// </summary>
        [Description("天蝎座")] Scorpio = 8,

        /// <summary>
        /// 射手座
        /// </summary>
        [Description("射手座")] Sagittarius = 9,

        /// <summary>
        /// 摩羯座
        /// </summary>
        [Description("摩羯座")] Capricornus = 10,

        /// <summary>
        /// 水瓶座
        /// </summary>
        [Description("水瓶座")] Aquarius = 11,

        /// <summary>
        /// 双鱼座
        /// </summary>
        [Description("双鱼座")] Pisces = 12
    }
}