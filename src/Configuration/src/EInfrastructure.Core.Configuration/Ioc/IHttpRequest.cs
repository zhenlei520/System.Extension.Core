// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace EInfrastructure.Core.Configuration.Ioc
{
    /// <summary>
    ///
    /// </summary>
    public interface IHttpRequest : IPerRequest
    {
        /// <summary>
        /// 得到请求Header
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<string, string>> GetHeaders();

        /// <summary>
        /// 是否是Https
        /// </summary>
        /// <returns></returns>
        bool IsHttps();

        /// <summary>
        /// 得到get请求参数name的值
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        string GetQuery(string name);

        /// <summary>
        /// 得到get请求参数name的值，多个参数名为name时，可获取多个参数的值
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns></returns>
        List<string> GetQueryList(string name);

        /// <summary>
        /// 得到请求参数信息
        /// </summary>
        /// <returns></returns>
        Task<object> GetRequestBody();
    }
}
