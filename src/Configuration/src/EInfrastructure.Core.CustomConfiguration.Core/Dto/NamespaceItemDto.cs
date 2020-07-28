// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Newtonsoft.Json;

namespace EInfrastructure.Core.CustomConfiguration.Core.Dto
{
    /// <summary>
    /// 名称空间值
    /// </summary>
    public class NamespaceItemDto
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// 键 
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        /// <summary>
        /// 值 
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [JsonProperty(PropertyName = "edit_time")]
        public DateTime EditTime { get; set; }
    }

    /// <summary>
    /// 名称空间值
    /// </summary>
    public class NamespaceDetailDto
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// 键 
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        /// <summary>
        /// 值 
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [JsonProperty(PropertyName = "add_time")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [JsonProperty(PropertyName = "edit_time")]
        public DateTime EditTime { get; set; }
    }
}