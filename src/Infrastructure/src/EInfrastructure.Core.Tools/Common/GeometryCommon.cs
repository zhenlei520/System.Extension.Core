// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// 地理空间帮助类
    /// </summary>
    public class GeometryCommon
    {
        //地球半径，单位米
        private const double EARTH_RADIUS = 6378137;

        #region 计算两点位置的距离，返回两点的距离，单位 米

        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLong1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLong2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLong1 - radLong2;
            double result =
                2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
                                        Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) *
                EARTH_RADIUS;
            return result;
        }

        #endregion

        #region private methods

        #region 经纬度转化成弧度

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return d * Math.PI / 180d;
        }

        #endregion

        #endregion
    }
}
