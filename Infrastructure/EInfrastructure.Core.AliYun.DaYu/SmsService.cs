using System;
using System.Collections.Generic;
using EInfrastructure.Core.AliYun.DaYu.Common;
using EInfrastructure.Core.AliYun.DaYu.Config;
using EInfrastructure.Core.AliYun.DaYu.Model;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.HelpCommon.Serialization;
using EInfrastructure.Core.Interface.IOC;
using EInfrastructure.Core.Interface.Sms;
using EInfrastructure.Core.Interface.Sms.Dto;
using Microsoft.Extensions.Options;
using RestSharp;

namespace EInfrastructure.Core.AliYun.DaYu
{
    /// <summary>
    /// 短信
    /// </summary>
    public class SmsService : ISmsService, ISingleInstance
    {
        private readonly SmsConfig _smsConfig;

        public SmsService(SmsConfig smsConfig)
        {
            _smsConfig = smsConfig;
        }

        readonly RestClient _restClient = new RestClient("http://dysmsapi.aliyuncs.com");

        #region 指定短信列表发送短信
        /// <summary>
        /// 指定短信列表发送短信
        /// </summary>
        /// <param name="phoneNumbers">手机号</param>
        /// <param name="templateCode">短信模板</param>
        /// <param name="content">内容</param>
        /// <param name="loseAction">失败回调函数</param>
        /// <returns></returns>
        public bool Send(List<string> phoneNumbers, string templateCode, object content, Action<SendSmsLoseDto> loseAction = null)
        {
            Dictionary<string, string> commonParam = Util.BuildCommonParam();
            commonParam.Add("Action", "SendSms");
            commonParam.Add("Version", "2017-05-25");
            commonParam.Add("RegionId", "cn-hangzhou");
            commonParam.Add("PhoneNumbers", phoneNumbers.ConvertListToString(','));
            commonParam.Add("SignName", "得有生活");
            commonParam.Add("TemplateCode", templateCode);
            commonParam.Add("TemplateParam", new JsonCommon().Serializer(content));

            string sign = Util.CreateSign(commonParam, _smsConfig.EncryptionKey);
            commonParam.Add("Signature", sign);
            RestRequest request = new RestRequest(Method.GET);
            foreach (var key in commonParam.Keys)
            {
                request.AddQueryParameter(key, commonParam[key]);
            }
            var response = _restClient.Execute(request);
            SendSmsResponse result = XmlHelper.Deserialize<SendSmsResponse>(response.Content);
            if (result.Code == "OK")
            {
                return true;
            }
            loseAction?.Invoke(new SendSmsLoseDto()
            {
                PhoneList = phoneNumbers,
                Msg = "短信发送失败",
                SubMsg = response.Content,
                Code=result.Code
            });
            return false;
        }
        #endregion

        #region 指定单个手机号发送短信
        /// <summary>
        /// 指定单个手机号发送短信
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="templateCode">短信模板</param>
        /// <param name="content">内容</param>
        /// <param name="loseAction">失败回调函数</param>
        /// <returns></returns>
        public bool Send(string phoneNumber, string templateCode, object content, Action<SendSmsLoseDto> loseAction = null)
        {
            return Send(new List<string>() { phoneNumber }, templateCode, content, loseAction);
        }
        #endregion
    }
}
