// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.


using System;
using EInfrastructure.Core.Tools.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EInfrastructure.Core.Test.Base
{
    /// <summary>
    /// base unit test
    /// </summary>
    public class BaseUnitTest
    {
        protected IServiceProvider provider;

        public BaseUnitTest()
        {
        }

        /// <summary>
        ///
        /// </summary>
        protected void GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IRandomProvider, IRandomProvider>();
            services.AddSingleton<ITestOutputHelper, TestOutputHelper>();
            provider = services.BuildServiceProvider();
        }
    }
}
