// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.


using System;
using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Aliyun.Storage.Enum;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.Aliyun.Storage.Test.Base
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
            services.AddQiNiuStorage(() => { return new ALiYunStorageConfig("", "", ZoneEnum.HangZhou, "", ""); });
            provider = EInfrastructure.Core.AutoFac.AutofacAutoRegister.Use(services, builder => { });
        }
    }
}
