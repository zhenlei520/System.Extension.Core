// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore.Api
{
    /// <summary>
    /// 正常响应信息
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 响应码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public virtual int Code { get; set; } = 200;

        /// <summary>
        /// 响应信息
        /// </summary>
        [JsonProperty(PropertyName = "response")]
        public virtual object Data { get; set; } = new { };
    }
}