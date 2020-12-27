// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    ///
    /// </summary>
    public class DecimalUnitTest: BaseUnitTest
    {
        [Fact]
        public void ToFixed()
        {
            var str = 1.12562m.ToFixed(2);
            var str2 = 1.12462m.ToFixed(2);
            var str3 = 1.1m.ToFixed(2);
            Assert.True(str=="1.13");
            Assert.True(str2=="1.12");
            Assert.True(str3=="1.10");
        }
    }
}
