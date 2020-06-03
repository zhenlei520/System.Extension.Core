// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using Xunit;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Test
{
    public class TypeUnitTest : BaseUnitTest
    {
        public TypeUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData(1, 1)]
        public void ConvertToShort(int num, short s)
        {
            Check.True(1.ConvertToShort() == s, "方法有误");
        }

        [Theory]
        [InlineData("是", true)]
        [InlineData("否", false)]
        [InlineData("TRUE", true)]
        [InlineData("True", true)]
        [InlineData("true", true)]
        [InlineData("FALSE", false)]
        [InlineData("false", false)]
        [InlineData("False", false)]
        public void ConvertToBool(string str,bool res)
        {
            Check.True(str.ConvertToBool() == res, "方法有误");
        }

    }
}
