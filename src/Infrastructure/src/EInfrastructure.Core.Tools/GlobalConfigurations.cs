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
        ///
        /// </summary>
        public static GregorianCalendar Calendar;

        /// <summary>
        ///
        /// </summary>
        static GlobalConfigurations()
        {
            Calendar = new GregorianCalendar();
        }
    }
}
