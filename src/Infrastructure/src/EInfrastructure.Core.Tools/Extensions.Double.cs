// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// Double扩展
    /// </summary>
    public partial class Extensions
    {
        #region 保留指定位数(默认四舍五入)

        /// <summary>
        /// 保留指定位数(默认四舍五入)
        /// </summary>
        /// <param name="dec">值</param>
        /// <param name="num">保留位数</param>
        /// <param name="midpointRounding">默认正常的四舍五入</param>
        /// <returns></returns>
        public static string ToFixed(this double dec, int num,
            MidpointRounding midpointRounding = MidpointRounding.AwayFromZero)
        {
            dec = Math.Round(dec, num, midpointRounding);
            return dec.ToString("0." + "".RepairZero(num));
        }

        #endregion
    }
}
