// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using EInfrastructure.Core.Configuration.Enumeration;
using EInfrastructure.Core.HelpCommon;
using Xunit;

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
        [InlineData(1993, "鸡")]
        [InlineData(1998, "虎")]
        [InlineData(2008, "鼠")]
        public void GetAnimal(int year, string animal)
        {
            Check.True(AnimalCommon.GetAnimalFromBirthday(year) == animal, "animal is error");
        }

        #endregion

        [Theory]
        [InlineData(1993, 9)]
        public void GetAnimalEnumFromBirthday(int year, int animal)
        {
            Check.True(
                AnimalCommon.GetAnimalEnumFromBirthday(year) ==
                Animal.GetAll<Animal>().FirstOrDefault(x => x.Id == animal), "方法异常");
        }
    }
}
