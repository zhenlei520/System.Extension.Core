// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.AliYun.DaYu.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Sms.Params;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Serialize.Xml;
using Xunit;

namespace EInfrastructure.Core.AliYun.DaYu.Test
{
    /// <summary>
    ///
    /// </summary>
    public class SmsConfigValidatorUnitTest
    {
        /// <summary>
        /// 检查sms配置检查
        /// </summary>
        [Fact]
        public void CheckSmsConfigValidaor()
        {
            try
            {
                var result2 = new SmsProvider(new AliSmsConfig()
                {
                    AccessKey = "LTAIDUtupDgzqDVr",
                    EncryptionKey = "vwV9ToOmooV8gKeYUFQRcPcmmGbYCt",
                }).SendVoiceSms(new SendVoiceSmsParam()
                {
                    Phone = "13653771007",
                    TemplateCode = "TTS_177536483",
                    CalledShowNumber="02566040803",
                    Content = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("code", "3982")
                    }
                });

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
    }
}
