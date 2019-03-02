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
    /// 加载Consul注册服务
    /// </summary>
    public static class Startup
    {
        #region 添加Consul配置文件

        /// <summary>
        /// 添加Consul配置文件
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="consulClientAddress">Consul 服务器地址</param>
        /// <param name="apiServiceConfig">api服务配置</param>
        /// <param name="apiServiceHealthyConfig">api服务健康配置</param>
        /// <returns></returns>
        public static IServiceCollection AddConsulConfig(this IServiceCollection serviceCollection,
            string consulClientAddress,
            ApiServiceConfig apiServiceConfig, ApiServiceHealthyConfig apiServiceHealthyConfig)
        {
            ConsulConfig.SetConsulClientAddress(consulClientAddress);
            ConsulConfig.SetApiServiceConfig(apiServiceConfig);
            ConsulConfig.SetApiServiceHealthyConfig(apiServiceHealthyConfig);
            Validation.Startup.AddModelValidation(serviceCollection); //默认开启级联验证
            return serviceCollection;
        }

        #endregion

        #region 服务注册

        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="app"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IApplicationLifetime lifetime)
        {
            new ApiServiceConfigValidator().Validate(ConsulConfig.GetApiServiceConfig()).Check();
            new ApiServiceHealthyConfigValidator().Validate(ConsulConfig.GetApiServiceHealthyConfig()).Check();

            var consulClient =
                new ConsulClient(x =>
                    x.Address = new Uri(ConsulConfig.GetConsulClientAddress())); //请求注册的 Consul 地址

            var apiServiceHealthyConfig = ConsulConfig.GetApiServiceHealthyConfig();
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(apiServiceHealthyConfig.RegisterTime), //服务启动多久后注册
                Interval = TimeSpan.FromSeconds(apiServiceHealthyConfig.CheckIntervalTime), //健康检查时间间隔，或者称为心跳间隔
                HTTP = apiServiceHealthyConfig.CheckApi, //健康检查地址
                Timeout = TimeSpan.FromSeconds(apiServiceHealthyConfig.TimeOutTime)
            };

            // Register service with consul
            var apiServiceConfig = ConsulConfig.GetApiServiceConfig();
            var registration = new AgentServiceRegistration()

            {
                Checks = new[] {httpCheck},
                ID = string.IsNullOrEmpty(apiServiceConfig.Id)
                    ? $"{apiServiceConfig.Name}:{apiServiceConfig.Port}"
                    : apiServiceConfig.Id,
                Name = apiServiceConfig.Name,
                Address = apiServiceConfig.Ip,
                Port = apiServiceConfig.Port,
                Tags = apiServiceConfig.Tags == null || apiServiceConfig.Tags.Length == 0
                    ? new[] {$"urlprefix-/{apiServiceConfig.Name}"}
                    : apiServiceConfig.Tags //添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            consulClient.Agent.ServiceRegister(registration).Wait(); //服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）

            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait(); //服务停止时取消注册
            });

            return app;
        }

        #endregion
    }
}