// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Enumerations;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// 扩展
    /// </summary>
    public class ExtensionsUnitTest : BaseUnitTest
    {
         #region 保留指定位数(默认四舍五入)

        /// <summary>
        /// 保留指定位数(默认四舍五入)
        /// </summary>
        [Fact]
        public void ToFixed()
        {
            var dec = 4.554d;
            var dec2 = 4.555d;
            Assert.True(dec.ToFixed(2) == "4.55");
            Assert.True(dec2.ToFixed(2) == "4.56");
        }

        #endregion

        #region 转换为对象

        /// <summary>
        /// 转换为对象
        /// </summary>
        [Fact]
        public void ConvertToObject()
        {
            List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("Id", 1007),
                new KeyValuePair<string, object>("Name", "张三")
            };
            var obj = list.ConvertToObject();
            var jsonProvider = new NewtonsoftJsonProvider();
            Assert.True(jsonProvider.Serializer( obj) == jsonProvider.Serializer(new
            {
                Id = 1007,
                Name = "张三"
            }));
        }

        #endregion

        #region List转换为string

        /// <summary>
        /// List转换为string
        /// </summary>
        [Fact]
        public void ConvertToString()
        {
            List<int> list = new List<int>() {1, 2};
            var str = list.ConvertToString(',');
            Assert.True(str == "1,2");
        }

        #endregion

        #region 获取list列表的数量

        /// <summary>
        /// 获取list列表的数量
        /// </summary>
        [Fact]
        public void GetListCount()
        {
            List<string> list = null;
            Assert.True(list.GetListCount() == 0);
            list = new List<string>() {"test"};
            Assert.True(list.GetListCount() == 1);
        }

        #endregion

        #region 验证列表是否为空

        /// <summary>
        /// 验证列表是否为空
        /// </summary>
        [Fact]
        public void IsNullOrEmpty()
        {
            List<string> list = null;
            Assert.True(list.IsNullOrEmpty());
        }

        #endregion

        #region 得到安全字符串

        /// <summary>
        /// 得到安全字符串
        /// </summary>
        [Fact]
        public void SafeString()
        {
            string str = null;
            Assert.True(str.SafeString() == string.Empty);
            Assert.True(str.SafeString() == "");
        }

        #endregion

        #region 字符串增加前缀

        /// <summary>
        /// 字符串增加前缀
        /// </summary>
        [Fact]
        public void AddPre()
        {
            string str = null;
            Assert.True(str.AddPrefix("te_") == "");
            str = "test";
            Assert.True(str.AddPrefix("te_") == "te_test");
        }

        #endregion

        #region 字符串增加后缀

        /// <summary>
        /// 字符串增加后缀
        /// </summary>
        [Fact]
        public void AddSuf()
        {
            string str = null;
            Assert.True(str.AddSuffix("_te") == "");
            str = "test";
            Assert.True(str.AddSuffix("_te") == "test_te");
        }

        #endregion

        #region 补足位数，指定字符串的固定长度，如果字符串小于固定长度，则在字符串的前面补足零，可设置的固定长度最大为9位

        /// <summary>
        /// 补足位数，指定字符串的固定长度，如果字符串小于固定长度，则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        [Fact]
        public void RepairZero()
        {
            string str = null;
            Assert.True(str.SafeString().RepairZero(5) == "00000");
            str = "1";
            Assert.True(str.SafeString().RepairZero(5) == "00001");
            str = "123466";
            Assert.True(str.SafeString().RepairZero(5) == "123466");
        }

        #endregion

        #region 得到字符串长度（一个汉字占用两个字符）

        /// <summary>
        /// 得到字符串长度（一个汉字占用两个字符）
        /// </summary>
        [Fact]
        public void GetLength()
        {
            string str = null;
            Assert.True(str.SafeString().GetLength() == 0);
            str = "12345测试";
            Assert.True(str.SafeString().GetLength() == 9);
        }

        #endregion

        #region 字符串转化为泛型集合

        /// <summary>
        /// 字符串转化为泛型集合
        /// </summary>
        [Fact]
        public void ConvertToList()
        {
            var str = "1,23,12";
            var list = str.ConvertToList<int>(',');
            str = "12te23te45";
            var list2 = str.ConvertToList<string>("te");
        }

        #endregion

        #region 获取字符第出现的下标位置

        /// <summary>
        /// 获取字符第出现的下标位置
        /// </summary>
        [Fact]
        public void IndexOfNumber()
        {
            var s = "HelloWorld".IndexOfNumber("o", 2);
            var s2 = "HelloWorld".IndexOfNumber("ll", 2);
            var s3 = "HelloWorld".IndexOfNumber('o', 3);
            Assert.True(s == 6);
            Assert.True(s2 == -1);
            Assert.True(s3 == -1);
        }

        #endregion

        #region 得到倒数第number次出现character的位置下标

        /// <summary>
        /// 得到倒数第number次出现character的位置下标
        /// </summary>
        [Fact]
        public void LastIndexOfNumber()
        {
            var s = "HelloWorld".LastIndexOfNumber('o', 1);
            var s2 = "HelloWorld".LastIndexOfNumber("rd", 2);
            Assert.True(s == 6);
            Assert.True(s2 == -1);
        }

        #endregion

        #region 分割

        /// <summary>
        /// 分割
        /// </summary>
        [Fact]
        public void Split()
        {
            var str = "1,2,,3,4";
            var strArray = str.Split(",", true);
            Assert.True(strArray.ConvertToString(',') == "1,2,3,4");
        }

        #endregion

        #region 判断字符串是否全部相等

        /// <summary>
        /// 判断字符串是否全部相等
        /// </summary>
        [Fact]
        public void IsEqualNumber()
        {
            var str = "1,23,23";
            Assert.False(str.IsEqualNumber());
            str = "111111";
            Assert.True(str.IsEqualNumber());
        }

        #endregion

        #region 是否身份证号码，支持15、18位

        /// <summary>
        /// 是否身份证号码，支持15、18位
        /// </summary>
        [Fact]
        public void IsIdCard()
        {
            var idCard = "41078219931007914x";
            Assert.False(idCard.IsIdCard());
        }

        #endregion

        #region 生成时间戳

        /// <summary>
        /// 生成时间戳
        /// </summary>
        [Fact]
        public void ToUnixTimestamp()
        {
            var date = DateTime.Parse("2020-12-29 09:58:50");
            Assert.True(date.ToUnixTimestamp(TimestampType.Second) == 1609207130);
            date=DateTime.Parse("2020-12-30 09:58:50");
            Assert.True(date.ToUnixTimestamp(TimestampType.Millisecond) == 1609293530000);
        }

        #endregion

        #region 生成时间戳

        /// <summary>
        /// 生成时间戳
        /// </summary>
        [Fact]
        public void UnixTimeStampToDateTime()
        {
            var date = DateTime.Parse("2020-12-29 09:58:50");
            Assert.True(1609207130L.UnixTimeStampToDateTime() ==date );
            date=DateTime.Parse("2020-12-30 09:58:50");
            Assert.True(1609293530000L.UnixTimeStampToDateTime() == date);
        }

        #endregion

        #region 判断值是否在指定范围内

        /// <summary>
        /// 判断值是否在指定范围内
        /// </summary>
        [Fact]
        public void InRange()
        {
            int x = 2;
            int min = 1, max = 4;
            Assert.True(x.InRange(min, max));
            x = 0;
            Assert.False(x.InRange(min, max));
            x = 5;
            max = 5;
            Assert.False(x.InRange(min, max, RangeMode.Close));
            Assert.True(x.InRange(min, max, RangeMode.CloseOpen));
            x = 1;
            Assert.False(x.InRange(min, max, RangeMode.CloseOpen));
            Assert.True(x.InRange(min, max, RangeMode.OpenClose));
            x = 5;
            Assert.False(x.InRange(min, max, RangeMode.OpenClose));
        }

        #endregion
    }
}
