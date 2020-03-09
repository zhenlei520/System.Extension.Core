// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.AliYun.DaYu.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
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
                new SmsService(new AliSmsConfig(), new List<IJsonProvider>()
                {
                    new NewtonsoftJsonProvider()
                }, new List<IXmlProvider>()
                {
                    new XmlProvider()
                }).Send(new List<string>(), "", "");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
