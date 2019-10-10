// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

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
        public ApiErrResult(int code, string msg, object extend = null)
        {
        }
    }
}
