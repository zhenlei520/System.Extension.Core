// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Redis.Config;
using EInfrastructure.Core.Redis.Validator;
using EInfrastructure.Core.Validation.Common;
using Xunit;

namespace EInfrastructure.Core.Redis.Test
{
    public class RedisConfigValidatorUnitTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void RedisConfigValidateTest()
        {
            RedisConfig redisConfig = new RedisConfig()
            {
                DataBase = 0,
                Ip = "123",
                Name = "test",
                Password = "123",
                PoolSize = 1,
                Port = 2
            };
            new RedisConfigValidator().Validate(redisConfig).Check();
        }
    }
}