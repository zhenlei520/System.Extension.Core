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

        #region 校验

        #region IsEven(是否偶数)

        /// <summary>
        /// 是否偶数
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEven(this long value) => value % 2 == 0;

        #endregion

        #region IsOdd(是否奇数)

        /// <summary>
        /// 是否奇数
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsOdd(this long value) => value % 2 != 0;

        #endregion

        #region IsPrime(是否质数)

        /// <summary>
        /// 是否质数（素数），一个质数（或素数）是具有两个不同约束的自然数：1和它本身
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsPrime(this long value)
        {
            if ((value & 1) == 0)
            {
                if (value == 2)
                    return true;
                return false;
            }

            for (long i = 3; (i * i) <= value; i += 2)
            {
                if ((value % i) == 0)
                    return false;
            }

            return value != 1;
        }

        /// <summary>
        /// 是否质数（素数），一个质数（或素数）是具有两个不同约束的自然数：1和它本身
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsPrime(this long? value)
        {
            if (value == null)
            {
                return false;
            }

            return IsPrime(value.Value);
        }

        #endregion

        #endregion
    }
}
