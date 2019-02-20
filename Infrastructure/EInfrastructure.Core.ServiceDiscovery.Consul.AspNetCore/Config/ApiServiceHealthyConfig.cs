using EInfrastructure.Core.AutoConfig.Interface;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Config
{
    /// <summary>
    /// Api服务健康检查
    /// </summary>
    public class ApiServiceHealthyConfig : IScopedConfigModel
    {
        /// <summary>
        /// 检查Api接口地址(HttpStatus为200代表正常)
        /// </summary>
        public string CheckApi { get; set; }

        /// <summary>
        /// 服务启动多久后注册，默认5s
        /// </summary>
        public int RegisterTime { get; set; } = 5;

        /// <summary>
        /// 健康检查时间间隔，或者称为心跳间隔，默认10s
        /// </summary>
        public int CheckIntervalTime { get; set; } = 10;

        /// <summary>
        /// 超时时间，默认5s
        /// </summary>
        public int TimeOutTime { get; set; } = 5;
    }
}