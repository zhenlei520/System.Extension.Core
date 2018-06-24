using System;
using System.Collections.Generic;
using EInfrastructure.Core.AliYun.Tbk.Config;
using RestSharp;

namespace EInfrastructure.Core.AliYun.Tbk
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenApi
    {
        protected OpenApi(string appKey, string appSecret)
        {
            AliConfig = new AliConfig()
            {
                AppKey = appKey,
                AppSecret = appSecret
            };
        }

        protected readonly AliConfig AliConfig;

        static readonly RestClient RestClient = new RestClient("http://gw.api.taobao.com/router/rest");

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
    }
}