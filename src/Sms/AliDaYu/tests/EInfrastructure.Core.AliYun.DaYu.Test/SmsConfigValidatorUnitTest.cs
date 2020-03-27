// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.AliYun.DaYu.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params;
using Xunit;

namespace EInfrastructure.Core.AliYun.DaYu.Test
{
    /// <summary>
    ///
    /// </summary>
    public class SmsConfigValidatorUnitTest
    {
        /// <summary>
        /// 普通短信
        /// </summary>
        [Fact]
        public void SendSms()
        {
            try
            {
                var result = new SmsProvider(new AliSmsConfig()
                {
                    AccessKey = "",
                    EncryptionKey = "",
                }).SendSms(new SendSmsParam()
                {
                    Phone = "",
                    TemplateCode = "",
                    SignName = "",
                    Content = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("code", "3982")
                    }
                });
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 语音短信
        /// </summary>
        [Fact]
        public void SendVoiceSms()
        {
            try
            {
                var result = new SmsProvider(new AliSmsConfig()
                {
                    AccessKey = "",
                    EncryptionKey = "",
                }).SendVoiceSms(new SendVoiceSmsParam()
                {
                    Phone = "",
                    TemplateCode = "",
                    CalledShowNumber="",
                    Content = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("code", "3982")
                    }
                });
            }
            catch (Exception ex)
            {
            }
        }
    }
}
