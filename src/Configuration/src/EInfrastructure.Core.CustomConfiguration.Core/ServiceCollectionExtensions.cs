// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.CustomConfiguration.Core.Internal;
using EInfrastructure.Core.Tools.Unique;
using EInfrastructure.Core.Validation.Common;
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
        /// 可修改自定义配置的信息
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services,
            Action<CustomConfigurationOptions> setupAction)
        {
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            CustomConfigurationOptions = new CustomConfigurationOptions();
            setupAction.Invoke(CustomConfigurationOptions);
            new CustomConfigurationOptionsValidator().Validate(CustomConfigurationOptions).Check(HttpStatus.Err.Name);
            CustomConfigurationOptions.Extensions.AddServices(services);
            return services;
        }
    }
}