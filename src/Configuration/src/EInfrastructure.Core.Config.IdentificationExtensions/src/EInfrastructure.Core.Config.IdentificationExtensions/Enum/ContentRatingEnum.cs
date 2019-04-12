// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;

namespace EInfrastructure.Core.Config.IdentificationExtensions.Enum
{
    /// <summary>
    /// 内容分级
    /// </summary>
    public enum ContentRatingEnum
    {
        /// <summary>
        /// 未知的
        /// </summary>
        [Description("未知的")] UnKnow = -1,

        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")] Normal = 0,

        /// <summary>
        /// 性感
        /// </summary>
        [Description("性感")] Sexy = 1,

        /// <summary>
        /// 色情
        /// </summary>
        [Description("色情")] Porn = 2,

        /// <summary>
        /// 低俗
        /// </summary>
        [Description("低俗")] Vulgar = 3,

        /// <summary>
        /// 暴力
        /// </summary>
        [Description("暴力")] Violence = 4,

        /// <summary>
        /// 血腥
        /// </summary>
        [Description("血腥")] BloodType = 5,

        /// <summary>
        /// 公众人物
        /// </summary>
        [Description("公众人物")] PublicFigures = 6,

        /// <summary>
        /// 政治
        /// </summary>
        [Description("政治")] Politically = 7,

        /// <summary>
        /// 恶心
        /// </summary>
        [Description("恶心")] Nausea = 8,

        /// <summary>
        /// 敏感词
        /// </summary>
        [Description("敏感词")] SensitiveWords = 9,

        /// <summary>
        /// 自定义敏感词
        /// </summary>
        [Description("自定义敏感词")]CustomerSensitiveWords = 10
    }
}