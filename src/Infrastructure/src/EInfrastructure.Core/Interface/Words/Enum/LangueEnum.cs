// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Interface.Words.Enum
{
    /// <summary>
    /// 语言
    /// </summary>
    public enum LanguageEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")] Unknow = 0,

        /// <summary>
        /// 国语
        /// </summary>
        [Description("国语")] Chinese = 1,

        /// <summary>
        /// 粤语
        /// </summary>
        [Description("粤语")] Cantonese = 2,

        /// <summary>
        /// 日语
        /// </summary>
        [Description("日语")] Japanese = 3,

        /// <summary>
        /// 英语
        /// </summary>
        [Description("英语")] Englist = 4,

        /// <summary>
        /// 韩语
        /// </summary>
        [Description("韩语")] Korean = 5,

        /// <summary>
        /// 法语
        /// </summary>
        [Description("法语")] French = 6,

        /// <summary>
        /// 印度语
        /// </summary>
        [Description("印度语")] Hindi = 7
    }
}