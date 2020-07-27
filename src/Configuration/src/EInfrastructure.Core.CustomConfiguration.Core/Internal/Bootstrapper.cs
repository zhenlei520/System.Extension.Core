// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using Microsoft.Extensions.Hosting;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    /// <summary>
    /// Default implement of
    /// </summary>
    internal class Bootstrapper : BackgroundService
    {
        private readonly CustomConfigurationOptions _configurationOptions;

        public Bootstrapper(CustomConfigurationOptions configurationOptions)
        {
            _configurationOptions = configurationOptions;
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
                    Thread.Sleep(_configurationOptions.Duration);
                }
            }, stoppingToken);

            await Task.CompletedTask;
        }
    }
}
