// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Http.Common;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace EInfrastructure.Core.Http.Provider
{
    /// <summary>
    ///
    /// </summary>
    public class PostByMultipartFormDataProvider : BaseProvider, IProvider
    {
        #region 得到请求

        /// <summary>
        /// 得到请求
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="method">方法类型</param>
        /// <param name="url">地址</param>
        /// <param name="requestBody">数据</param>
        /// <param name="headers">请求头</param>
        /// <param name="timeOut">超时限制</param>
        /// <returns></returns>
        public RestRequest GetRequest(ILogger logger, Method method, string url, RequestBody requestBody,
            Dictionary<string, string> headers,
            int timeOut)
        {
            RestRequest request = GetRestRequest(logger, url, method, timeOut, headers);
            request.AddHeader("Content-Type", "multipart/form-data");
            var bodyDic = ObjectCommon.GetParams(requestBody.Data ?? new { },
                "Newtonsoft.Json.JsonPropertyAttribute,Newtonsoft.Json");
            foreach (var item in bodyDic)
            {
                request.AddParameter(item.Key, item.Value);
            }

            foreach (var file in requestBody.Files)
            {
                request.AddFile(file.Name, file.FileByte, file.FileName, file.ContextType);
            }

            if (logger != null)
            {
                string log = "";
                var list = request.Parameters.Where(x => x.Type == ParameterType.HttpHeader)
                    .Select(x => $"key：{x.Name}，value：{x.Value}").ToList();
                list.ForEach(item => { log += $"{item}，"; });
                if (!string.IsNullOrEmpty(log))
                {
                    log = log.Substring(0, log.Length - 1);
                }

                logger.LogDebug(
                    $"url：{url}，method:{method.ToString()}，timeOut：{timeOut}，Header：{log}，data：{request.Parameters.Where(x => x.Type == ParameterType.RequestBody).Select(x => x.Value).FirstOrDefault()}");
            }

            return request;
        }

        #endregion
    }
}
