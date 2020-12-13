// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Http;
using EInfrastructure.Core.Infrastructure;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Common;
using Xunit;

namespace EInfrastructure.Core.Test
{
    public class TypeUnitTest : BaseUnitTest
    {
        [Theory]
        [InlineData(1, 1)]
        public void ConvertToShort(int num, short s)
        {
            var assemblies = AssemblyProvider.GetDefaultAssemblyProvider.GetAssemblies();
            var type=new TypeFinder().FindClassesOfType<ISingleInstance>();
            Check.True(num.ConvertToShort() == s, "方法有误");
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
            Uri uri=new Uri(url);
            string host = $"{uri.Scheme}://{uri.Host}";
            var stream = new HttpClient(host).GetStream(
                url.Replace(host, ""));
            string base64 = stream.ConvertToBase64();
        }
    }
}
