// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using EInfrastructure.Core.AliYun.DaYu.Common;
using EInfrastructure.Core.AliYun.DaYu.Config;
using EInfrastructure.Core.AliYun.DaYu.Model;
using EInfrastructure.Core.AliYun.DaYu.Model.SendSms;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Enum;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Http;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Serialize.Xml;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Validation.Common;
using RestSharp;

namespace EInfrastructure.Core.AliYun.DaYu
{
    /// <summary>
    /// 短信
    /// </summary>
    public class SmsProvider : ISmsProvider
    {
        private AliSmsConfig _smsConfig;
        private readonly IJsonProvider _jsonProvider;
        private readonly IXmlProvider _xmlProvider;

        /// <summary>
        /// 短信服务
        /// </summary>
        public SmsProvider(AliSmsConfig smsConfig) : this(smsConfig, new List<IJsonProvider>()
        {
            new NewtonsoftJsonProvider(),
        }, new List<IXmlProvider>()
        {
            new XmlProvider()
        })
        {
        }

        /// <summary>
        /// 短信服务
        /// </summary>
        public SmsProvider(AliSmsConfig smsConfig, ICollection<IJsonProvider> jsonProviders,
            ICollection<IXmlProvider> xmlProviders)
        {
            _smsConfig = smsConfig;
            _jsonProvider = InjectionSelectionCommon.GetImplement(jsonProviders);
            _xmlProvider = InjectionSelectionCommon.GetImplement(xmlProviders);
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

        #region 指定短信列表发送短信

        /// <summary>
        /// 指定短信列表发送短信
        /// </summary>
        /// <param name="phoneNumbers">手机号</param>
        /// <param name="templateCode">短信模板</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public List<SendSmsResponseDto> Send(List<string> phoneNumbers, string templateCode,
            List<KeyValuePair<string, string>> content)
        {
            List<SendSmsResponseDto> responseList = new List<SendSmsResponseDto>();
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
        public SendSmsResponseDto Send(string phoneNumber, string templateCode,
            List<KeyValuePair<string, string>> content)
        {
            if (content.Any(x => x.Key.Length >= 20 || x.Value.Length >= 20))
            {
                throw new BusinessException<string>("请确保短信参数以及短信内容不超过20个字符", "Param Error");
            }

            Dictionary<string, string> data = new Dictionary<string, string>();
            content.ForEach(item => { data.Add(item.Key, item.Value); });
            Dictionary<string, string> commonParam = Util.BuildCommonParam(_smsConfig.AccessKey);
            commonParam.Add("Action", "SendSms");
            commonParam.Add("Version", "2017-05-25");
            commonParam.Add("RegionId", "cn-hangzhou");
            commonParam.Add("PhoneNumbers", phoneNumber);
            commonParam.Add("SignName", _smsConfig.SignName);
            commonParam.Add("TemplateCode", templateCode);
            commonParam.Add("TemplateParam", _jsonProvider.Serializer(data));
            string sign = Util.CreateSign(commonParam, _smsConfig.EncryptionKey);
            commonParam.Add("Signature", sign);

            var response = _smsClient.GetString("", commonParam);

            if (string.IsNullOrEmpty(response))
            {
                return new SendSmsResponseDto(phoneNumber)
                {
                    Code = SmsCode.Unknown,
                    Msg = "发送异常"
                };
            }

            var xmlElement = XmlCommon.GetXmlElement(response);
            if (xmlElement != null)
            {
                if (xmlElement.Name == "SendSmsResponse")
                {
                    var result =
                        _xmlProvider.Deserialize<SendSmsSuccessResponse>(response, Encoding.UTF8, (ex) => null);
                    if (result != null)
                    {
                        SmsCode smsCode = SmsCodeMap.Where(x => x.Key == result.Code).Select(x => x.Value)
                            .FirstOrDefault();
                        if (smsCode != default(SmsCode))
                        {
                            return new SendSmsResponseDto(phoneNumber)
                            {
                                Code = smsCode,
                                Msg = smsCode == SmsCode.Ok ? "success" : "lose",
                                Extend = new SendSmsExtend()
                                {
                                    BizId = "",
                                    RequestId = result.RequestId,
                                    Msg = result.Message
                                }
                            };
                        }
                    }
                }
                else if (xmlElement.Name == "Error")
                {
                    var result = _xmlProvider.Deserialize<SendSmsErrorResponse>(response, Encoding.UTF8, (ex) => null);
                    if (result != null)
                    {
                        SmsCode smsCode = SmsCodeMap.Where(x => x.Key == result.Code).Select(x => x.Value)
                            .FirstOrDefault();
                        if (smsCode != default(SmsCode))
                        {
                            return new SendSmsResponseDto(phoneNumber)
                            {
                                Code = smsCode,
                                Msg = smsCode == SmsCode.Ok ? "success" : "lose",
                                Extend = new SendSmsExtend()
                                {
                                    BizId = "",
                                    RequestId = result.RequestId,
                                    Msg = result.Message
                                }
                            };
                        }
                    }
                }
            }

            return new SendSmsResponseDto(phoneNumber)
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

        #region private methods

        /// <summary>
        /// 短信验证码
        /// </summary>
        private Dictionary<string, SmsCode> SmsCodeMap = new Dictionary<string, SmsCode>()
        {
            {"OK", SmsCode.Ok},
            {"isv.TEMPLATE_MISSING_PARAMETERS", SmsCode.TemplateIllegal},
            {"isv.SMS_TEMPLATE_ILLEGAL", SmsCode.TemplateIllegal},
            {"isv.SMS_SIGNATURE_ILLEGAL", SmsCode.SignIllegal},
            {"isv.MOBILE_NUMBER_ILLEGAL", SmsCode.MobileNumberIllegal},
            {"isv.BUSINESS_LIMIT_CONTROL", SmsCode.BusinessLimitControl},
            {"isv.AMOUNT_NOT_ENOUGH", SmsCode.AmountNotEnough},
            {"isp.RAM_PERMISSION_DENY", SmsCode.InsufficientPrivileges},
            {"isv.OUT_OF_SERVICE", SmsCode.BusinessStop},
            {"isv.ACCOUNT_NOT_EXISTS", SmsCode.AbnormalAccount},
            {"isv.ACCOUNT_ABNORMAL", SmsCode.AbnormalAccount},
            {"isv.BLACK_KEY_CONTROL_LIMIT", SmsCode.BlackKeyControlLimit},
            {"isv.INVALID_PARAMETERS", SmsCode.InvalidParameters},
            {"isv.PARAM_LENGTH_LIMIT", SmsCode.LengthError},
            {"isv.INVALID_JSON_PARAM", SmsCode.InvalidParameters}
        };

        #endregion
    }
}
