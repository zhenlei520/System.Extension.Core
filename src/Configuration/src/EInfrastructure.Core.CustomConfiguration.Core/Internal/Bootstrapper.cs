// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using EInfrastructure.Core.CustomConfiguration.Core.Logging;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    /// <summary>
    /// Default implement of
    /// </summary>
    internal class Bootstrapper
    {
        private static readonly Func<Action<LogLevel, string, Exception>> Logger = () =>
            LogManager.CreateLogger(typeof(Bootstrapper));

        private readonly CustomConfigurationOptions _customConfigurationOptions;
        private readonly ICustomConfigurationDataProvider _configurationDataProvider;

        public Bootstrapper(CustomConfigurationOptions customConfigurationOptions,
            ICustomConfigurationDataProvider customConfigurationDataProvider)
        {
            this._customConfigurationOptions = customConfigurationOptions;
            this._configurationDataProvider = customConfigurationDataProvider;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public void Execute()
        {
            // Task.Run(() =>
            // {
            //     while (true)
            //     {
            //         try
            //         {
            //             var data = _configurationDataProvider.GetAllData();
            //             
            //         }
            //         catch (Exception ex)
            //         {
            //             Logger().Error(ex);
            //         }
            //         finally
            //         {
            //             Thread.Sleep(_customConfigurationOptions.Duration);
            //         }
            //     }
            // });
        }
    }
}