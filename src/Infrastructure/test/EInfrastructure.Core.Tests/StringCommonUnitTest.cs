using System.Collections.Generic;
using EInfrastructure.Core.Tests.Base;
using EInfrastructure.Core.Tools;
using Xunit;

namespace EInfrastructure.Core.Tests
{
    /// <summary>
    ///
    /// </summary>
    public class StringCommonUnitTest : BaseUnitTest
    {
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

        [Fact]
        public void LastIndexOf()
        {
            var s3="123,1234,".IndexOf(',',2,3);
            var s = StringCommon.LastIndexOf("123,1234,4323,2", ',', 2);
            var s5 = StringCommon.LastIndexOf("123,1234,4323,2,4", ',', 4);
            var s6 = StringCommon.LastIndexOf("123,1234,4323,2", ',', 3);
            var s2 = StringCommon.IndexOf("123,1234,4323,2", ',', 2);
            var s4 = StringCommon.IndexOf("123,1234,4323,2", ',', 3);
            var s7 = StringCommon.IndexOf("123,1234,4323,2,3,2", ',', 4);
            var s9=StringCommon.LastIndexOf("123,12341,,4323,2", ',', 2);
        }
    }
}
