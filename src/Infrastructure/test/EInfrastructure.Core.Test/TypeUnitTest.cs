// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using Xunit;

namespace EInfrastructure.Core.Test
{
    public class TypeUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(1, 1)]
        public void ConvertToShort(int num, short s)
        {
            Check.True(1.ConvertToShort() == s, "方法有误");
        }
    }
}
