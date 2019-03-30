// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Exception;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 校验方法
    /// </summary>
    public static class Check
    {
        #region 检查空或者null

        /// <summary>
        /// 检查空或者null
        /// </summary>
        /// <param name="str"></param>
        /// <param name="message"></param>
        /// <param name="action"></param>
        public static void IsNullOrEmptyTip(this string str, string message, Func<bool> action = null)
        {
            if (string.IsNullOrEmpty(str))
            {
                if (action == null || action.Invoke())
                {
                    throw new BusinessException(message);
                }
            }
        }

        #endregion

        #region 检查是否空或者null

        /// <summary>
        /// 检查是否空或者null
        /// </summary>
        /// <param name="array"></param>
        /// <param name="message">异常信息</param>
        /// <param name="action">委托，是否执行系统预设的异常</param>
        /// <exception cref="Exception"></exception>
        public static void IsNullOrEmptyTip(this object array, string message, Func<bool> action = null)
        {
            if (array == null)
            {
                if (action == null || action.Invoke())
                {
                    throw new BusinessException(message);
                }
            }
        }

        /// <summary>
        /// 检查是否空或者null
        /// </summary>
        /// <param name="array"></param>
        /// <param name="message">异常信息</param>
        /// <param name="action">委托，是否执行系统预设的异常</param>
        /// <exception cref="Exception"></exception>
        public static void IsNullOrEmptyTip(this object[] array, string message, Func<bool> action = null)
        {
            if (array == null || array.Length == 0)
            {
                if (action == null || action.Invoke())
                {
                    throw new System.Exception(message);
                }
            }
        }

        #endregion

        #region 检查是否为假

        /// <summary>
        /// 检查是否为假
        /// </summary>
        /// <param name="status"></param>
        /// <param name="errorMessageFormat"></param>
        public static void True(bool status, string errorMessageFormat)
        {
            if (!status)
            {
                throw new BusinessException(string.Format(errorMessageFormat));
            }
        }

        #endregion

        #region 判断是否是淘口令

        /// <summary>
        /// 判断是否是淘口令
        /// </summary>
        /// <param name="str">待校验的字符串</param>
        /// <param name="code">淘口令</param>
        /// <returns></returns>
        public static bool IsAmoyPsdTip(string str, ref string code)
        {
            Regex reg = new Regex("(￥|€|《).*(￥|€|《)", RegexOptions.Multiline);
            MatchCollection matchs = reg.Matches(str);
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    code = item.Value;
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}