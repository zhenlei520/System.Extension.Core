using System;
using EInfrastructure.Core.Exception;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Interface.Cache;
using EInfrastructure.Core.WebChat.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EInfrastructure.Core.WebChat.Common
{
    public class WebChatJsSdkCommon
    {
        private readonly WxConfig _config;
        private readonly ICacheService _cacheService;

        public static RestClient RestClient = new RestClient("https://api.weixin.qq.com/");

        public WebChatJsSdkCommon(WxConfig config, ICacheService cacheService)
        {
            _config = config;
            _cacheService = cacheService;
        }

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