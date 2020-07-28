// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Newtonsoft.Json;

namespace EInfrastructure.Core.CustomConfiguration.Core.Dto
{
    /// <summary>
    /// 应用名称空间列表
    /// </summary>
    public class AppNamespacesItemDto
    {
        /// <summary>
        /// 名称空间id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        [JsonProperty(PropertyName = "appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 名称空间
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 格式
        /// 目前仅支持Json
        /// </summary>
        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty(PropertyName = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [JsonProperty(PropertyName = "edit_time")]
        public DateTime EditTime { get; set; }
    }

    /// <summary>
    /// 应用名称空间详情
    /// </summary>
    public class AppNamespacesDetailDto
    {
        /// <summary>
        /// 名称空间id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        [JsonProperty(PropertyName = "appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 名称空间
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 格式
        /// 目前仅支持Json
        /// </summary>
        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty(PropertyName = "remark")]
        public string Remark { get; set; }

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