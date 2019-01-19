using EInfrastructure.Core.AutoConfig.Interface;

namespace EInfrastructure.Core.AutoConfig
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class HostingEnvironmentConfigs : IScopedConfigModel
    {
        /// <summary>
        /// 文件地址
        /// </summary>
        public string ContentRootPath { get; set; }
    }
}