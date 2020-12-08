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

        #region 判断是否全部相等

        /// <summary>
        /// 判断数字是否全部相等
        /// </summary>
        /// <param name="number">待验证的数字</param>
        /// <returns></returns>
        public static bool IsEqualNumber(this int number)
        {
            int[] num = number.ToString().Select(s => int.Parse(s.ToString())).ToArray();
            for (int i = 0; i < num.Length - 1; i++)
            {
                if (num[i] != num[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 补足位数

        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// </summary>
        /// <param name="number">原数字</param>
        /// <param name="limitedLength">固定几位长度</param>
        public static string RepairZero(this int number, int limitedLength)
        {
            //补足0的字符串
            string temp = "";

            //补足0
            for (int i = 0; i < limitedLength - number.ToString().Length; i++)
            {
                temp += "0";
            }

            return temp + number;
        }

        #endregion
    }
}
