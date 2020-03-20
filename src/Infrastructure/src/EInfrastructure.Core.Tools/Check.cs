// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Text.RegularExpressions;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;

namespace EInfrastructure.Core.Tools
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
        /// <param name="errCode">错误码</param>
        public static void IsNullOrEmptyTip(this string str, string message, Func<bool> action = null,
            int? errCode = null)
        {
            if (string.IsNullOrEmpty(str))
            {
                if (action == null || action.Invoke())
                {
                    throw new BusinessException(message, errCode ?? HttpStatus.Err.Id);
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
        /// <param name="errCode">错误码</param>
        /// <exception cref="Exception"></exception>
        public static void IsNullOrEmptyTip(this object array, string message, Func<bool> action = null,
            int? errCode = null)
        {
            if (array == null)
            {
                if (action == null || action.Invoke())
                {
                    throw new BusinessException(message, errCode ?? HttpStatus.Err.Id);
                }
            }
        }

        /// <summary>
        /// 检查是否空或者null
        /// </summary>
        /// <param name="array"></param>
        /// <param name="message">异常信息</param>
        /// <param name="action">委托，是否执行系统预设的异常</param>
        /// <param name="errCode">错误码</param>
        /// <exception cref="Exception"></exception>
        public static void IsNullOrEmptyTip(this object[] array, string message, Func<bool> action = null,
            int? errCode = null)
        {
            if (array == null || array.Length == 0)
            {
                if (action == null || action.Invoke())
                {
                    throw new BusinessException(message, errCode ?? HttpStatus.Err.Id);
                }
            }
        }

        #endregion

        #region 检查是否为假

        /// <summary>
        /// 检查是否为假 若status为false，则抛出异常
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="errorMessageFormat"></param>
        /// <param name="errCode">错误码</param>
        public static void True(bool status, string errorMessageFormat,
            int? errCode = null)
        {
            if (!status)
            {
                throw new BusinessException(string.Format(errorMessageFormat), errCode ?? HttpStatus.Err.Id);
            }
        }

        #endregion
    }
}
