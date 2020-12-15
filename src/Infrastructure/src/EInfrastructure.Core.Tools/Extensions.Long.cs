// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Globalization;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// Long类型扩展
    /// </summary>
    public partial class Extensions
    {
        #region 将时间戳转时间

        /// <summary>
        /// 将时间戳转时间
        /// </summary>
        /// <param name="unixTimeStamp">待转时间戳</param>
        /// <param name="dateTimeKind"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp,
            DateTimeKind dateTimeKind = DateTimeKind.Utc)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, dateTimeKind);
            switch (unixTimeStamp.ToString(CultureInfo.InvariantCulture).Length)
            {
                case 10:
                    dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                    break;
                case 13:
                    dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
                    break;
            }

            return dtDateTime;
        }

        #endregion
    }
}
