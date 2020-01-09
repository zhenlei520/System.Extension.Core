// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 计算帮助类
    /// </summary>
    public static class MathCommon
    {
        #region 大小写转换

        #region 转为大写
        /// <summary>
        /// 转为大写
        /// </summary>
        /// <param name="parameter">需要转换的参数</param>
        /// <returns></returns>
        public static string ToUppers(this string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                return "";
            }
            return parameter.ToUpper();
        }
        #endregion

        #region 转为小写
        /// <summary>
        /// 转为小写
        /// </summary>
        /// <param name="parameter">需要转换的参数</param>
        /// <returns></returns>
        public static string ToLowers(this string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                return "";
            }
            return parameter.ToLower();
        }
        #endregion

        #endregion
    }
}
