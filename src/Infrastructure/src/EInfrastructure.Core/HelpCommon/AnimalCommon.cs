// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Configuration.Enum;

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
            List<string> animateList = EnumCommon.ToDescriptionDictionary<AnimalEnum>().Select(x => x.Value).ToList();
            int tmp = year - 2008;
            if (year < 2008)
                return animateList[tmp % 12 + 12];
            return animateList[tmp % 12];
        }
        #endregion

        #region 得到生肖枚举
        /// <summary>
        /// 得到生肖枚举
        /// </summary>
        /// <param name="year">年</param>
        /// <returns></returns>
        public static AnimalEnum GetAnimalEnumFromBirthday(int year)
        {
            int tmp = year - 2008;
            if (year < 2008)
                return (AnimalEnum)(tmp % 12 + 12);
            return (AnimalEnum)(tmp % 12);
        }
        #endregion
    }
}
