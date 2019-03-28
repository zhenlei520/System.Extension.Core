// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Exception;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Interface.Cache;
using EInfrastructure.Core.WeChat.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EInfrastructure.Core.WeChat.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class WebChatJsSdkCommon
    {
        private readonly WxConfig _config;
        private readonly ICacheService _cacheService;

        /// <summary>
        /// 
        /// </summary>
        public static RestClient RestClient = new RestClient("https://api.weixin.qq.com/");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="cacheService"></param>
        public WebChatJsSdkCommon(WxConfig config, ICacheService cacheService)
        {
            _config = config;
            _cacheService = cacheService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public string GetAccessToken(string cacheKey)
        {
            cacheKey = cacheKey + _config.Type;

            string token = _cacheService.StringGet<string>(cacheKey);

            if (string.IsNullOrEmpty(token))
            {
                string resources = "cgi-bin/token?grant_type=client_credential&appid=" + _config.AppId + "&secret=" +
                                   _config.AppSecret;

                string result = RestClient.Execute(new RestRequest(resources, Method.GET)).Content;

                if (result.Contains("errcode"))
                {
                    throw new BusinessException("获取token失败");
                }

                JObject obj = JsonConvert.DeserializeObject<dynamic>(result);

                token = obj["access_token"].ToString();

                _cacheService.StringSet(cacheKey, token, TimeSpan.FromSeconds(7000));
            }

            return token;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tickCacheKey"></param>
        /// <param name="tokenCacheKey"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public string GetJsApiTicket(string tickCacheKey, string tokenCacheKey)
        {
            string ticket = _cacheService.StringGet<string>(tickCacheKey);

            if (string.IsNullOrEmpty(ticket))
            {
                string token = GetAccessToken(tokenCacheKey);

                string resoures = "cgi-bin/ticket/getticket?access_token=" + token + "&type=jsapi";

                string result = RestClient.Execute(new RestRequest(resoures, Method.GET)).Content;

                if (!result.Contains("ok"))
                {
                    throw new BusinessException("获取ticket失败");
                }

                dynamic obj = JsonConvert.DeserializeObject<dynamic>(result);

                ticket = obj["ticket"].ToString();

                _cacheService.StringSet(tickCacheKey, ticket, TimeSpan.FromSeconds(7000));
            }

            return ticket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tickCacheKey"></param>
        /// <param name="tokenCacheKey"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public JsSdkConfig GetConfig(string tickCacheKey, string tokenCacheKey, string url)
        {
            string ticket = GetJsApiTicket(tickCacheKey, tokenCacheKey);

            string nonceStr = Guid.NewGuid().ToString().Replace("-", "");

            long timestamp = TimeCommon.CurrentTimeMillis();

            JsSdkConfig config = new JsSdkConfig()
            {
                AppId = _config.AppId,
                TimeStamp = timestamp,
                NonceStr = nonceStr
            };

            string valueTeam = "jsapi_ticket=" + ticket + "&noncestr=" + nonceStr + "&timestamp=" + timestamp +
                               "&url=" + url;

            config.Signature = SecurityCommon.Sha1(valueTeam).ToLower();

            return config;
        }
    }
}