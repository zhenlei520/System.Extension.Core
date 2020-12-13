using System.Collections.Generic;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Common;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    ///
    /// </summary>
    public class StringCommonUnitTest : BaseUnitTest
    {
        [Fact]
        public void EncryptStr()
        {
            var str = "13653107777".EncryptSpecialStr("*", 3, 4); //136****7777
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
            var s3 = "123,1234,".IndexOf(',', 2, 3);
            var s = "123,1234,4323,2".LastIndexOf(',', 2);
            var s5 = "123,1234,4323,2,4".LastIndexOf(',', 4);
            var s6 = "123,1234,4323,2".LastIndexOf(',', 3);
            var s2 = "123,1234,4323,2".IndexOf(',', 2);
            var s4 = "123,1234,4323,2".IndexOf(',', 3);
            var s7 = "123,1234,4323,2,3,2".IndexOf(',', 4);
            var s9 = "123,12341,,4323,2".LastIndexOf(',', 2);
        }

        [Theory]
        [InlineData("github挺好使的")]
        [InlineData("wangzhenlei520@gmail.com")]
        public void GetLength(string str)
        {
            int length = str.GetLength();
        }
    }
}
