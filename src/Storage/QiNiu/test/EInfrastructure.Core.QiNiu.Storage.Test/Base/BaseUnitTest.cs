// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.


using System;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Enum;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EInfrastructure.Core.QiNiu.Storage.Test.Base
{
    /// <summary>
    /// base unit test
    /// </summary>
    public class BaseUnitTest
    {
        protected IServiceProvider provider;

        public BaseUnitTest()
        {
            var services = new ServiceCollection();
            services.AddQiNiuStorage(() =>
            {
                return new QiNiuStorageConfig("accessKey","secretKey",ZoneEnum.ZoneCnSouth,"host","buck");
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
