// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 生肖
    /// </summary>
    public static class AnimalCommon
    {
        #region 得到生肖信息

        /// <summary>
        /// 得到生肖信息
        /// </summary>
        /// <param name="year">年</param>
        /// <returns></returns>
        public static Animal GetAnimalFromBirthday(this int year)
        {
            var index = (year - 3) % 12;
            if (index == 0)
            {
                index = 12;
            }
            return Animal.GetAll<Animal>().FirstOrDefault(x => x.Id == index);
        }

        #endregion

        #region 得到生肖信息

        /// <summary>
        /// 得到生肖信息 如果身份证号码错误，则返回Null
        /// </summary>
        /// <param name="cardNo">身份证号</param>
        /// <returns></returns>
        public static Animal GetAnimalFromCardNo(this string cardNo)
        {
            if (!cardNo.IsIdCard())
            {
                return null;
            }

            var birthday = cardNo.GetBirthday();
            return birthday != null ? GetAnimalFromBirthday(birthday.Value.Year) : null;
        }

        #endregion
    }
}
