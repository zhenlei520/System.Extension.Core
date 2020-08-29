// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Logger;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Serialize.Xml;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.WeChat.Common;
using EInfrastructure.Core.WeChat.Config;
using EInfrastructure.Core.WeChat.Enumerations;
using Newtonsoft.Json;
using RestSharp;

namespace EInfrastructure.Core.WeChat
{
    /// <summary>
    /// 微信公众号信息处理
    /// </summary>
    public class WebChatProcessRequest
    {
        private readonly ILoggerProvider<WebChatProcessRequest> _logger;
        private readonly IJsonProvider _jsonProvider;
        private readonly IXmlProvider _xmlProvider;

        /// <summary>
        ///
        /// </summary>
        public WebChatProcessRequest() : this(null, new List<IJsonProvider>()
        {
            new NewtonsoftJsonProvider()
        }, new List<IXmlProvider>()
        {
            new XmlProvider()
        })
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jsonProviders"></param>
        public WebChatProcessRequest(ICollection<IJsonProvider> jsonProviders) : this(null, jsonProviders,
            new List<IXmlProvider>()
            {
                new XmlProvider()
            })
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="xmlProviders"></param>
        public WebChatProcessRequest(
            ICollection<IXmlProvider> xmlProviders) : this(null, new List<IJsonProvider>()
        {
            new NewtonsoftJsonProvider()
        }, xmlProviders)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jsonProviders"></param>
        /// <param name="xmlProviders"></param>
        public WebChatProcessRequest(ICollection<IJsonProvider> jsonProviders,
            ICollection<IXmlProvider> xmlProviders) : this(null, jsonProviders, xmlProviders)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="jsonProviders"></param>
        /// <param name="xmlProviders"></param>
        public WebChatProcessRequest(ILoggerProvider<WebChatProcessRequest> logger, ICollection<IJsonProvider> jsonProviders,
            ICollection<IXmlProvider> xmlProviders)
        {
            _logger = logger;
            _jsonProvider = InjectionSelectionCommon.GetImplement(jsonProviders);
            _xmlProvider = InjectionSelectionCommon.GetImplement(xmlProviders);
        }

        #region 处理用户消息

