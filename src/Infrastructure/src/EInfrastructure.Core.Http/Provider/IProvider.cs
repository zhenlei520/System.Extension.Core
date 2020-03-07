// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Http.Enumerations;
using EInfrastructure.Core.Http.Params;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace EInfrastructure.Core.Http.Provider
{
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
        #region 得到参数

        /// <summary>
        /// 得到参数
        /// </summary>
        /// <param name="data">对象 允许自定义参数名，可以从JsonProperty的属性中获取</param>
        /// <returns></returns>
        protected Dictionary<string, string> GetParams(object data)
        {
            if (data == null || data is string || !data.GetType().IsClass)
            {
                return new Dictionary<string, string>();
            }

            var type = data.GetType();
            var properties = type.GetProperties();

            Dictionary<string, string> objectDic = new Dictionary<string, string>();
            foreach (var property in properties)
            {
                string name;
                if (property.CustomAttributes.Any(x => x.AttributeType == typeof(JsonProperty)))
                {
                    var namedargument = property.CustomAttributes.Where(x => x.AttributeType == typeof(JsonProperty))
                        .Select(x => x.NamedArguments).FirstOrDefault();
                    name = namedargument.Select(x => x.TypedValue.Value).FirstOrDefault()?.ToString();
                }
                else
                {
                    name = property.Name;
                }

                if (objectDic.All(x => x.Key != name) && name != null)
                {
                    objectDic.Add(name, property.GetValue(data, null)?.ToString() ?? "");
                }
            }

            return objectDic;
        }

        #endregion

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
        public RequestBody(object data, RequestBodyFormat requestBodyFormat = null,
            List<RequestMultDataParam> files = null)
        {
            Data = data;
            RequestBodyFormat = requestBodyFormat ?? RequestBodyFormat.None;
            Files = files ?? new List<RequestMultDataParam>();
        }

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
