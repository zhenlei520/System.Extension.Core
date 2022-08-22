// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Redis.Tests.Base;
using EInfrastructure.Core.Tools;
using Xunit;
using Xunit.Abstractions;

namespace EInfrastructure.Core.Redis.Tests
{
    public class RedisConfigValidatorUnitTest : BaseUnitTest
    {
        public RedisConfigValidatorUnitTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        ///
        /// </summary>
        [Theory]
        [InlineData("key", "hashKey", "123", 5)]
        public void HashSet(string key, string hashKey, string value, long expire)
        {
            Check.True(_redisCacheService.HashSet(key, hashKey, value, expire, true), "方法有误");
        }

        /// <summary>
        ///
        /// </summary>
        [Theory]
        [InlineData("key2", "hashKey1,hashKey2", "123,345", 5000)]
        public void HashSet2(string key, string hashKey, string value, long expire)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>();
            for (int i = 0; i < hashKey.Split(',').Length; i++)
            {
                dics.Add(hashKey.Split(',')[i], value.Split(',')[i]);
            }

            Check.True(_redisCacheService.HashSet(key, dics, expire, true), "方法有误");
        }

        /// <summary>
        ///
        /// </summary>
        [Theory]
        [InlineData("key3,key4", "hashKey1,hashKey4", "123,345", 5)]
        public void HashSet3(string key, string hashKey, string value, long expire)
        {
            Dictionary<string, Dictionary<string, string>> dics = new Dictionary<string, Dictionary<string, string>>();
            for (int i = 0; i < hashKey.Split(',').Length; i++)
            {
                dics.Add(key.Split(',')[i], new Dictionary<string, string>()
                {
                    {hashKey.Split(',')[i], value.Split(',')[i]}
                });
            }

            Check.True(_redisCacheService.HashSet(dics, expire, true), "方法有误");
        }

        [Fact]
        public void GetOverTimeKey()
        {
            var list=_redisCacheService.SortedSetRangeByRankAndOverTime(1000);
        }

        [Fact]
        public void ClearOverTimeHashCache()
        {
            Check.True(_redisCacheService.ClearOverTimeHashKey(1000),"方法异常");
        }

       /// <summary>
       ///
       /// </summary>
       /// <param name="key"></param>
        [Theory]
        [InlineData("key3")]
        public void StringGet(string key)
       {
           while (true)
           {
               var test=_redisCacheService.StringGet(key);
           }
       }
    }
}
