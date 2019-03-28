// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Interface.ContentIdentification.Enum;

namespace EInfrastructure.Core.Interface.ContentIdentification.Config
{
    /// <summary>
    /// 鉴定配置
    /// </summary>
    public class AuthenticateConfig
    {
        /// <summary>
        /// 内容不合规
        /// </summary>
        internal static List<ContentRatingEnum> Rating { get; set; } = new List<ContentRatingEnum>()
        {
            ContentRatingEnum.Porn
        };

        #region 设置内容规范

        /// <summary>
        /// 设置内容规范
        /// </summary>
        /// <param name="ratingList">分级</param>
        public static void SetContentSpecification(List<ContentRatingEnum> ratingList)
        {
            Rating = ratingList;
        }

        #endregion
    }
}