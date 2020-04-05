// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Dto.Sms;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params.Sms;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Sms
{
    /// <summary>
    /// 短信
    /// </summary>
    public interface ISmsProvider : ISingleInstance, IIdentify
    {
        /// <summary>
        /// 指定单个手机号发送短信
        /// </summary>
        /// <param name="param">短信参数</param>
        /// <returns></returns>
        SmsResponseDto<SendSmsResponseDto> SendSms(SendSmsParam param);

        /// <summary>
        /// 查看短信发送记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        SmsResponseDto<SendSmsRecordDto> GetRecords(SendSmsRecordParam param);
    }
}
