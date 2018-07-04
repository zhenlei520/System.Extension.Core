using System;
using EInfrastructure.Core.Exception;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.HelpCommon.Serialization;
using EInfrastructure.Core.WebChat.Common;
using EInfrastructure.Core.WebChat.Config;
using EInfrastructure.Core.WebChat.Enum;
using Newtonsoft.Json;
using RestSharp;

namespace EInfrastructure.Core.WebChat
{
    /// <summary>
    /// 微信公众号信息处理
    /// </summary>
    public class WebChatProcessRequest
    {
        #region 处理用户消息

        /// <summary>
        /// 处理用户消息
        /// </summary>
        /// <param name="webChatAuthConfig"></param>
        /// <param name="wxConfig"></param>
        /// <param name="xml"></param>
        public WebChatMessage ProcessRequest(WebChatAuthConfig webChatAuthConfig, WxConfig wxConfig, string xml)
        {
            WebChatMessage refundReponse = null;
            try
            {
                if (!string.IsNullOrEmpty(Auth(webChatAuthConfig, wxConfig)))
                {
                    throw new BusinessException("签名错误");
                }

                refundReponse = XmlHelper.Deserialize<WebChatMessage>(xml);
                if (refundReponse == null)
                {
                    throw new BusinessException("参数错误");
                }
            }
            catch (System.Exception e)
            {
                LogCommon.Error("接受用户信息错误：", e);
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
                LogCommon.Error("WeixinToken 配置项没有配置！");
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
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public LoginResultConfig Login(WxConfig config, string code)
        {
            LoginResultConfig loginResult = new LoginResultConfig();

            if (config.Type == WebChatTypeEnum.ThirdPartyLogins)
            {
                if (string.IsNullOrEmpty(code))
                {
                    throw new BusinessException("登录失败，授权异常");
                }

                WxUserInfo wxUserInfo = JsonConvert.DeserializeObject<WxUserInfo>(code);
                if (wxUserInfo == null)
                {
                    throw new BusinessException("登录失败，授权异常");
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
                if (config.Type == WebChatTypeEnum.Mweb)
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
                    LogCommon.Warn("[微信登陆失败]\r\n请求参数：" + getAccessTokenUrl + "\r\n返回参数：" +
                                   new JsonCommon().Serializer(getAccessTokenResult, true));
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