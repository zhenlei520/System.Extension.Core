using System;
using System.Collections.Generic;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Enumerable;
using EInfrastructure.Core.Tools.Systems;
using Xunit;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    ///
    /// </summary>
    public class StringCommonUnitTest : BaseUnitTest
    {
        public StringCommonUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void EncryptStr()
        {
            var str = StringCommon.EncryptStr("13653107777", "*", 3, 4); //136****7777
            var str2 = StringCommon.HideMobile("03736793777");
            var str3 = StringCommon.HideMobile("0373-6793777");
            var str4 = StringCommon.HideMobile("037-6793777");
            var str5 = StringCommon.HideMobile("6793777");
        }

        [Fact]
        public void DistinctStringArray()
        {
            List<string> list = new List<string>()
            {
                "", "123"
            };
            var stringArray = StringCommon.DistinctStringArray(list.ToArray(), 2);
        }
    }
}
