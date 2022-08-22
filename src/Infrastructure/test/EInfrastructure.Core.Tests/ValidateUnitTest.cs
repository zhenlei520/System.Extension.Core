// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.Tests.Base;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Enumerations;
using Xunit;

namespace EInfrastructure.Core.Tests
{
    /// <summary>
    /// 验证方法
    /// </summary>
    public class ValidateUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData("450000", true)]
        [InlineData("4500002", false)]
        public void IsZipCode(string param, bool result)
        {
            Check.True(param.IsZipCode() == result, "方法异常");
        }

        [Theory]
        [InlineData("wangzhenlei520@gmail.com", true)]
        public void IsEmail(string param, bool result)
        {
            Check.True(param.IsEmail() == result, "方法异常");
        }

        [Theory]
        [InlineData("13653777777", true)]
        public void IsMobile(string param, bool result)
        {
            Check.True(param.IsMobile() == result, "方法异常");
        }

        [Theory]
        [InlineData("0373-66793210", true)]
        [InlineData("66793210", true)]
        [InlineData("13653777777@qq.com", false)]
        public void IsPhone(string param, bool result)
        {
            Check.True(param.IsPhone() == result, "方法异常");
        }

        [Theory]
        [InlineData("123asd", false)]
        [InlineData("+123", true)]
        [InlineData("123", true)]
        [InlineData("++123", false)]
        [InlineData("+-123", false)]
        [InlineData("-123", true)]
        [InlineData("--123", false)]
        [InlineData("asd12", false)]
        [InlineData("asd", false)]
        public void IsNumber(string param, bool result)
        {
            Check.True(param.IsNumber(NumericType.Minus) == result, "方法异常");
        }

        [Theory]
        [InlineData("410782199310069653", true)]
        [InlineData("410782199310069652", false)]
        public void IsIdCard(string param, bool result)
        {
            Check.True(param.IsIdCard() == result, "方法异常");
        }

        [Theory]
        [InlineData("77d99c08-a192-4a16-80dd-5e50f6e1c2d2", true)]
        [InlineData("77d99c08-a192-4a16-80dd5e50f6e1-c2d2", false)]
        public void IsGuid(string param, bool result)
        {
            Check.True(param.IsGuid() == result, "方法异常");
        }

        [Theory]
        [InlineData("192.168.1.124", true)]
        [InlineData("a.d.1.124", false)]
        public void IsIp(string param, bool result)
        {
            Check.True(param.IsIp() == result, "方法异常");
        }

        [Theory]
        [InlineData("http://github.com/zhenlei520", true)]
        [InlineData("https://github.com/zhenlei520", true)]
        [InlineData("github.com/zhenlei520", true)]
        [InlineData("百度", false)]
        public void IsUrl(string param, bool result)
        {
            Check.True(param.IsUrl() == result, "方法异常");
        }

        [Theory]
        [InlineData("github.com/zhenlei520", false)]
        [InlineData("百度", true)]
        public void IsChinese(string param, bool result)
        {
            Check.True(param.IsChinese() == result, "方法异常");
        }

        [Theory]
        [InlineData("0.1210", 2, false)]
        [InlineData("0.120", 2, true)]
        [InlineData("0.12", 2, true)]
        [InlineData("0.1", 2, true)]
        [InlineData("2", 2, true)]
        public void IsMaxScale(string str, int scale, bool state)
        {
            Check.True(str.IsMaxScale(scale) == state, "状态不一致");
        }
    }
}
