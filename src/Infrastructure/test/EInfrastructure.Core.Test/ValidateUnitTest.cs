// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Test.Base;
using Xunit;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// 验证方法
    /// </summary>
    public class ValidateUnitTest : BaseUnitTest
    {
        public ValidateUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData("450000", true)]
        [InlineData("4500002", false)]
        public void IsZipCode(string code, bool result)
        {
            Check.True(code.IsZipCode() == result, "方法异常");
        }
    }
}
