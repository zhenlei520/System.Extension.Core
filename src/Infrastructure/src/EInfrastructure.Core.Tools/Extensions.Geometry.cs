// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Configuration.Configurations;
using EInfrastructure.Core.Tools.Common;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// Geometry扩展
    /// </summary>
    public partial class Extensions
    {
        #region 计算两点位置的距离，返回两点的距离，单位 米

        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="x">位置1</param>
        /// <param name="y">位置2</param>
        /// <returns></returns>
        public static double GetDistance(this Points<double, double> x, Points<double, double> y)
        {
            return GeometryCommon.GetDistance(x.Y, x.X, y.Y, y.X);
        }

        #endregion

        #region 判断当前坐标是在圆内

        /// <summary>
        /// 判断当前坐标是在圆内
        /// </summary>
        /// <param name="currentPosition">当前位置</param>
        /// <param name="point">圆中心经纬度坐标</param>
        /// <param name="radius">半径，单位：m</param>
        /// <returns></returns>
        public static bool IsInRegion(this Points<double, double> currentPosition, Points<double, double> point,
            int radius)
        {
            double distance = currentPosition.GetDistance(point);
            return radius - distance > 0;
        }

        #endregion

        #region 判断当前坐标是否在多边形内

        /// <summary>
        /// 判断当前坐标是否在多边形内
        /// </summary>
        /// <param name="currentPosition">当前位置</param>
        /// <param name="points">经纬度</param>
        /// <returns></returns>
        public static bool IsInRegion(this Points<double, double> currentPosition,
            params Points<double, double>[] points)
        {
            return currentPosition.IsInRegion(points.SafeList());
        }

        /// <summary>
        /// 判断当前坐标是否在多边形内
        /// </summary>
        /// <param name="currentPosition">当前位置</param>
        /// <param name="points">经纬度</param>
        /// <returns></returns>
        public static bool IsInRegion(this Points<double, double> currentPosition, List<Points<double, double>> points)
        {
            if (points.Count < 3) //点小于3无法构成多边形
            {
                return false;
            }

            int nvert = points.Count;
            List<double> vertx = points.Select(x => x.X).ToList(), verty = points.Select(x => x.Y).ToList();
            int i, j, c = 0;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((verty[i] > currentPosition.Y) != (verty[j] > currentPosition.Y)) &&
                    (currentPosition.X <
                     (vertx[j] - vertx[i]) * (currentPosition.Y - verty[i]) / (verty[j] - verty[i]) + vertx[i]))
                {
                    c = 1 + c;
                }
            }

            if (c % 2 == 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
