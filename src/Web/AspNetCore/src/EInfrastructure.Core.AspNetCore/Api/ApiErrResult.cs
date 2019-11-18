// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.AspNetCore.Api.Common;

namespace EInfrastructure.Core.AspNetCore.Api
{
    /// <summary>
    /// 异常响应信息
    /// </summary>
    public class ApiErrResult : ApiErrResult<int>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="extend"></param>
        public ApiErrResult(int code, string msg, object extend = null) : base(code, msg, extend)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="msg">错误信息</param>
        /// <param name="extend">扩展信息</param>
        /// <param name="isReturnCurrentTime">是否返回方式时间</param>
        public ApiErrResult(int code, string msg, object extend, bool isReturnCurrentTime) : base(code, msg, extend,
            isReturnCurrentTime)
        {
        }

        public override string Msg { get; set; }
        public override int Code { get; set; }
        public override object Extend { get; set; }
        public override DateTime? CurrentTime { get; set; }
    }
}
