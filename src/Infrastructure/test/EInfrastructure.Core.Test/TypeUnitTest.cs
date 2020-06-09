// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Http;
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
        public void ConvertToBool(string str, bool res)
        {
            Check.True(str.ConvertToBool() == res, "方法有误");
        }

        /// <summary>
        /// 得到文件的base64
        /// </summary>
        /// <param name="url"></param>
        [Theory]
        [InlineData(
            "http://t8.baidu.com/it/u=1484500186,1503043093&fm=79&app=86&size=h300&n=0&g=4n&f=jpeg?sec=1592303979&t=d945f1509afab787aa33a3519aa51ba4")]
        public void GetBase64String(string url)
        {
            var stream = new HttpClient("http://t8.baidu.com").GetStream(
                "it/u=1484500186,1503043093&fm=79&app=86&size=h300&n=0&g=4n&f=jpeg?sec=1592303979&t=d945f1509afab787aa33a3519aa51ba4");
            string base64 = stream.ConvertToBase64();
        }
    }
}
