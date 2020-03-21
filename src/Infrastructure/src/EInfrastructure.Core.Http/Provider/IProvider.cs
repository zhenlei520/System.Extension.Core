// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Http.Enumerations;
using EInfrastructure.Core.Http.Params;
using RestSharp;

namespace EInfrastructure.Core.Http.Provider
{
    /// <summary>
    ///
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// 得到请求
        /// </summary>
        /// <param name="method">方法类型</param>
        /// <param name="url">地址</param>
        /// <param name="requestBody">数据</param>
        /// <param name="headers">请求头</param>
        /// <param name="timeOut">超时限制</param>
        /// <returns></returns>
        RestRequest GetRequest(Method method, string url, RequestBody requestBody, Dictionary<string, string> headers,
            int timeOut);
    }

    /// <summary>
    ///
    /// </summary>
    public class BaseProvider
    {
        #region 得到基本的请求

        /// <summary>
        /// 得到基本的请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法类型</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="headers">请求头（可为空）</param>
        /// <returns></returns>
        protected RestRequest GetRestRequest(string url, Method method, int timeOut, Dictionary<string, string> headers)
        {
            RestRequest request = new RestRequest(url, method) {Timeout = timeOut};
            if (headers != null)
            {
                foreach (var key in headers.Keys)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        request.AddHeader(key, headers[key]);
                    }
                }
            }
            else
            {
                headers = new Dictionary<string, string>();
            }

            return request;
        }

        #endregion
    }

    /// <summary>
    /// 请求信息
    /// </summary>
    public class RequestBody
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="requestBodyFormat">请求Body格式</param>
        /// <param name="files">文件信息</param>
        /// <param name="jsonProvider"></param>
        /// <param name="xmlProvider"></param>
        public RequestBody(object data, RequestBodyFormat requestBodyFormat = null,
            List<RequestMultDataParam> files = null, IJsonProvider jsonProvider = null, IXmlProvider xmlProvider = null)
        {
            Data = data;
            RequestBodyFormat = requestBodyFormat ?? RequestBodyFormat.None;
            Files = files ?? new List<RequestMultDataParam>();
            _jsonProvider = jsonProvider;
            _xmlProvider = xmlProvider;
        }

        /// <summary>
        ///
        /// </summary>
        public readonly IJsonProvider _jsonProvider;

        /// <summary>
        ///
        /// </summary>
        public readonly IXmlProvider _xmlProvider;

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; }

        /// <summary>
        /// 请求Body格式
        /// </summary>
        public RequestBodyFormat RequestBodyFormat { get; }

        /// <summary>
        /// 文件信息
        /// </summary>
        public List<RequestMultDataParam> Files { get; }
    }
}
