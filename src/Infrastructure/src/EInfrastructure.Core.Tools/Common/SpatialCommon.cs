using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Configuration.Configurations;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// 空间帮助类
    /// </summary>
    public class SpatialCommon
    {
        #region 判断当前坐标是否在多边形内

        /// <summary>
        /// 判断当前坐标是否在多边形内
        /// </summary>
        /// <param name="points">经纬度</param>
        /// <param name="lng">经度</param>
        /// <param name="lat">维度</param>
        /// <returns></returns>
        public static bool IsInRegion(List<Points<double, double>> points, double lng, double lat)
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
                if (((verty[i] > lat) != (verty[j] > lat)) &&
                    (lng < (vertx[j] - vertx[i]) * (lat - verty[i]) / (verty[j] - verty[i]) + vertx[i]))
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

        #region 判断当前坐标是在圆内

        /// <summary>
        /// 判断当前坐标是在圆内
        /// </summary>
        /// <param name="currentPosition">当前位置</param>
        /// <param name="point">圆中心经纬度坐标</param>
        /// <param name="radius">半径，单位：m</param>
        /// <returns></returns>
        public static bool IsInRegion(Points<double, double> currentPosition, Points<double, double> point, int radius)
        {
            double distance = DistanceCommon.GetDistance(currentPosition, point);
            return radius - distance > 0;
        }

        #endregion
    }
}
