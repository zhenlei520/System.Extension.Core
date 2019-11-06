// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Interface.IOC;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Config
{
    /// <summary>
    /// api服务配置
    /// </summary>
    public class ApiServiceConfig : ISingleInstance
    {
        /// <summary>
        /// 服务id（非必填）
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 服务名称（相同服务的名称一致）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// api服务站的ip信息
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 0;

        /// <summary>
        /// 标签信息（非必填）
        /// </summary>
        public string[] Tags { get; set; } = null;

        /// <summary>
        /// Providing a datacenter overwrites the DC provided by the Config
        /// </summary>
        public string Datacenter { get; set; }

        /// <summary>
        /// Token is used to provide a per-request ACL token which overrides the agent's default token.
        /// </summary>
        public string Token { get; set; }
    }
}