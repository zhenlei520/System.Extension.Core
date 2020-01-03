// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using Xunit;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    ///
    /// </summary>
    public class DistanceCommonUnitTest : BaseUnitTest
    {
        public DistanceCommonUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData(34.7732120000, 113.7388160000, 34.7786680000, 113.7330130000, 10000)]
        public void GetDistance(double lat1, double lng1, double lat2, double lng2, double result)
        {
            var distance = DistanceCommon.GetDistance(lat1, lng1, lat2, lng2);
            Check.True(distance < result, "方法有误");
        }
    }
}
