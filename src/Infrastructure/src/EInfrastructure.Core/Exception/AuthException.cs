// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Config.SerializeExtensions.Interfaces;
using EInfrastructure.Core.Configuration.Enumeration;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Serialize.NewtonsoftJson;

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
        public AuthException(string msg = "") : base(string.IsNullOrEmpty(msg) ? HttpStatus.Unauthorized.Name : msg)
        {
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AuthException<T> : System.Exception
    {
        /// <summary>
        /// 权限校验
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="jsonProvider"></param>
        public AuthException(T msg, IJsonService jsonProvider = null) : base(
            (jsonProvider ?? new JsonService(new List<IJsonProvider>()
            {
                new NewtonsoftJsonProvider()
            })).Serializer(new ExceptionResponse<string, T>()
            {
                Code = HttpStatus.Unauthorized.Name,
                Content = msg
            }))
        {
        }
    }
}
