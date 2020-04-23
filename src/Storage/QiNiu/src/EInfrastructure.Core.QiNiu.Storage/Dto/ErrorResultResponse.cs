// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Newtonsoft.Json;

namespace EInfrastructure.Core.QiNiu.Storage.Dto
{
    /// <summary>
    /// 错误异常
    /// </summary>
    public class ErrorResultResponse
    {
        /// <summary>
        /// 错误
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        /// <summary>
        /// 错误状态码
        /// </summary>
        [JsonProperty(PropertyName = "error_code", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ErrorCode { get; set; }
    }
}
