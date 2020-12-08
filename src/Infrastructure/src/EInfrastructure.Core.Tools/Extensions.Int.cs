// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// int扩展
    /// </summary>
    public partial class Extensions
    {
        #region 得到生肖信息

        /// <summary>
        /// 得到生肖信息
        /// </summary>
        /// <param name="year">年</param>
        /// <returns></returns>
        public static Animal GetAnimal(this int year)
        {
            if (year < 1582 || year > 2099)
            {
                return null;
            }

            var index = (year - 3) % 12;
            if (index == 0)
            {
                index = 12;
            }

            return Animal.GetAll<Animal>().FirstOrDefault(x => x.Id == index);
        }

        #endregion

        #region 判断param的值是否在枚举中

        /// <summary>
        /// 判断值是否在枚举中
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool IsExist<T>(this int param) where T : Enum
        {
            return Enum.IsDefined(typeof(T), param);
        }

        /// <summary>
        /// 判断值是否在枚举中
        /// </summary>
        /// <param name="enumValue">需要判断的参数</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsExist(this int enumValue, Type type)
        {
            return Enum.IsDefined(type, enumValue);
        }

        #endregion
    }
}
