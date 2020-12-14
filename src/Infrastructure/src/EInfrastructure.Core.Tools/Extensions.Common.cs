// Copyright (c) zhenlei520 All rights reserved.

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public partial class Extensions
    {
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
