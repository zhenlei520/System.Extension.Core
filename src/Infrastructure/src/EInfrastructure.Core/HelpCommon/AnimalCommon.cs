// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using EInfrastructure.Core.Configuration.Enumeration;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 生肖
    /// </summary>
    public class AnimalCommon
    {
        #region 得到生肖

        /// <summary>
        /// 得到生肖
        /// </summary>
        /// <param name="year">年</param>
        /// <returns></returns>
        public static string GetAnimalFromBirthday(int year)
        {
            var animateList = Animal.GetAll<Animal>();
            int tmp = year - 2008;
            if (year < 2008)
                return animateList.Where(x => x.Id == tmp % 12 + 12).Select(x => x.Name).FirstOrDefault();
            return animateList.Where(x => x.Id ==tmp % 12).Select(x=>x.Name).FirstOrDefault();
        }

        #endregion

        #region 得到生肖枚举

        /// <summary>
        /// 得到生肖枚举
        /// </summary>
        /// <param name="year">年</param>
        /// <returns></returns>
        public static Animal GetAnimalEnumFromBirthday(int year)
        {
            int tmp = year - 2008;
            if (year < 2008)
                return Animal.GetAll<Animal>().FirstOrDefault(x => x.Id == tmp % 12 + 12);
            return Animal.GetAll<Animal>().FirstOrDefault(x => x.Id == tmp % 12);
        }

        #endregion
    }
}
