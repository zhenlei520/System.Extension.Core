// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Configuration.Configurations;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 距离帮助类
    /// </summary>
    public class DistanceCommon
    {
        //地球半径，单位米
        private const double EARTH_RADIUS = 6378137;

        #region 计算两点位置的距离，返回两点的距离，单位 米

        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="x">位置1</param>
        /// <param name="y">位置2</param>
        /// <returns></returns>
        public static double GetDistance(Points<double, double> x, Points<double, double> y)
        {
            return GetDistance(x.Y, x.X, y.Y, y.X);
        }

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
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
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
