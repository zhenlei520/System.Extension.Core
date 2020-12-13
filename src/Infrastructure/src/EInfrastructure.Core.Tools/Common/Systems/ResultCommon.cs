// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace EInfrastructure.Core.Tools.Common.Systems
{
    /// <summary>
    ///
    /// </summary>
    public static class ResultCommon
    {
        #region 安全转换为字符串，去除两端空格，当值为null时返回空

        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回空
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="isReplaceSpace">是否移除空格（默认移除）</param>
        public static string SafeString(this object param, bool isReplaceSpace = true)
        {
            return ObjectCommon.SafeObject(param != null,
                () =>
                {
                    if (isReplaceSpace)
                    {
                        return ValueTuple.Create(param?.ToString().Trim(), string.Empty);
                    }

                    return ValueTuple.Create(param?.ToString(), string.Empty);
                });
        }

        #endregion

        #region 返回安全的集合

        /// <summary>
        /// 返回安全的集合
        /// </summary>
        /// <param name="param"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> SafeList<T>(this ICollection<T> param)
        {
            return ObjectCommon.SafeObject(param != null,
                () => ValueTuple.Create(param?.ToList(), new List<T>()));
        }

        #endregion

        #region 返回安全的集合数组

        /// <summary>
        /// 返回安全的集合数组
        /// </summary>
        /// <param name="param"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SafeArray<T>(this ICollection<T> param)
        {
            return SafeList(param).ToArray();
        }

        #endregion

        #region 安全获取值，当值为null时，不会抛出异常

        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }

        #endregion
    }
}
