// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.AliYun.DaYu.Common;
using EInfrastructure.Core.AliYun.DaYu.Config;
using EInfrastructure.Core.AliYun.DaYu.Model;
using EInfrastructure.Core.AliYun.DaYu.Validator;
using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Config.SmsExtensions;
using EInfrastructure.Core.Config.SmsExtensions.Dto;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.HelpCommon.Systems;
using EInfrastructure.Core.Validation.Common;
using RestSharp;

namespace EInfrastructure.Core.AliYun.DaYu
{
    /// <summary>
    /// 短信
    /// </summary>
    public class SmsService : ISmsService, ISingleInstance
    {
        private AliSmsConfig _smsConfig;
        private readonly JsonProvider _jsonProvider;

        /// <summary>
        /// 短信服务
        /// </summary>
        public SmsService(AliSmsConfig smsConfig, JsonProvider jsonProvider)
        {
            _smsConfig = smsConfig;
            _jsonProvider = jsonProvider;
        }

        readonly RestClient _restClient = new RestClient("http://dysmsapi.aliyuncs.com");

        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            return AssemblyCommon.GetReflectedInfo().Namespace;
        }

        #endregion

        #region 指定短信列表发送短信

        /// <summary>
        /// 指定短信列表发送短信
        /// </summary>
        /// <param name="phoneNumbers">手机号</param>
        /// <param name="templateCode">短信模板</param>
        /// <param name="content">内容</param>
        /// <param name="loseAction">失败回调函数</param>
        /// <param name="smsConfigJson">短信配置Json串</param>
        /// <returns></returns>
        public bool Send(List<string> phoneNumbers, string templateCode, object content,
            Action<SendSmsLoseDto> loseAction = null, string smsConfigJson = "")
        {
            Dictionary<string, string> commonParam = Util.BuildCommonParam(GetSmsConfig(smsConfigJson).AccessKey);
            commonParam.Add("Action", "SendSms");
            commonParam.Add("Version", "2017-05-25");
            commonParam.Add("RegionId", "cn-hangzhou");
            commonParam.Add("PhoneNumbers", phoneNumbers.ConvertListToString(','));
            commonParam.Add("SignName", GetSmsConfig(smsConfigJson).SignName);
            commonParam.Add("TemplateCode", templateCode);
            commonParam.Add("TemplateParam", _jsonProvider.Serializer(content));

            string sign = Util.CreateSign(commonParam, GetSmsConfig(smsConfigJson).EncryptionKey);
            commonParam.Add("Signature", sign);
            RestRequest request = new RestRequest(Method.GET);
            foreach (var key in commonParam.Keys)
            {
                request.AddQueryParameter(key, commonParam[key]);
            }

            var response = _restClient.Execute(request);
            SendSmsResponse result = XmlCommon.Deserialize<SendSmsResponse>(response.Content);
            if (result.Code == "OK")
            {
                return true;
            }

            loseAction?.Invoke(new SendSmsLoseDto()
            {
                PhoneList = phoneNumbers,
                Msg = "短信发送失败",
                SubMsg = response.Content,
                Code = result.Code
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
        /// <param name="smsConfigJson">短信配置Json串</param>
        /// <returns></returns>
        public bool Send(string phoneNumber, string templateCode, object content,
            Action<SendSmsLoseDto> loseAction = null, string smsConfigJson = "")
        {
            return Send(new List<string>() {phoneNumber}, templateCode, content, loseAction, smsConfigJson);
        }

        #endregion

        #region private methods

        #region 获取阿里大于配置

        /// <summary>
        /// 获取阿里大于配置
        /// </summary>
        /// <param name="smsConfigJson">自定义短信配置</param>
        /// <returns></returns>
        public AliSmsConfig GetSmsConfig(string smsConfigJson)
        {
            if (!string.IsNullOrEmpty(smsConfigJson))
            {
                _smsConfig = _jsonProvider.Deserialize<AliSmsConfig>(smsConfigJson);
            }

            new AliYunConfigValidator().Validate(_smsConfig).Check();
            return _smsConfig;
        }

        #endregion

        #endregion
    }
}
