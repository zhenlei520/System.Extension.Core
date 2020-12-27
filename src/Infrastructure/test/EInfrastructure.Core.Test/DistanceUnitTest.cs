// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Common;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// Distance Unit Test
    /// </summary>
    public class DistanceUnitTest
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lng1"></param>
        /// <param name="lat2"></param>
        /// <param name="lng2"></param>
        [Theory]
        [InlineData(34.772058d, 113.734792d, 34.788907d, 113.787664d)]
        [InlineData(34.780589d, 113.796848d, 34.784607d, 113.695997d)]
        public void GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            var result = GeometryCommon.GetDistance(lat1, lng1, lat2, lng2);
        }

        [Theory]
        [InlineData(4, 32, 4)]
        [InlineData(2, 1, 4)]
        [InlineData(3, 2, 12)]
        public void Test(int x, int len, int res)
        {
            Check.True(x << len == res, "异常");
        }
    }
}
