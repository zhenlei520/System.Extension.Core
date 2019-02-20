using EInfrastructure.Core.Exception;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Config
{
    /// <summary>
    /// Consul配置文件
    /// </summary>
    public class ConsulConfig
    {
        #region Consul 服务器地址

        /// <summary>
        /// Consul 服务器地址
        /// </summary>
        private static string _consulClientAddress;

        /// <summary>
        /// 得到Consul 服务器地址
        /// </summary>
        /// <returns></returns>
        public static string GetConsulClientAddress()
        {
            if (string.IsNullOrEmpty(_consulClientAddress))
            {
                throw new BusinessException("未配置Consul服务器地址");
            }

            return _consulClientAddress;
        }

        /// <summary>
        /// 设置Consul 服务器地址
        /// </summary>
        /// <param name="consulClientAddress">Consul 服务器地址</param>
        public static void SetConsulClientAddress(string consulClientAddress)
        {
            _consulClientAddress = consulClientAddress;
        }

        #endregion

        #region Api服务配置

        /// <summary>
        /// Api服务配置
        /// </summary>
        private static ApiServiceConfig _apiServiceConfig;

        /// <summary>
        /// 得到Api服务配置
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public static ApiServiceConfig GetApiServiceConfig()
        {
            if (_apiServiceConfig == null)
            {
                throw new BusinessException("未配置api服务配置");
            }

            return _apiServiceConfig;
        }

        /// <summary>
        /// 设置Api服务配置
        /// </summary>
        public static void SetApiServiceConfig(ApiServiceConfig apiServiceConfig)
        {
            _apiServiceConfig = apiServiceConfig;
        }

        /// <summary>
        /// Api健康检查配置
        /// </summary>
        private static ApiServiceHealthyConfig _apiServiceHealthyConfig;

        /// <summary>
        /// 得到Api健康检查配置
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public static ApiServiceHealthyConfig GetApiServiceHealthyConfig()
        {
            if (_apiServiceConfig == null)
            {
                throw new BusinessException("未配置Api健康检查配置");
            }

            return _apiServiceHealthyConfig;
        }
        
        /// <summary>
        /// 设置Api健康检查配置
        /// </summary>
        /// <param name="apiServiceHealthyConfig"></param>
        public static void SetApiServiceHealthyConfig(ApiServiceHealthyConfig apiServiceHealthyConfig)
        {
            _apiServiceHealthyConfig = apiServiceHealthyConfig;
        }

        #endregion
    }
}