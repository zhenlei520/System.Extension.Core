// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.Interface.Sms.Dto;

namespace EInfrastructure.Core.Interface.Sms
{
    /// <summary>
    /// 短信
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// 指定短信列表发送短信
        /// </summary>
        /// <param name="phoneNumbers">手机号</param>
        /// <param name="templateCode">短信模板</param>
        /// <param name="content">内容</param>
        /// <param name="loseAction">失败回调函数</param>
        /// <param name="smsConfigJson">短信配置Json串（优先使用此配置）</param>
        /// <returns></returns>
        bool Send(List<string> phoneNumbers, string templateCode, object content,
            Action<SendSmsLoseDto> loseAction = null, string smsConfigJson = "");

        /// <summary>
        /// 指定单个手机号发送短信
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="templateCode">短信模板</param>
        /// <param name="content">内容</param>
        /// <param name="loseAction">失败回调函数</param>
        /// <param name="smsConfigJson">短信配置Json串（优先使用此配置）</param>
        /// <returns></returns>
        bool Send(string phoneNumber, string templateCode, object content, Action<SendSmsLoseDto> loseAction = null,
            string smsConfigJson = "");
    }
}