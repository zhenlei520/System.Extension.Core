// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.Tools.Common;

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
            return ObjectCommon.SafeObject(!param.IsNullOrDbNull(), () =>
            {
                var res = param.ToString();
                return isReplaceSpace ? res.Trim() : res;
            }, () => string.Empty);
        }

        #endregion
    }
}
