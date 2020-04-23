// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Config
{
    /// <summary>
    /// Consul配置文件
    /// </summary>
    public class ConsulConfig
    {
        /// <summary>
        /// Consul 服务器地址
        /// </summary>
        public string ConsulClientAddress { get; set; }

        /// <summary>
        /// Api服务配置
        /// </summary>
        public ApiServiceConfig ApiServiceConfig { get; set; }

        /// <summary>
        /// Api健康检查配置
        /// </summary>
        public ApiServiceHealthyConfig ApiServiceHealthyConfig { get; set; }
    }
}