        /// <summary>
        /// 处理用户消息
        /// </summary>
        /// <param name="webChatAuthConfig"></param>
        /// <param name="wxConfig"></param>
        /// <param name="xml"></param>
        /// <param name="errCode">错误码</param>
        public WebChatMessage ProcessRequest(WebChatAuthConfig webChatAuthConfig, WxConfig wxConfig, string xml,
            int? errCode = null)
        {
            WebChatMessage refundReponse = null;
            try
            {
                if (!string.IsNullOrEmpty(Auth(webChatAuthConfig, wxConfig)))
                {
                    throw new BusinessException("签名错误", HttpStatus.Err.Id);
                }

                refundReponse = _xmlProvider.Deserialize<WebChatMessage>(xml);
                if (refundReponse == null)
                {
                    throw new BusinessException("参数错误", HttpStatus.Err.Id);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("接受用户信息错误：", ex.ExtractAllStackTrace());
            }

            return refundReponse;
        }

        #endregion

        #region 授权认证

        /// <summary>
        /// 授权认证
        /// </summary>
        /// <returns></returns>
        public string Auth(WebChatAuthConfig chatAuthConfig, WxConfig wxConfig)
        {
            string encodingAesKey = wxConfig.EncodingAesKey; //根据自己后台的设置保持一致
            string appId = wxConfig.AppId; //根据自己后台的设置保持一致
            string token = wxConfig.Token; //从配置文件获取Token
            if (string.IsNullOrEmpty(token))
            {
                _logger?.LogError("WeixinToken 配置项没有配置！");
            }

            if (CheckSignature(token, chatAuthConfig.Signature, chatAuthConfig.Timestamp, chatAuthConfig.Nonce) &&
                !string.IsNullOrEmpty(chatAuthConfig.EchoString))
            {
                return chatAuthConfig.EchoString;
            }

            return "";
        }

        #endregion

        #region 获取用户信息

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="config"></param>
        /// <param name="code"></param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public LoginResultConfig Login(WxConfig config, string code, int? errCode = null)
        {
            LoginResultConfig loginResult = new LoginResultConfig();

            if (config.Type == WebChatType.ThirdPartyLogins.Id)
            {
                if (string.IsNullOrEmpty(code))
                {
                    throw new BusinessException("登录失败，授权异常", errCode ?? HttpStatus.Err.Id);
                }

                WxUserInfo wxUserInfo = JsonConvert.DeserializeObject<WxUserInfo>(code);
                if (wxUserInfo == null)
                {
                    throw new BusinessException("登录失败，授权异常", errCode ?? HttpStatus.Err.Id);
                }

                loginResult.Success = !string.IsNullOrEmpty(wxUserInfo.Openid) &&
                                      !string.IsNullOrEmpty(wxUserInfo.Unionid);

                loginResult.Error = "";
                loginResult.OpenId = wxUserInfo.Openid;
                loginResult.Unionid = wxUserInfo.Unionid;
                loginResult.AppId = config.AppId;
                loginResult.UserInfo = wxUserInfo;
            }
            else
            {
                //                string getAccessTokenUrl = "sns/oauth2/access_token?appid=" + appConfig.AppId + "&secret=" +
                //                                           appConfig.AppSecret + "&code=" +
                //                                           code + "&grant_type=authorization_code";

                string getAccessTokenUrl;
                if (config.Type == WebChatType.MWeb.Id)
                {
                    getAccessTokenUrl = "sns/oauth2/access_token?appid=" + config.AppId + "&secret=" +
                                        config.AppSecret + "&code=" +
                                        code + "&grant_type=authorization_code";
                }
                else
                {
                    getAccessTokenUrl =
                        $"sns/jscode2session?appid={config.AppId}&secret={config.AppSecret}&js_code={code}&grant_type=authorization_code";
                }

                string getAccessTokenResponse = WebChatJsSdkCommon.RestClient
                    .Execute(new RestRequest(getAccessTokenUrl
                    ))
                    .Content;


                GetAccessTokenResultConfig getAccessTokenResult =
                    JsonConvert.DeserializeObject<GetAccessTokenResultConfig>(getAccessTokenResponse);


                loginResult.Success = !string.IsNullOrEmpty(getAccessTokenResult.Openid) &&
                                      !string.IsNullOrEmpty(getAccessTokenResult.Unionid);

                loginResult.Error = getAccessTokenResult.Errmsg;
                loginResult.OpenId = getAccessTokenResult.Openid;
                loginResult.Unionid = getAccessTokenResult.Unionid;
                loginResult.AppId = config.AppId;


                if (loginResult.Success)
                {
                    string getUserInfoResponse = WebChatJsSdkCommon.RestClient
                        .Execute(new RestRequest("sns/userinfo?access_token=" + getAccessTokenResult.AccessToken +
                                                 "&openid=" + loginResult.OpenId + "")).Content;


                    WxUserInfo wxUserInfo = JsonConvert.DeserializeObject<WxUserInfo>(getUserInfoResponse);

                    if (string.IsNullOrEmpty(wxUserInfo.Errmsg) && string.IsNullOrEmpty(wxUserInfo.Errcode))
                    {
                        loginResult.UserInfo = wxUserInfo;
                    }
                    else
                    {
                        loginResult.UserInfo = null;
                    }
                }

                if (!loginResult.Success)
                {
                    _logger?.LogError("[微信登陆失败]\r\n请求参数：" + getAccessTokenUrl + "\r\n返回参数：" +
                                      _jsonProvider.Serializer(getAccessTokenResult, true));
                }
            }

            return loginResult;
        }

        #endregion

        #region Private Methods

        #region 验证微信签名

        /// <summary>
        /// 验证微信签名
        /// </summary>
        private static bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] arrTmp = {token, timestamp, nonce};

            Array.Sort(arrTmp);
            string tmpStr = string.Join("", arrTmp);

            tmpStr = SecurityCommon.Sha1(tmpStr);
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }

            return false;
        }

        #endregion

        #endregion
    }
}
