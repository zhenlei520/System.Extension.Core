// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using EInfrastructure.Core.AliYun.DaYu.Config;
using EInfrastructure.Core.AliYun.DaYu.Model.SendSms;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Enum;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Http;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Validation.Common;

namespace EInfrastructure.Core.AliYun.DaYu
{
    /// <summary>
    /// 短信
    /// </summary>
    public class SmsProvider : BaseSmsProvider, ISmsProvider
    {
        private readonly IJsonProvider _jsonProvider;

        /// <summary>
        /// 短信服务
        /// </summary>
        public SmsProvider(AliSmsConfig smsConfig) : this(smsConfig, new List<IJsonProvider>()
        {
            new NewtonsoftJsonProvider(),
        })
        {
        }

        /// <summary>
        /// 短信服务
        /// </summary>
        public SmsProvider(AliSmsConfig smsConfig, ICollection<IJsonProvider> jsonProviders) : base(smsConfig)
        {
            _smsConfig = smsConfig;
            _jsonProvider = InjectionSelectionCommon.GetImplement(jsonProviders);
            ValidationCommon.Check(smsConfig, "请完善阿里云短信配置信息", HttpStatus.Err.Name);
        }

        readonly HttpClient _smsClient = new HttpClient("http://dysmsapi.aliyuncs.com");

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

        #region 指定手机号列表发送同一短信

        /// <summary>
        /// 指定手机号列表发送同一短信
        /// </summary>
        /// <param name="phoneNumbers">手机号</param>
        /// <param name="templateCode">短信模板</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public List<Configuration.Ioc.Plugs.Sms.Dto.SendSmsResponseDto> Send(List<string> phoneNumbers,
            string templateCode,
            List<KeyValuePair<string, string>> content)
        {
            List<Configuration.Ioc.Plugs.Sms.Dto.SendSmsResponseDto> responseList =
                new List<Configuration.Ioc.Plugs.Sms.Dto.SendSmsResponseDto>();
            foreach (var phone in phoneNumbers)
            {
                responseList.Add(Send(phone, templateCode, content));
            }

            return responseList;
        }

        #endregion

        #region 指定单个手机号发送短信

        /// <summary>
        /// 指定单个手机号发送短信
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="templateCode">短信模板</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public Configuration.Ioc.Plugs.Sms.Dto.SendSmsResponseDto Send(string phoneNumber,
            string templateCode,
            List<KeyValuePair<string, string>> content)
        {
            CommonRequest request = base.GetRequest("SendSms", "2017-05-25", "cn-hangzhou");
            request.AddQueryParameters("PhoneNumbers", phoneNumber);
            request.AddQueryParameters("SignName", _smsConfig.SignName);
            request.AddQueryParameters("TemplateCode", templateCode);
            Dictionary<string, string> data = new Dictionary<string, string>();
            content.ForEach(item => { data.Add(item.Key, item.Value); });
            request.AddQueryParameters("TemplateParam", _jsonProvider.Serializer(data));
            try
            {
                CommonResponse response = GetClient().GetCommonResponse(request);
                if (response != null)
                {
                    var res = _jsonProvider
                        .Deserialize<Model.SendSms.SendSmsResponseDto>(
                            response.Data);
                    if (res != null)
                    {
                        SmsCode smsCode = SmsCodeMap.Where(x => x.Key == res.Code).Select(x => x.Value)
                            .FirstOrDefault();

                        if (smsCode != default(SmsCode))
                        {
                            return new Configuration.Ioc.Plugs.Sms.Dto.SendSmsResponseDto(
                                phoneNumber)
                            {
                                Code = smsCode,
                                Msg = smsCode == SmsCode.Ok ? "success" : "lose",
                                Extend = new SendSmsExtend()
                                {
                                    BizId = smsCode == SmsCode.Ok
                                        ? _jsonProvider
                                            .Deserialize<SendSmsSuccessResponseDto>(
                                                response.Data).BizId
                                        : "",
                                    RequestId = res.RequestId,
                                    Msg = res.Message
                                }
                            };
                        }
                    }
                }
            }
            catch (ServerException e)
            {
            }

            return new Configuration.Ioc.Plugs.Sms.Dto.SendSmsResponseDto(phoneNumber)
            {
                Code = SmsCode.Unknown,
                Msg = "发送异常"
            };
        }

        #endregion

        #region 发送语音短信

        /// <summary>
        /// 发送语音短信
        /// </summary>
        /// <param name="phoneNumber">接受语音的手机号</param>
        /// <param name="calledShowNumber">被叫显号，必须是已购买的号码</param>
        /// <param name="templateCode"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Configuration.Ioc.Plugs.Sms.Dto.SendSmsResponseDto SendVoice(string phoneNumber, string calledShowNumber,
            string templateCode,
            List<KeyValuePair<string, string>> content)
        {
            CommonRequest request = base.GetRequest("SingleCallByTts", "2017-05-25", "cn-hangzhou");
            request.AddQueryParameters("CalledNumber", phoneNumber);
            request.AddQueryParameters("CalledShowNumber", calledShowNumber);
            request.AddQueryParameters("TtsCode", templateCode);
            request.AddQueryParameters("PlayTimes", "3"); //播放次数
            request.AddQueryParameters("Volume", templateCode); //播放音量
            Dictionary<string, string> data = new Dictionary<string, string>();
            content.ForEach(item => { data.Add(item.Key, item.Value); });
            request.AddQueryParameters("TtsParam", _jsonProvider.Serializer(data));
            try
            {
                CommonResponse response = GetClient().GetCommonResponse(request);
                if (response != null)
                {
                    var res = _jsonProvider
                        .Deserialize<Model.SendSms.SendSmsResponseDto>(
                            response.Data);
                    if (res != null)
                    {
                        SmsCode smsCode = SmsCodeMap.Where(x => x.Key == res.Code).Select(x => x.Value)
                            .FirstOrDefault();

                        if (smsCode != default(SmsCode))
                        {
                            return new Configuration.Ioc.Plugs.Sms.Dto.SendSmsResponseDto(
                                phoneNumber)
                            {
                                Code = smsCode,
                                Msg = smsCode == SmsCode.Ok ? "success" : "lose",
                                Extend = new SendSmsExtend()
                                {
                                    BizId = smsCode == SmsCode.Ok
                                        ? _jsonProvider
                                            .Deserialize<SendSmsSuccessResponseDto>(
                                                response.Data).BizId
                                        : "",
                                    RequestId = res.RequestId,
                                    Msg = res.Message
                                }
                            };
                        }
                    }
                }
            }
            catch (ServerException e)
            {
            }

            return new Configuration.Ioc.Plugs.Sms.Dto.SendSmsResponseDto(phoneNumber)
            {
                Code = SmsCode.Unknown,
                Msg = "发送异常"
            };
        }

        #endregion

        #region 返回权重

        /// <summary>
        /// 返回权重
        /// </summary>
        /// <returns></returns>
        public int GetWeights()
        {
            return 99;
        }

        #endregion
    }
}
