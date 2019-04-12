// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.IdentificationExtensions.Config;
using EInfrastructure.Core.Config.IdentificationExtensions.Enum;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Config.IdentificationExtensions.Dto
{
    /// <summary>
    /// 内容信息
    /// </summary>
    public class ContentInfoDto
    {
        /// <summary>
        /// 是否鉴定成功
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; } = false;

        /// <summary>
        /// 鉴定信息
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        public string Msg { get; set; } = "success";

        /// <summary>
        /// 是否合规
        /// </summary>
        [JsonProperty(PropertyName = "is_normal", NullValueHandling = NullValueHandling.Ignore)]
        public ViolationStatusEnum? IsNormal
        {
            get
            {
                if (!Success)
                {
                    return null;
                }


                if (Data.Any(x => x.Rating == ContentRatingEnum.UnKnow))
                {
                    return ViolationStatusEnum.UnKnow;
                }

                if (Data.Any(x => x.Rating == ContentRatingEnum.Normal) ||
                    !Data.Any(x => AuthenticateConfig.Rating.Any(y => x.Rating == y)))
                {
                    return ViolationStatusEnum.Normal;
                }

                return ViolationStatusEnum.Violation;
            }
        }

        /// <summary>
        /// 详细信息
        /// </summary>
        [JsonProperty(PropertyName = "data", NullValueHandling = NullValueHandling.Ignore)]
        public List<DataDto> Data { get; set; }

        /// <summary>
        /// 详细信息
        /// </summary>
        public class DataDto
        {
            /// <summary>
            /// 内容分级
            /// </summary>
            [JsonProperty(PropertyName = "rating")]
            public ContentRatingEnum Rating { get; set; }

            /// <summary>
            /// 详细分级
            /// </summary>
            [JsonProperty(PropertyName = "sub_rating")]
            public SubContentRatingEnum SubRating { get; set; }

            /// <summary>
            /// 信息
            /// </summary>
            [JsonProperty(PropertyName = "msg")]
            public string Msg { get; set; }

            /// <summary>
            /// 相似度
            /// </summary>
            [JsonProperty(PropertyName = "probability", NullValueHandling = NullValueHandling.Ignore)]
            public decimal? Probability { get; set; }

            /// <summary>
            /// 人物信息
            /// </summary>
            [JsonProperty(PropertyName = "star", NullValueHandling = NullValueHandling.Ignore)]
            public List<PersonDto> Star { get; set; } = null;
        }

        /// <summary>
        /// 人物信息
        /// </summary>
        public class PersonDto
        {
            /// <summary>
            /// 相似度
            /// </summary>
            [JsonProperty(PropertyName = "probability", NullValueHandling = NullValueHandling.Ignore)]
            public decimal? Probability { get; set; }

            /// <summary>
            /// 姓名
            /// </summary>
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }
        }
    }
}