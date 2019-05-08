// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enum;
using EInfrastructure.Core.HelpCommon;

namespace EInfrastructure.Core.Exception
{
    /// <summary>
    /// 权限不足异常信息
    /// </summary>
    public class AuthException : System.Exception
    {
        /// <summary>
        /// 权限校验
        /// </summary>
        /// <param name="msg">提示信息</param>
        public AuthException(string msg = ""):base(HttpStatusEnum.Unauthorized.GetDescription())
        {

        }
    }
}
