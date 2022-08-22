// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.


using System;
using EInfrastructure.Core.Aliyun.Storage.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EInfrastructure.Core.Aliyun.Storage.Tests.Base
{
    /// <summary>
    /// base unit test
    /// </summary>
    public class BaseUnitTest
    {
        protected IServiceProvider provider;

        /// <summary>
        ///
        /// </summary>
        public BaseUnitTest()
        {
            var services = new ServiceCollection();
            services.AddAliYunStorage(() =>
            {
                return new ALiYunStorageConfig("LTAI4G3eSHefUdzXT2BdaKVo", "h88AYXo49Xp89ir418fgtavpMzoy7m");
            });
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Information);
                loggingBuilder.AddConsole();
            });
            provider = AutoFac.AutofacAutoRegister.Use(services, builder => { });
        }
    }
}
