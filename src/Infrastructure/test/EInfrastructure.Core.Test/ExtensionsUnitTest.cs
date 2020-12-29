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
            Assert.True(jsonProvider.Serializer(obj) == jsonProvider.Serializer(new
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
            date = DateTime.Parse("2020-12-30 09:58:50");
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
            Assert.True(1609207130L.UnixTimeStampToDateTime() == date);
            date = DateTime.Parse("2020-12-30 09:58:50");
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

        #region 参数1大于参数2

        /// <summary>
        /// 参数1大于参数2
        /// </summary>
        [Fact]
        public void GreaterThan()
        {
            Assert.True(2.GreaterThan(1));
            Assert.False(2.GreaterThan(2));
            Assert.False(2.GreaterThan(3));
        }

        #endregion

        #region 参数1大于参数2

        /// <summary>
        /// 参数1大于参数2
        /// </summary>
        [Fact]
        public void GreaterThanOrEqualTo()
        {
            Assert.True(2.GreaterThanOrEqualTo(1));
            Assert.True(2.GreaterThanOrEqualTo(2));
            Assert.False(2.GreaterThanOrEqualTo(3));
        }

        #endregion

        #region 参数1小于等于参数2

        /// <summary>
        /// 参数1小于等于参数2
        /// </summary>
        [Fact]
        public void LessThan()
        {
            Assert.False(2.LessThan(1));
            Assert.False(2.LessThan(2));
            Assert.True(2.LessThan(3));
        }

        #endregion

        #region 参数1大于参数2

        /// <summary>
        /// 参数1大于参数2
        /// </summary>
        [Fact]
        public void LessThanOrEqualTo()
        {
            Assert.False(2.LessThanOrEqualTo(1));
            Assert.True(2.LessThanOrEqualTo(2));
            Assert.True(2.LessThanOrEqualTo(3));
        }

        #endregion

        #region 判断是否在/不在指定列表内

        /// <summary>
        /// 判断是否在/不在指定列表内
        /// </summary>
        [Fact]
        public void IsIn()
        {
            List<int> list = new List<int>() {1, 2, 3};
            Assert.True(1.IsIn(list));
            Assert.False(10.IsIn(list));

            int[] list2 = new[] {1, 2, 3};
            Assert.True(1.IsIn(list2));
            Assert.False(10.IsIn(list2));

            List<string> list3 = new List<string>() {"1", "2", "3"};
            Assert.True("1".IsIn(list3));
            Assert.False("10".IsIn(list3));

            List<Configurations.UserDto> list4 = new List<Configurations.UserDto>()
            {
                new Configurations.UserDto() {Id = 1, Name = "张三", Sex = 0, Age = 18},
                new Configurations.UserDto() {Id = 2, Name = "张三", Sex = 0, Age = 28}
            };
            var user = new Configurations.UserDto() {Id = 1, Name = "张三", Sex = 0, Age = 18};
            Assert.True(user.IsIn(list4));
            user = new Configurations.UserDto() {Id = 1, Name = "张三2", Sex = 0, Age = 18};
            Assert.True(user.IsIn(list4));
            user = new Configurations.UserDto() {Id = 3, Name = "张三", Sex = 0, Age = 18};
            Assert.False(user.IsIn(list4));
        }

        #endregion

        #region 对可空类型进行判断转换，转换后类型为object

        /// <summary>
        /// 对可空类型进行判断转换，转换后类型为object
        /// </summary>
        [Fact]
        public void ConvertToSpecifyType()
        {
            var str = "123";
            var str2 = (int) str.ConvertToSpecifyType(typeof(int));
            Assert.True(str2 == 123);
        }

        #endregion

        #region 更改源参数类型（适用于简单的类型转换，序列化反序列化不适用）

        /// <summary>
        /// 更改源参数类型（适用于简单的类型转换，序列化反序列化不适用）
        /// </summary>
        [Fact]
        public void ChangeType()
        {
            var str = "123";
            var x = str.ChangeType<int>();
            Assert.True(x == 123);

            var list = new List<string>() {"12", "23"};
            Assert.True(new NewtonsoftJsonProvider().Serializer(list.ChangeType<int>()) == "[12,23]");
        }

        #endregion

        #region obj转Guid

        /// <summary>
        /// obj转Guid
        /// </summary>
        [Fact]
        public void ConvertToGuid()
        {
            var guid = "db4ba62e-2e08-4bdd-8520-ad2ef836ae2f".ConvertToGuid();
            Assert.True(guid == Guid.Parse("db4ba62e-2e08-4bdd-8520-ad2ef836ae2f"));

            guid = "".ConvertToGuid();
            Assert.True(guid == null);

            var guid2 = "".ConvertToGuid(Guid.Empty);
            Assert.True(guid2 == Guid.Empty);
        }

        #endregion

        #region obj转Short

        /// <summary>
        /// obj转Short
        /// </summary>
        [Fact]
        public void ConvertToShort()
        {
            var s = "db4ba62e-2e08-4bdd-8520-ad2ef836ae2f".ConvertToShort();
            Assert.True(s == null);

            s = "1".ConvertToShort();
            Assert.True(s == 1);

            var s2 = "s".ConvertToShort(0);
            Assert.True(s2 == 0);
        }

        #endregion

        #region obj转long

        /// <summary>
        /// obj转long
        /// </summary>
        [Fact]
        public void ConvertToLong()
        {
            var s = "db4ba62e-2e08-4bdd-8520-ad2ef836ae2f".ConvertToLong();
            Assert.True(s == null);

            s = "12342".ConvertToLong();
            Assert.True(s == 12342);

            var s2 = "ad2ef836ae2f".ConvertToShort(0);
            Assert.True(s2 == 0);
        }

        #endregion

        #region obj转decimal

        /// <summary>
        /// obj转decimal
        /// </summary>
        [Fact]
        public void ConvertToDecimal()
        {
            var s = "db4ba62e-2e08-4bdd-8520-ad2ef836ae2f".ConvertToDecimal();
            Assert.True(s == null);

            s = "12342".ConvertToDecimal();
            Assert.True(s == 12342);

            decimal s2 = "ad2ef836ae2f".ConvertToDecimal(1.0m);
            Assert.True(s2 == 1.0m);
        }

        #endregion

        #region obj转double

        /// <summary>
        /// obj转double
        /// </summary>
        [Fact]
        public void ConvertToDouble()
        {
            var s = "db4ba62e-2e08-4bdd-8520-ad2ef836ae2f".ConvertToDouble();
            Assert.True(s == null);

            s = "12342".ConvertToDouble();
            Assert.True(s == 12342);

            double s2 = "ad2ef836ae2f".ConvertToDouble(1.0d);
            Assert.True(s2 == 1.0d);
        }

        #endregion

        #region obj转float

        /// <summary>
        /// obj转float
        /// </summary>
        [Fact]
        public void ConvertToFloat()
        {
            var s = "db4ba62e-2e08-4bdd-8520-ad2ef836ae2f".ConvertToFloat();
            Assert.True(s == null);

            s = "12342".ConvertToFloat();
            Assert.True(s == 12342);

            double s2 = "ad2ef836ae2f".ConvertToFloat(1.0f);
            Assert.True(s2 == 1.0f);
        }

        #endregion

        #region obj转datetime

        /// <summary>
        /// obj转datetime
        /// </summary>
        [Fact]
        public void ConvertToDateTime()
        {
            var s = "db4ba62e-2e08-4bdd-8520-ad2ef836ae2f".ConvertToDateTime();
            Assert.True(s == null);

            s = "2020-12-12".ConvertToDateTime();
            Assert.True(s == DateTime.Parse("2020-12-12"));

            DateTime s2 = "ad2ef836ae2f".ConvertToDateTime(DateTime.Parse("2020-12-12"));
            Assert.True(s2 == DateTime.Parse("2020-12-12"));
        }

        #endregion

        #region obj转byte

        /// <summary>
        /// obj转byte
        /// </summary>
        [Fact]
        public void ConvertToByte()
        {
            var s = 'c'.ConvertToByte();
            Assert.True(s == null); //转换失败，结果为默认值null

            byte? s2 = 2.ConvertToByte();
            Assert.True(s2 == 2); //转换成功，结果为2


            byte? s3 = "".ConvertToByte(2);
            Assert.True(s3 == 2); //转换成功，结果为自定义默认值2
        }

        #endregion

        #region obj转sbyte

        /// <summary>
        /// obj转sbyte
        /// </summary>
        [Fact]
        public void ConvertToSByte()
        {
            var s = 'c'.ConvertToSByte();
            Assert.True(s == null); //转换失败，结果为默认值null

            sbyte? s2 = 2.ConvertToSByte();
            Assert.True(s2 == 2); //转换成功，结果为2


            sbyte? s3 = "".ConvertToSByte(2);
            Assert.True(s3 == 2); //转换成功，结果为自定义默认值2
        }

        #endregion

        #region obj转bool（其中0、不、否、失败、no、fail、lose、false为false，1、是、ok、yes、success、pass、true、成功为true，判断不区分大小写）

        /// <summary>
        /// obj转bool（其中0、不、否、失败、no、fail、lose、false为false，1、是、ok、yes、success、pass、true、成功为true，判断不区分大小写）
        /// </summary>
        [Fact]
        public void ConvertToBool()
        {
            var s = "是".ConvertToBool();
            Assert.True(s == true); //转换成功

            var s2 = "".ConvertToBool();
            Assert.True(s2 == null); //转换失败，结果为默认值null

            var s3 = 0.ConvertToBool();
            Assert.False(s3.Value); //转换成功，结果为false

            var s4 = 2.ConvertToBool(false);
            Assert.True(s4 == false); //转换失败，结果为默认值false
        }

        #endregion

        #region 返回数字的绝对值

        /// <summary>
        /// 返回数字的绝对值
        /// </summary>
        [Fact]
        public void Abs()
        {
            Assert.True((-1.5).Abs() == 1.5);
            Assert.True((-1).Abs() == 1);

            var list = new List<int>() {1, 2, -100};

            Assert.True(new NewtonsoftJsonProvider().Serializer(list.Abs()) == "[1,2,100]");
        }

        #endregion
    }
}
