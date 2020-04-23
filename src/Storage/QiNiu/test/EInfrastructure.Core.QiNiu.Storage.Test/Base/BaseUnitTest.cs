// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.


using System;
using EInfrastructure.Core.HelpCommon.Randoms;
using EInfrastructure.Core.HelpCommon.Randoms.Interface;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Enum;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit.Abstractions;

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
                return new QiNiuStorageConfig("AfgRCCFwzEB5BUNMX7xkN-shv8icPueyDU1GPleX",
                    "qHOKkPU1_X345-69nd8UfMkSwXds7IUA0mjNMJB1", ZoneEnum.ZoneCnSouth,
                    "http://test.storage.bflove.cn", "test");
                // return new QiNiuStorageConfig("","",ZoneEnum.ZoneCnSouth,"","");
            });
            provider = EInfrastructure.Core.AutoFac.AutofacAutoRegister.Use(services, builder => { });
        }
    }
}
