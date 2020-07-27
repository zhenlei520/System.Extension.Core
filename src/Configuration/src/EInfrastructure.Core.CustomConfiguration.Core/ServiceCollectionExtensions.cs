// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.CustomConfiguration.Core.Internal;
using EInfrastructure.Core.Tools.Unique;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.CustomConfiguration.Core
{
    /// <summary>
    ///
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        internal static SnowflakeId SnowflakeId = new SnowflakeId(15, 5);

        /// <summary>
        /// 配置文件
        /// </summary>
        internal static CustomConfigurationOptions CustomConfigurationOptions;

        /// <summary>
        ///
        /// </summary>
        /// <param name="configurationBuilder"></param>
        /// <param name="setupAction"></param>
        public static void AddCustomConfiguration(this IConfigurationBuilder configurationBuilder,
            Action<CustomConfigurationOptions> setupAction)
        {
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            CustomConfigurationOptions = new CustomConfigurationOptions();
            setupAction.Invoke(CustomConfigurationOptions);

            using (IServiceScope serviceScope = BuildProvider().CreateScope())
            {
                var customConfigurationDataProvider =
                    serviceScope.ServiceProvider.GetService<ICustomConfigurationDataProvider>();
                configurationBuilder.Add(new CustomConfigurationSource(customConfigurationDataProvider));
            }
        }

        #region 得到容器

        /// <summary>
        /// 得到容器
        /// </summary>
        /// <returns></returns>
        internal static IServiceProvider BuildProvider()
        {
            var serviceCollection = new ServiceCollection();
            CustomConfigurationOptions.Extensions.AddServices(serviceCollection);
            serviceCollection.AddSingleton(CustomConfigurationOptions);
            // ServiceCollection.AddHostedService<Bootstrapper>();
            return serviceCollection.BuildServiceProvider();
        }

        #endregion
    }
}
