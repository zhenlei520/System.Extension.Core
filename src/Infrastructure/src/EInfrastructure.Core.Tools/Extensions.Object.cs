// Copyright (c) zhenlei520 All rights reserved.

using System;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public partial class Extensions
    {
        #region 安全转换为字符串，去除两端空格，当值为null时返回空

        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回空
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="isReplaceSpace">是否移除空格（默认移除）</param>
        public static string SafeString(this object param, bool isReplaceSpace = true)
        {
            return Common.ObjectCommon.SafeObject(!param.IsNullOrDbNull(),
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

        #region 是否Null

        /// <summary>
        /// 是否Null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 是否null或者DBNull
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrDbNull(this object obj)
        {
            return obj == null || obj is DBNull;
        }

        #endregion
    }
}
