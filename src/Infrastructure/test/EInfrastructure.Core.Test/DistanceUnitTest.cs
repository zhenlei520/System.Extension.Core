// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.HelpCommon;
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
        [InlineData(34.772586d, 113.636379d, 34.777745, 113.611227)]
        [InlineData(34.777448, 113.618773, 34.774483, 113.623516)]
        public void GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            var result = DistanceCommon.GetDistance(lat1, lng1, lat2, lng2);
        }
    }
}