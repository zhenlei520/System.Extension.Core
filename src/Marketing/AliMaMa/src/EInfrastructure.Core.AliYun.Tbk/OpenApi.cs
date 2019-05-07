// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.AliYun.Tbk.Config;
using EInfrastructure.Core.AliYun.Tbk.Respose;
using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Config.SerializeExtensions.Interfaces;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using RestSharp;

namespace EInfrastructure.Core.AliYun.Tbk
{
    /// <summary>
    ///
    /// </summary>
    public class OpenApi
    {
        protected readonly IJsonService _jsonProvider;

        /// <summary>
        ///
        /// </summary>
        /// <param name="appKey">appKey</param>
        /// <param name="appSecret">app秘钥</param>
        /// <param name="jsonProvider"></param>
        protected OpenApi(string appKey, string appSecret, IJsonService jsonProvider = null)
        {
            _jsonProvider = jsonProvider ?? new JsonService(new List<IJsonProvider>()
            {
                new NewtonsoftJsonProvider()
            });
            AliConfig = new AliTbkConfig
            {
                AppKey = appKey,
                AppSecret = appSecret
            };
        }

        /// <summary>
        /// 淘宝客配置
        /// </summary>
        protected readonly AliTbkConfig AliConfig;

        static readonly RestClient RestClient = new RestClient("https://eco.taobao.com/router/rest");
//        static readonly RestClient RestClient = new RestClient("http://gw.api.taobao.com/router/rest");

        /// <summary>
        ///
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="method"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected string GetResponse(string apiUrl, Method method,
            Func<Dictionary<string, string>, Dictionary<string, string>> func)
        {
            Dictionary<string, string> param =
                OpenApiUtil.BuildCommonParam(apiUrl, AliConfig.AppKey);
            param = func?.Invoke(param);
            string sign = OpenApiUtil.CreateSign(param, AliConfig.AppSecret);
            if (param != null)
            {
                param.Add("sign", sign);
                RestRequest request = new RestRequest(method);
                foreach (var key in param.Keys)
                {
                    request.AddParameter(key, param[key]);
                }

                var resultContent = RestClient.Execute(request).Content;
                return resultContent;
            }

            return "";
        }

        #region 得到结果

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <param name="response">响应信息</param>
        /// <param name="successAction">成功响应</param>
        /// <param name="errAction">失败响应</param>
        /// <typeparam name="T"></typeparam>
        protected void GetResult<T>(string response, Action<T> successAction, Action<ErrDto> errAction)
            where T : class, new()
        {
            if (response.Contains("error_response"))
            {
                errAction(_jsonProvider.Deserialize<ErrDto>(response));
            }

            successAction(_jsonProvider.Deserialize<T>(response));
        }

        #endregion
    }
}
