// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.HelpCommon;
using Xunit;
using Assert = Xunit.Assert;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// Animal Test
    /// </summary>
    public class AnimalUnitTest
    {
        #region Get into Chinese zodiac

        /// <summary>
        /// Get into Chinese zodiac
        /// </summary>
        /// <param name="year"></param>
        [Theory]
        [InlineData(1993)]
        [InlineData(1998)]
        [InlineData(2008)]
        public void GetAnimal(int year)
        {
            Assert.True(AnimalCommon.GetAnimalFromBirthday(year) == GetAnimal2(year));
        }

        /// <summary>
        /// Get into Chinese zodiac
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private string GetAnimal2(int year)
        {
            return new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1993, "鸡"),
                new KeyValuePair<int, string>(1998, "虎"),
                new KeyValuePair<int, string>(2008, "鼠")
            }.Where(x => x.Key == year).Select(x => x.Value).FirstOrDefault();
        }

        #endregion
        
        
    }
}