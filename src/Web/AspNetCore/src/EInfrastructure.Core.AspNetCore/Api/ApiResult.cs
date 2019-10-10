// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.AspNetCore.Api.Common;

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
    }
}
