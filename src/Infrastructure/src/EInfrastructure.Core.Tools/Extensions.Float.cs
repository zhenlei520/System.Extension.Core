// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// Float扩展
    /// </summary>
    public partial class Extensions
    {
        #region Abs(返回数字的绝对值)

        /// <summary>
        /// 返回数字的绝对值
        /// </summary>
        /// <param name="dec">值</param>
        public static float Abs(this float dec) => Math.Abs(dec);

        /// <summary>
        /// 返回数字的绝对值
        /// </summary>
        /// <param name="dec">值</param>
        public static IEnumerable<float> Abs(this IEnumerable<float> dec) => dec.Select(x => x.Abs());

        #endregion

        #region 保留指定位数(默认四舍五入)

        /// <summary>
        /// 保留指定位数(默认四舍五入)
        /// </summary>
        /// <param name="param">值</param>
        /// <param name="num">保留位数</param>
        /// <param name="midpointRounding">默认正常的四舍五入</param>
        /// <returns></returns>
        public static string ToFixed(this float param, int num,
            MidpointRounding midpointRounding = MidpointRounding.AwayFromZero)
        {
            param = Math.Round(param, num, midpointRounding).ConvertToFloat(0);
            return param.ToString("0." + "".RepairZero(num));
        }

        #endregion
    }
}
