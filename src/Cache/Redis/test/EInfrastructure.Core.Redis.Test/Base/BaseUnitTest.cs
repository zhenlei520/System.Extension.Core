// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.


using System.Collections.Generic;
using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Config.SerializeExtensions.Interfaces;
using EInfrastructure.Core.Redis.Config;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Redis.Test.Base
{
    /// <summary>
    /// base unit test
    /// </summary>
    public class BaseUnitTest
    {
        protected readonly RedisCacheService _redisCacheService;

        public BaseUnitTest(ITestOutputHelper output)
        {
            _redisCacheService = new RedisCacheService(new RedisConfig()
            {
                Ip = "122.114.19.229",
                Port = 6379,
                DataBase = 3,
                Name = "einfrastructure_",
                Password = "dysh_enjoy",
                PoolSize = 1,
                OverTimeCacheKeyPre = "OverTime_HashSet",
            }, new JsonService(new List<IJsonProvider>()
            {
                new NewtonsoftJsonProvider()
            }));
//            var connectionString = "";
//            var services = new ServiceCollection();
//            this.output = output;
//            services.AddSingleton<IRandomBuilder, RandomCommon>();
//            provider = services.BuildServiceProvider();
        }
    }
}
