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
    /// <inheritdoc />
    /// <summary>
    /// 业务异常,可以将Exception消息直接返回给用户
    /// </summary>
    public class BusinessException : BusinessException<int>
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="content">异常详情</param>
        /// <param name="jsonProvider"></param>
        public BusinessException(string content, int? code = null,
            IJsonService jsonProvider = null) :
            base(content, code ?? HttpStatus.Err.Id)
        {
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessException<T> : BusinessException<T, string>
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="content">异常详情</param>
        /// <param name="jsonProvider"></param>
        public BusinessException(string content, T code = default(T),
            IJsonService jsonProvider = null) :
            base(content, code)
        {
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class BusinessException<T, T2> : System.Exception
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="content">异常详情</param>
        /// <param name="jsonProvider"></param>
        public BusinessException(T2 content, T code = default(T),
            IJsonService jsonProvider = null) :
            base((jsonProvider ?? new JsonService(new List<IJsonProvider>()
            {
                new NewtonsoftJsonProvider()
            })).Serializer(new ExceptionResponse<T, T2>() {Code = code, Content = content}))
        {
        }
    }
}
