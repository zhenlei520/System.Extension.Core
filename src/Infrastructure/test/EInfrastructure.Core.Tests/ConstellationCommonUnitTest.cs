// Copyright (c) zhenlei520 All rights reserved.

using System;
using EInfrastructure.Core.Tests.Base;
using EInfrastructure.Core.Tools;
using Xunit;

namespace EInfrastructure.Core.Tests
{
    /// <summary>
    ///
    /// </summary>
    public class ConstellationCommonUnitTest : BaseUnitTest
    {
        /// <summary>
        /// 根据日期得到星座名称
        /// </summary>
        /// <param name="birthdayStr">日期</param>
        /// <param name="result"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(null, "未知")]
        [InlineData("1994-11-09", "天蝎座")]
        [InlineData("1993-04-17", "白羊座")]
        public void GetConstellationFromBirthday(string birthdayStr, string result)
        {
            DateTime? birthday = null;
            if (!string.IsNullOrEmpty(birthdayStr))
                birthday = DateTime.Parse(birthdayStr);

            Check.True((birthday.GetConstellationFromBirthday()?.Name ?? "") == result,
                "方法有误");
        }

        /// <summary>
        /// 根据日期得到星座名称
        /// </summary>
        /// <param name="birthday">日期</param>
        /// <param name="result"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("", 13)]
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
