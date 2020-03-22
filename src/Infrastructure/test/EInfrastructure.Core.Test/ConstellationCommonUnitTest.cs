// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using Xunit;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    ///
    /// </summary>
    public class ConstellationCommonUnitTest : BaseUnitTest
    {
        public ConstellationCommonUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// 根据日期得到星座名称
        /// </summary>
        /// <param name="birthday">日期</param>
        /// <returns></returns>
        [Theory]
        [InlineData(null, "未知")]
        [InlineData("1994-11-09", "天蝎座")]
        [InlineData("1993-04-17", "白羊座")]
        public void GetConstellationFromBirthday(string birthday, string result)
        {
            Check.True((ConstellationCommon.GetConstellationFromBirthday(DateTime.Parse(birthday))?.Name??"") == result,
                "方法有误");
        }

        /// <summary>
        /// 根据日期得到星座名称
        /// </summary>
        /// <param name="birthday">日期</param>
        /// <returns></returns>
        [Theory]
        [InlineData("", -2)]
        [InlineData("1994-11-09", 8)]
        [InlineData("1993-04-17", 1)]
        public void GetConstellationEnumFromBirthday(string birthday, int result)
        {
            Check.True(
                birthday.ConvertToDateTime(null).GetConstellationFromBirthday().Id == result,
                "方法有误");
        }
    }
}
