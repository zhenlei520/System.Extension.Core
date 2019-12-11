// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using Consul;
using EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Config;
using EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Validator;
using EInfrastructure.Core.Validation.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore
{
    /// <summary>
    /// 服务发现扩展
    /// </summary>
    public static class ServiceDiscoveryBuilderExtensions
    {
        #region 服务注册

        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="app"></param>
        /// <param name="cancellationToken">停止</param>
        /// <returns></returns>
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, CancellationToken cancellationToken)
        {
            var consulConfigs = app.ApplicationServices.GetService<List<ConsulConfig>>();
            if (consulConfigs == null || consulConfigs.Count == 0)
            {
                consulConfigs = new List<ConsulConfig>()
                {
                    app.ApplicationServices.GetService<ConsulConfig>()
                };
            }

            consulConfigs.ForEach(consulConfig =>
            {
                consulConfig.UseConsul(cancellationToken);
            });
            return app;
        }

        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="consulConfig">配置信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static void UseConsul(this ConsulConfig consulConfig, CancellationToken cancellationToken)
        {
            new ApiServiceConfigValidator().Validate(consulConfig.ApiServiceConfig).Check();
            new ApiServiceHealthyConfigValidator().Validate(consulConfig.ApiServiceHealthyConfig).Check();

            var consulClient =
                new ConsulClient(x =>
                {
                    x.Address = new Uri(consulConfig.ConsulClientAddress);
                    if (!string.IsNullOrEmpty(consulConfig.ApiServiceConfig.Token))
                    {
                        x.Token = consulConfig.ApiServiceConfig.Token;
                    }

                    if (!string.IsNullOrEmpty(consulConfig.ApiServiceConfig.Datacenter))
                    {
                        x.Datacenter = consulConfig.ApiServiceConfig.Datacenter;
                    }
                }); //请求注册的 Consul 地址

            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter =
                    TimeSpan.FromSeconds(consulConfig.ApiServiceHealthyConfig.RegisterTime), //服务启动多久后注册
                Interval = TimeSpan.FromSeconds(consulConfig.ApiServiceHealthyConfig
                    .CheckIntervalTime), //健康检查时间间隔，或者称为心跳间隔
                HTTP = consulConfig.ApiServiceHealthyConfig.CheckApi, //健康检查地址
                Timeout = TimeSpan.FromSeconds(consulConfig.ApiServiceHealthyConfig.TimeOutTime)
            };

            // Register service with consul
            var registration = new AgentServiceRegistration()

            {
                Checks = new[] {httpCheck},
                ID = string.IsNullOrEmpty(consulConfig.ApiServiceConfig.Id)
                    ? $"{consulConfig.ApiServiceConfig.Name}:{consulConfig.ApiServiceConfig.Port}"
                    : consulConfig.ApiServiceConfig.Id,
                Name = consulConfig.ApiServiceConfig.Name,
                Address = consulConfig.ApiServiceConfig.Ip,
                Port = consulConfig.ApiServiceConfig.Port,
                Tags = consulConfig.ApiServiceConfig.Tags == null || consulConfig.ApiServiceConfig.Tags.Length == 0
                    ? new[] {$"urlprefix-/{consulConfig.ApiServiceConfig.Name}"}
                    : consulConfig.ApiServiceConfig.Tags //添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };
            consulClient.Agent.ServiceRegister(registration)
                .Wait(); //服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）

            cancellationToken.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID, cancellationToken).Wait(cancellationToken); //服务停止时取消注册
            });
        }

        #endregion
    }
}
