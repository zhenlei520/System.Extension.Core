// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Config.SerializeExtensions.Interfaces;
using EInfrastructure.Core.Configuration.Enum;

namespace EInfrastructure.Core.Exception
{
    /// <inheritdoc />
    /// <summary>
    /// 业务异常,可以将Exception消息直接返回给用户
    /// </summary>
    public class BusinessException : System.Exception
    {
        private static IJsonProvider _jsonProvider;

        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="content">异常详情</param>
        public BusinessException(string content, int code = (int) HttpStatusEnum.Err)
        {
            if (_jsonProvider == null)
            {
                _jsonProvider=new Serialize.NewtonsoftJson.NewtonsoftJsonProvider();
            }
            throw new System.Exception(_jsonProvider.Serializer(new {code, content}));
        }
    }
}
