// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.


using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Redis.Config;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Redis.Tests.Base
{
    /// <summary>
    /// base unit test
    /// </summary>
    public class BaseUnitTest
    {
        protected readonly RedisCacheProvider _redisCacheService;

        public BaseUnitTest(ITestOutputHelper output)
        {
            _redisCacheService = new RedisCacheProvider(new RedisConfig()
            {
                Ip = "localhost",
                Port = 6379,
                DataBase = 1,
                Prefix = "",
                Password = "",
                PoolSize = 1,
                OverTimeCacheKeyPre = "OverTime_HashSet",
            }, new List<IJsonProvider>()
            {
                new NewtonsoftJsonProvider()
            });
        }
    }
}
