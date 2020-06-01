// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.


using System;
using EInfrastructure.Core.HelpCommon.Randoms;
using EInfrastructure.Core.HelpCommon.Randoms.Interface;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Test.Base
{
    /// <summary>
    /// base unit test
    /// </summary>
    public class BaseUnitTest
    {
        protected readonly ITestOutputHelper output;
        protected IServiceProvider provider;

        public BaseUnitTest(ITestOutputHelper output)
        {
            var services = new ServiceCollection();
            this.output = output;
            services.AddSingleton<IRandomBuilder, RandomCommon>();
//            services.Configure<>(connectionString);////add using CNBlogs.Ad.Bootstrapper;
            provider = services.BuildServiceProvider();
        }
    }
}
