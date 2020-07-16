// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 特殊需求，针对编号做处理
    /// 此类严格执行(方法设置>初始化预设>系统预设)的规则
    /// </summary>
    public static class NumberTypeCommon
    {
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
