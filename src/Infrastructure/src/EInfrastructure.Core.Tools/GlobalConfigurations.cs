// Copyright (c) zhenlei520 All rights reserved.

using System.Globalization;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 配置
    /// </summary>
    internal class GlobalConfigurations
    {
        /// <summary>
        /// 国际日历
        /// </summary>
        public static GregorianCalendar Calendar;

        /// <summary>
        /// 中国日历
        /// </summary>
        internal static ChineseLunisolarCalendar ChineseCalendar;

        /// <summary>
        ///
        /// </summary>
        static GlobalConfigurations()
        {
            Calendar = new GregorianCalendar();
            ChineseCalendar = new ChineseLunisolarCalendar();
        }
    }
}
