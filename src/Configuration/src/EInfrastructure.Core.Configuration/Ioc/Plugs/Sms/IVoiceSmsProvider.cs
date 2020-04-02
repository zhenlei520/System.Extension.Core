// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Dto.Sms;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params.VoiceSms;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Sms
{
    /// <summary>
    /// 语音短信
    /// </summary>
    public interface IVoiceSmsProvider: ISingleInstance, IIdentify
    {
        /// <summary>
        /// 发送语音短信
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        SmsResponseDto<SendSmsResponseDto> SendVoiceSms(SendVoiceSmsParam param);
    }
}
