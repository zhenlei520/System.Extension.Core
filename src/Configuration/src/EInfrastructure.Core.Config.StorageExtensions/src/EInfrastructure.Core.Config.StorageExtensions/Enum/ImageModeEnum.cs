// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Config.StorageExtensions.Enum
{
    /// <summary>
    /// 图片缩放
    /// </summary>
    public enum ImageModeEnum
    {
        /// <summary>
        /// 不处理（原图）
        /// </summary>
        [Description("不处理")] Nothing = -1,

        /// <summary>
        /// 指定高宽缩放（可能变形）   
        /// </summary>
        [Description("指定高宽缩放（可能变形）")]Hw = 0,

        /// <summary>
        /// 指定宽，高按比例   
        /// </summary>
        [Description("指定宽，高按比例")]W = 1,

        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        [Description("指定高，宽按比例")]H = 2,

        /// <summary>
        /// 指定高宽裁减（不变形） 
        /// </summary>
        [Description("指定高宽裁减（不变形） ")]Cut = 3,
    }
}