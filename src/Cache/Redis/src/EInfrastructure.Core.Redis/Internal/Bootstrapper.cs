// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using Microsoft.Extensions.Hosting;

namespace EInfrastructure.Core.Redis.Internal
{
    /// <summary>
    /// Default implement of
    /// </summary>
    internal class Bootstrapper : BackgroundService, IBootstrapper
    {
        private ICacheProvider CacheProvider { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cacheProvider"></param>
        public Bootstrapper(ICacheProvider cacheProvider)
        {
            CacheProvider = cacheProvider;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                CacheProvider.ClearOverTimeHashKey();
            }
            await BootstrapAsync(stoppingToken);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public async Task BootstrapAsync(CancellationToken stoppingToken)
        {
            await Task.CompletedTask;
        }
    }
}
