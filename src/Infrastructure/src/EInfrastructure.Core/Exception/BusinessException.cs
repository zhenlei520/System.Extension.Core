// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Config.SerializeExtensions.Interfaces;
using EInfrastructure.Core.Configuration.Enumeration;
using EInfrastructure.Core.Serialize.NewtonsoftJson;

namespace EInfrastructure.Core.Exception
{
    /// <inheritdoc />
    /// <summary>
    /// 业务异常,可以将Exception消息直接返回给用户
    /// </summary>
    public class BusinessException : System.Exception
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="content">异常详情</param>
        /// <param name="jsonProvider"></param>
        public BusinessException(string content, int code = 201,
            IJsonService jsonProvider = null) :
            base(
                (jsonProvider ?? new JsonService(new List<IJsonProvider>()
                {
                    new NewtonsoftJsonProvider()
                })).Serializer(new {code = code, content}))
        {
        }

        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="content">异常详情</param>
        /// <param name="jsonProvider"></param>
        public BusinessException(string content, string code = null,
            IJsonService jsonProvider = null) :
            base((jsonProvider ?? new JsonService(new List<IJsonProvider>()
                {
                    new NewtonsoftJsonProvider()
                })).Serializer(new {code = code ?? HttpStatus.Err.Name, content}))
        {
        }
    }
}
