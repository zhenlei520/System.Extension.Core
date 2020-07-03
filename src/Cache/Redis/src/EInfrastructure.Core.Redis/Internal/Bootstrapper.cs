// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Redis.Config;
using Microsoft.Extensions.Hosting;

namespace EInfrastructure.Core.Redis.Internal
{
    /// <summary>
    /// Default implement of
    /// </summary>
    internal class Bootstrapper : BackgroundService
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly RedisConfig _redisConfig;

        public Bootstrapper(ICollection<ICacheProvider> cacheProviders, RedisConfig redisConfig)
        {
            _cacheProvider = cacheProviders.FirstOrDefault(x => x.GetIdentify() == "EInfrastructure.Core.Redis");
            _redisConfig = redisConfig;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _cacheProvider.ClearOverTimeHashKey();
                    Thread.Sleep(_redisConfig.Timer);
                }
            }, stoppingToken);

            await Task.CompletedTask;
        }
    }
}
