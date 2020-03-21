// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Tools.Systems
{
    /// <summary>
    ///
    /// </summary>
    public static class ResultCommon
    {
        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回空
        /// </summary>
        /// <param name="param">参数</param>
        public static string SafeString(this object param)
        {
            return ObjectCommon.SafeObject(param != null,
                () => ValueTuple.Create(param.ToString().Trim(), string.Empty));
        }
    }
}
