// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 内容分级
    /// </summary>
    public class ContentRating : Enumeration
    {
        /// <summary>
        /// 正常
        /// </summary>
        public static ContentRating Normal = new ContentRating(1, "正常");

        /// <summary>
        /// 性感
        /// </summary>
        public static ContentRating Sexy = new ContentRating(2, "性感");

        /// <summary>
        /// 色情
        /// </summary>
        public static ContentRating Porn = new ContentRating(3, "色情");

        /// <summary>
        /// 低俗
        /// </summary>
        public static ContentRating Vulgar = new ContentRating(4, "低俗");

        /// <summary>
        /// 暴力
        /// </summary>
        public static ContentRating Violence = new ContentRating(5, "暴力");

        /// <summary>
        /// 血腥
        /// </summary>
        public static ContentRating BloodType = new ContentRating(6, "血腥");

        /// <summary>
        /// 恶心
        /// </summary>
        public static ContentRating Nausea = new ContentRating(7, "恶心");

        /// <summary>
        /// 未知的
        /// </summary>
        public static ContentRating UnKnow = new ContentRating(8, "未知的");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public ContentRating(int id, string name) : base(id, name)
        {
        }
    }
}
