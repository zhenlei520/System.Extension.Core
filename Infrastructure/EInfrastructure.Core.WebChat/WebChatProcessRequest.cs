using System;
using EInfrastructure.Core.Exception;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.HelpCommon.Serialization;
using EInfrastructure.Core.WebChat.Config;

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
      if (CheckSignature(token, chatAuthConfig.Signature, chatAuthConfig.Timestamp, chatAuthConfig.Nonce) && !string.IsNullOrEmpty(chatAuthConfig.EchoString))
      {
        return chatAuthConfig.EchoString;
      }
      return "";
    }

    #endregion

    #region Private Methods

    #region 验证微信签名

    /// <summary>
    /// 验证微信签名
    /// </summary>
    private static bool CheckSignature(string token, string signature, string timestamp, string nonce)
    {
      string[] arrTmp = { token, timestamp, nonce };

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
