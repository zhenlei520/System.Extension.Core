using EInfrastructure.Core.AutoConfig.Interface;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Extensions.Configs
{
    /// <summary>
    /// Consul服务配置
    /// </summary>
    public class ConsulServiceConfig : IScopedConfigModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
    }
}