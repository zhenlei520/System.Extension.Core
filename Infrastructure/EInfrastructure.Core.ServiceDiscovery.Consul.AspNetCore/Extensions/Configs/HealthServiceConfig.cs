namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Extensions.Configs
{
    /// <summary>
    /// 接口健康配置
    /// </summary>
    public class HealthServiceConfig
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
    }
}