// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Newtonsoft.Json;

namespace EInfrastructure.Core.AliYun.Tbk.Respose
{
    /// <summary>
    /// 错误信息
    /// </summary>
    public class ErrDto
    {
        /// <summary>
        /// 错误响应信息
        /// </summary>
        [JsonProperty(PropertyName = "error_response")]
        public ErrorResponseDto ErrorResponse { get; set; }

        /// <summary>
        /// 错误响应信息
        /// </summary>
        public class ErrorResponseDto
        {
            /// <summary>
            /// 详细错误信息
            /// </summary>
            [JsonProperty(PropertyName = "sub_msg")]
            public string SubMsg { get; set; }

            /// <summary>
            /// 状态码
            /// </summary>
            [JsonProperty(PropertyName = "code")]
            public int Code { get; set; }

            /// <summary>
            /// 详细状态码
            /// </summary>
            [JsonProperty(PropertyName = "sub_code")]
            public string SubCode { get; set; }

            /// <summary>
            /// 错误信息
            /// </summary>
            [JsonProperty(PropertyName = "msg")]
            public string Msg { get; set; }
        }
    }
}