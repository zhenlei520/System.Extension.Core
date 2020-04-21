// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using EInfrastructure.Core.AliYun.DaYu.Config;
using EInfrastructure.Core.AliYun.DaYu.Model.SendSms;
using EInfrastructure.Core.AliYun.DaYu.Validator;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Dto.Sms;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Enum;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params.Sms;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Validation.Common;
using SendSmsResponseDto = EInfrastructure.Core.AliYun.DaYu.Model.SendSms.SendSmsResponseDto;

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
            _jsonProvider = InjectionSelectionCommon.GetImplement(jsonProviders);
            ValidationCommon.Check(smsConfig, "请完善阿里云短信配置信息", HttpStatus.Err.Name);
        }

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

        #region 指定单个手机号发送短信

        /// <summary>
        /// 指定单个手机号发送短信
        /// </summary>
        /// <param name="param">短信参数</param>
        /// <returns></returns>
        public SmsResponseDto<Configuration.Ioc.Plugs.Sms.Dto.Sms.SendSmsResponseDto> SendSms(SendSmsParam param)
        {
            new SendSmsParamValidator().Validate(param).Check(HttpStatus.Err.Name);
            CommonRequest request = base.GetRequest("dysmsapi.aliyuncs.com", "SendSms", "2017-05-25", "cn-hangzhou");
            request.AddQueryParameters("PhoneNumbers", param.Phone);
            request.AddQueryParameters("SignName", param.SignName);
            request.AddQueryParameters("TemplateCode", param.TemplateCode);
            Dictionary<string, string> data = new Dictionary<string, string>();
            param.Content.ForEach(item => { data.Add(item.Key, item.Value); });
            request.AddQueryParameters("TemplateParam", _jsonProvider.Serializer(data));
            try
            {
                CommonResponse response = GetClient().GetCommonResponse(request);
                if (response != null)
                {
                    var res = _jsonProvider
                        .Deserialize<SendSmsResponseDto>(
                            response.Data);
                    if (res != null)
                    {
                        SmsCode smsCode = SmsCodeMap.Where(x => x.Key == res.Code).Select(x => x.Value)
                            .FirstOrDefault();

                        if (smsCode != default(SmsCode))
                        {
                            return new SmsResponseDto<Configuration.Ioc.Plugs.Sms.Dto.Sms.SendSmsResponseDto>()
                            {
                                Code = smsCode,
                                Msg = smsCode == SmsCode.Ok ? "success" : "lose",
                                Extend = new Configuration.Ioc.Plugs.Sms.Dto.Sms.SendSmsResponseDto(param.Phone,
                                    new SendSmsExtend()
                                    {
                                        BizId = smsCode == SmsCode.Ok
                                            ? _jsonProvider
                                                .Deserialize<SendSmsSuccessResponseDto>(
                                                    response.Data).BizId
                                            : "",
                                        RequestId = res.RequestId,
                                        Msg = res.Message
                                    })
                            };
                        }
                    }
                }
            }
            catch (ServerException e)
            {
            }

            return new SmsResponseDto<Configuration.Ioc.Plugs.Sms.Dto.Sms.SendSmsResponseDto>()
            {
                Code = SmsCode.Unknown,
                Msg = "发送异常"
            };
        }

        #endregion

        #region 查看短信发送记录

        /// <summary>
        /// 查看短信发送记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public SmsResponseDto<SendSmsRecordDto> GetRecords(SendSmsRecordParam param)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
