// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools.Extensions.BloomFilter;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// 布隆过滤器
    /// </summary>
    public class BloomFilterCommonUnitTest : BaseUnitTest
    {
        /// <summary>
        /// GetList
        /// </summary>
        [Fact]
        public void Test()
        {
            int capacity = 2000000;
            var filter = new BloomFilter(capacity, 20000);
            filter.Add("content");
            filter.Add("content2");
            filter.Add("content3");
            filter.Add("content4");
            filter.Add("content5");
            bool res = filter.Contains("content");
            bool res2 = filter.Contains("content12");
        }
    }
}
