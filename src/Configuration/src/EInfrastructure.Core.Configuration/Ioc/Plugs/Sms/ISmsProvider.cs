// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Dto;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Sms
{
    /// <summary>
    /// 短信
    /// </summary>
    public interface ISmsProvider : ISingleInstance, IIdentify
    {
        #region 指定短信列表发送短信

        /// <summary>
        /// 指定短信列表发送短信
        /// </summary>
        /// <param name="phoneNumbers">手机号</param>
        /// <param name="templateCode">短信模板</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        List<SendSmsResponseDto> Send(List<string> phoneNumbers, string templateCode,
            List<KeyValuePair<string, string>> content);

        #endregion

        #region 指定单个手机号发送短信

        /// <summary>
        /// 指定单个手机号发送短信
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="templateCode">短信模板</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        SendSmsResponseDto Send(string phoneNumber, string templateCode, List<KeyValuePair<string, string>> content);

        #endregion
    }
}
