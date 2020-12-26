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
        #region 加密隐藏信息

        /// <summary>
        /// 加密隐藏信息
        /// </summary>
        [Fact]
        public void EncryptStr()
        {
            var str = "13653107777".EncryptSpecialStr("*", 3, 4); //136****7777
            var str2 = StringCommon.HideMobile("03736793777");
            var str3 = StringCommon.HideMobile("0373-6793777");
            var str4 = StringCommon.HideMobile("037-6793777");
            var str5 = StringCommon.HideMobile("6793777");
        }

        #endregion

        #region 进制转换

        /// <summary>
        /// 进制转换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="res"></param>
        [Theory]
        [InlineData("10", 10, 2, "1010")]
        [InlineData("12", 8, 10, "10")]
        public void ConvertBinary(string str, int from, int to, string res)
        {
            Assert.True(res == str.ConvertBinary(from, to));
        }

        #endregion

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
            var str = "helloworld";
            var from = "llo";
            var to = "rl";
            var res = str.SubstringFrom(from,false);
            var res2 = str.SubstringFrom(from,true);
            var res3 = str.SubstringTo(to,true);

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
