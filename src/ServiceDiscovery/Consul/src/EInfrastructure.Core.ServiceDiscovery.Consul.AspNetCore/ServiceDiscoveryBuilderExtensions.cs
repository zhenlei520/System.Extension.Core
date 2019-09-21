// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using Consul;
using EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Config;
using EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Validator;
using EInfrastructure.Core.Validation.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

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
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IApplicationLifetime lifetime)
        {
            var consulConfig = app.ApplicationServices.GetService<ConsulConfig>();
            new ApiServiceConfigValidator().Validate(consulConfig.ApiServiceConfig).Check();
            new ApiServiceHealthyConfigValidator().Validate(consulConfig.ApiServiceHealthyConfig).Check();

            var consulClient =
                new ConsulClient(x =>
                    x.Address = new Uri(consulConfig.ConsulClientAddress)); //请求注册的 Consul 地址

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

            WriteOptions writeOptions = null;
            if (!string.IsNullOrEmpty(consulConfig.ApiServiceConfig.Datacenter) ||
                !string.IsNullOrEmpty(consulConfig.ApiServiceConfig.Token))
            {
                writeOptions = new WriteOptions()
                {
                    Datacenter = consulConfig.ApiServiceConfig.Datacenter,
                    Token = consulConfig.ApiServiceConfig.Token
                };
            }

            consulClient.Agent.ServiceRegister(registration, writeOptions)
                .Wait(); //服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）

            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID, writeOptions).Wait(); //服务停止时取消注册
            });

            return app;
        }

        #endregion
    }
}