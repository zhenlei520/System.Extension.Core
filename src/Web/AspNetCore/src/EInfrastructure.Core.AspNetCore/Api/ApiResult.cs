﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.AspNetCore.Api.Common;
using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore.Api
{
    /// <summary>
    /// 接口信息
    /// </summary>
    public class ApiResult : ApiResult<int>
    {
        /// <summary>
        ///
        /// </summary>
        public ApiResult() : base()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code"></param>
        /// <param name="data"></param>
        public ApiResult(int code = 200, object data = null) : base(code, data)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="data">数据</param>
        /// <param name="isReturnCurrentTime">是否返回方式时间</param>
        public ApiResult(int code, object data, bool isReturnCurrentTime) : base(code, data, isReturnCurrentTime)
        {
        }

        /// <summary>
        /// 状态码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public override int Code { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty(PropertyName = "response")]
        public override object Data { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        [JsonProperty(PropertyName = "current_time", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public override DateTime? CurrentTime { get; set; }
    }
}
