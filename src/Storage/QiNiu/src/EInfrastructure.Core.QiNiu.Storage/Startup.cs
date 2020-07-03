// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Enum;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Validation.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 加载七牛云存储服务
    /// </summary>
    public static class Startup
    {
        #region 加载七牛云存储

        /// <summary>
        /// 加载七牛云存储
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddQiNiuStorage(this IServiceCollection services)
        {
            var service = services.First(x => x.ServiceType == typeof(IConfiguration));
            var configuration = (IConfiguration) service.ImplementationInstance;
            if (configuration == null)
            {
                throw new BusinessException("获取IConfiguration失败");
            }
            return AddQiNiuStorage(services, configuration);
        }

        #endregion

        #region 加载七牛云存储

        /// <summary>
        /// 加载七牛云存储
        /// </summary>
        /// <param name="services"></param>
        /// <param name="func">委托</param>
        public static IServiceCollection AddQiNiuStorage(this IServiceCollection services,
            Func<QiNiuStorageConfig> func)
        {
            StartUp.Run();
            var qiNiuConfig = func.Invoke();
            ValidationCommon.Check(qiNiuConfig, "七牛云存储配置异常", HttpStatus.Err.Name);
            services.AddSingleton(qiNiuConfig);
            return services;
        }

        #endregion

        #region 加载七牛云存储

        /// <summary>
        /// 加载七牛云存储
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddQiNiuStorage(this IServiceCollection services,
            IConfiguration configuration)
        {
            StartUp.Run();
            var section = configuration.GetSection(nameof(QiNiuStorageConfig));
            if (section == null)
            {
                throw new BusinessException<string>("七牛云存储配置异常", HttpStatus.Err.Name);
            }

            QiNiuStorageConfig qiNiuStorageConfig = new QiNiuStorageConfig(section.GetValue<string>("AccessKey"),
                section.GetValue<string>("SecretKey"), section.GetValue<ZoneEnum>("DefaultZones"),
                section.GetValue<string>("DefaultHost"), section.GetValue<string>("DefaultBucket"))
            {
                IsUseHttps = section.GetValue<bool>("IsUseHttps"),
                UseCdnDomains = section.GetValue<bool>("UseCdnDomains"),
                IsAllowOverlap = section.GetValue<bool>("IsAllowOverlap"),
                PersistentNotifyUrl = section.GetValue<string>("PersistentNotifyUrl"),
                PersistentPipeline = section.GetValue<string>("PersistentPipeline"),
                ChunkUnit =
                    ChunkUnit
                        .FromValue<ChunkUnit>((section
                            .GetValue<string>("ChunkUnit").ConvertToInt(ChunkUnit.U2048K.Id)))
            };
            qiNiuStorageConfig.SetCallBack(section.GetValue<string>("CallbackBodyType").ConvertToInt(CallbackBodyType.Json.Id),
                section.GetValue<string>("CallbackHost"), section.GetValue<string>("CallbackUrl"),
                section.GetValue<string>("CallbackBody"));
            configuration.GetSection(nameof(QiNiuStorageConfig)).Bind(qiNiuStorageConfig);
            qiNiuStorageConfig.SetCallBackState(!string.IsNullOrEmpty(qiNiuStorageConfig.CallbackUrl) &&
                                                !string.IsNullOrEmpty(qiNiuStorageConfig.CallbackHost));

            return AddQiNiuStorage(services,
                () => qiNiuStorageConfig);
        }

        #endregion
    }
}
