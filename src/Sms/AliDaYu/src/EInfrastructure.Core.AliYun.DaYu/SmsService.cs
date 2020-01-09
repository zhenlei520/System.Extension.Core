// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;
using EInfrastructure.Core.AliYun.DaYu.Common;
using EInfrastructure.Core.AliYun.DaYu.Config;
using EInfrastructure.Core.AliYun.DaYu.Model;
using EInfrastructure.Core.Config.EnumerationExtensions;
using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Config.SerializeExtensions.Interfaces;
using EInfrastructure.Core.Config.SmsExtensions;
using EInfrastructure.Core.Config.SmsExtensions.Dto;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Tools;
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
        private readonly IJsonService _jsonProvider;

        /// <summary>
        /// 短信服务
        /// </summary>
        public SmsService(AliSmsConfig smsConfig) : this(smsConfig, new JsonService(new List<IJsonProvider>()
        {
            new NewtonsoftJsonProvider()
        }))
        {
        }

        /// <summary>
        /// 短信服务
        /// </summary>
        public SmsService(AliSmsConfig smsConfig, IJsonService jsonProvider)
        {
            _smsConfig = smsConfig;
            _jsonProvider = jsonProvider;
            smsConfig.Check("请完善阿里云短信配置信息",HttpStatus.Err.Name);
            if (_jsonProvider == null)
            {
                throw new ArgumentNullException(nameof(jsonProvider));
            }
        }

        readonly RestClient _restClient = new RestClient("http://dysmsapi.aliyuncs.com");

        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
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
        /// <returns></returns>
        public bool Send(List<string> phoneNumbers, string templateCode, object content,
            Action<SendSmsLoseDto> loseAction = null)
        {
            Dictionary<string, string> commonParam = Util.BuildCommonParam(_smsConfig.AccessKey);
            commonParam.Add("Action", "SendSms");
            commonParam.Add("Version", "2017-05-25");
            commonParam.Add("RegionId", "cn-hangzhou");
            commonParam.Add("PhoneNumbers", phoneNumbers.ConvertListToString(','));
            commonParam.Add("SignName", _smsConfig.SignName);
            commonParam.Add("TemplateCode", templateCode);
            commonParam.Add("TemplateParam", _jsonProvider.Serializer(content));

            string sign = Util.CreateSign(commonParam, _smsConfig.EncryptionKey);
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
        /// <returns></returns>
        public bool Send(string phoneNumber, string templateCode, object content,
            Action<SendSmsLoseDto> loseAction = null)
        {
            return Send(new List<string>() {phoneNumber}, templateCode, content, loseAction);
        }

        #endregion
    }
}
