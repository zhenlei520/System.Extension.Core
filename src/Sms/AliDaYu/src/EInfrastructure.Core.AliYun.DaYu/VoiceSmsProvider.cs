// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

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
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Enum;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params.VoiceSms;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Validation.Common;
using SendSmsResponseDto = EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Dto.Sms.SendSmsResponseDto;

namespace EInfrastructure.Core.AliYun.DaYu
{
    /// <summary>
    /// 语音短信
    /// </summary>
    public class VoiceSmsProvider : BaseSmsProvider, IVoiceSmsProvider
    {
        private readonly IJsonProvider _jsonProvider;

        /// <summary>
        ///
        /// </summary>
        /// <param name="smsConfig"></param>
        public VoiceSmsProvider(AliSmsConfig smsConfig) : this(smsConfig, new List<IJsonProvider>
        {
            new NewtonsoftJsonProvider()
        })
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="smsConfig"></param>
        /// <param name="jsonProviders"></param>
        public VoiceSmsProvider(AliSmsConfig smsConfig, ICollection<IJsonProvider> jsonProviders) : base(smsConfig)
        {
            _jsonProvider = InjectionSelectionCommon.GetImplement(jsonProviders);
        }

        #region 权重

        /// <summary>
        /// 权重
        /// </summary>
        /// <returns></returns>
        public int GetWeights()
        {
            return 99;
        }

        #endregion

        #region 获取唯一标识

        /// <summary>
        /// 获取唯一标识
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }

        #endregion

        #region 发送语音短信

        /// <summary>
        /// 发送语音短信
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public SmsResponseDto<SendSmsResponseDto> SendVoiceSms(SendVoiceSmsParam param)
        {
            new SendVoiceSmsParamValidator().Validate(param).Check(HttpStatus.Err.Name);
            CommonRequest request =
                base.GetRequest("dyvmsapi.aliyuncs.com", "SingleCallByTts", "2017-05-25", "cn-hangzhou");
            request.AddQueryParameters("CalledNumber", param.Phone);
            request.AddQueryParameters("CalledShowNumber", param.CalledShowNumber);
            request.AddQueryParameters("TtsCode", param.TemplateCode);
            request.AddQueryParameters("PlayTimes", param.PlatTimes.ToString()); //播放次数
            request.AddQueryParameters("Volume", param.Volume.ToString()); //播放音量
            Dictionary<string, string> data = new Dictionary<string, string>();
            param.Content.ForEach(item => { data.Add(item.Key, item.Value); });
            request.AddQueryParameters("TtsParam", _jsonProvider.Serializer(data));
            try
            {
                CommonResponse response = GetClient().GetCommonResponse(request);
                if (response != null)
                {
                    var res = _jsonProvider
                        .Deserialize<SendVoiceSmsResponseDto>(
                            response.Data);
                    if (res != null)
                    {
                        SmsCode smsCode = SmsCodeMap.Where(x => x.Key == res.Code).Select(x => x.Value)
                            .FirstOrDefault();

                        if (smsCode != default(SmsCode))
                        {
                            return new SmsResponseDto<SendSmsResponseDto>()
                            {
                                Code = smsCode,
                                Msg = smsCode == SmsCode.Ok ? "success" : "lose",
                                Extend = new SendSmsResponseDto(param.Phone, new SendSmsExtend()
                                {
                                    BizId = smsCode == SmsCode.Ok
                                        ? _jsonProvider
                                            .Deserialize<SendVoiceSmsSuccessResponseDto>(
                                                response.Data).CallId
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

            return new SmsResponseDto<SendSmsResponseDto>()
            {
                Code = SmsCode.Unknown,
                Msg = "发送异常"
            };
        }

        #endregion
    }
}
