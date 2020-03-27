// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Words.Enum
{
    /// <summary>
    /// 词库类型
    /// </summary>
    internal enum DictTypeEnum
    {
        /// <summary>
        /// 内容
        /// </summary>
        [Description("内容")] Text = 1,

        /// <summary>
        /// 拼音
        /// </summary>
        [Description("拼音")] PinYin = 2
    }
}
