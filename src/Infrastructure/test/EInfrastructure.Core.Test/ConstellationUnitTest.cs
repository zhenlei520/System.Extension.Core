// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Tools;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// Constellation Unit Test
    /// </summary>
    public class ConstellationUnitTest
    {
        #region Get the constellation

        /// <summary>
        /// Get the constellation
        /// </summary> [Theory]
        [Theory]
        [InlineData("1993-04-18")]
        [InlineData("1994-11-09")]
        public void GetConstellationFromBirthday(string date)
        {
            Check.True(ConstellationCommon.GetConstellationFromBirthday(DateTime.Parse(date)) == GetConstellation(date),"constellation is error");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetConstellation(string date)
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("1993-04-18", "白羊座"),
                new KeyValuePair<string, string>("1994-11-09", "天蝎座")
            }.Where(x => x.Key == date).Select(x => x.Value).FirstOrDefault();
        } 

        #endregion
    }
}