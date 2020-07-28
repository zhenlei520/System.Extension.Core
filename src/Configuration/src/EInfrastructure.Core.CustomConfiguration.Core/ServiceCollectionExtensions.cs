// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.CustomConfiguration.Core.Internal;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Serialize.Xml;
using EInfrastructure.Core.Tools.Unique;
using EInfrastructure.Core.Validation.Common;
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
            new CustomConfigurationOptionsValidator().Validate(CustomConfigurationOptions).Check(HttpStatus.Err.Name);

            Invoke(serviceProvider =>
            {
                var customConfigurationDataProvider =
                    serviceProvider.GetService<ICustomConfigurationDataProvider>();

                var customConfigurationProvider = serviceProvider.GetService<ICustomConfigurationProvider>();
                customConfigurationProvider?.InitConfig();
                customConfigurationProvider?.AddFile(configurationBuilder);
                
                var bootstrapper = new Bootstrapper(CustomConfigurationOptions, customConfigurationDataProvider);
                bootstrapper.Execute();
            });
        }

        #region 得到容器

        /// <summary>
        /// 得到容器
        /// </summary>
        /// <returns></returns>
        private static IServiceProvider BuildProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(CustomConfigurationOptions);
            serviceCollection.AddSingleton<IJsonProvider, NewtonsoftJsonProvider>();
            serviceCollection.AddSingleton<IXmlProvider, XmlProvider>();
            serviceCollection.AddTransient<ICacheFileProvider, CacheFileProvider>();
            serviceCollection.AddTransient<ICustomConfigurationProvider, CustomConfigurationProvider>();
            CustomConfigurationOptions.Extensions.AddServices(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }

        #endregion

        #region private methods

        /// <summary>
        /// private methods
        /// </summary>
        /// <param name="func"></param>
        public static T Invoke<T>(Func<IServiceProvider, T> func)
        {
            using (IServiceScope serviceScope = BuildProvider().CreateScope())
            {
                return func.Invoke(serviceScope.ServiceProvider);
            }
        }

        /// <summary>
        /// private methods
        /// </summary>
        /// <param name="action"></param>
        public static void Invoke(Action<IServiceProvider> action)
        {
            using (IServiceScope serviceScope = BuildProvider().CreateScope())
            {
                action.Invoke(serviceScope.ServiceProvider);
            }
        }

        #endregion
    }
}