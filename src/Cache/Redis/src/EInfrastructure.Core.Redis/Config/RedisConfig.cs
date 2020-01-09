using System.Collections.Generic;
using EInfrastructure.Core.AutomationConfiguration.Interface;
using EInfrastructure.Core.Validation;

namespace EInfrastructure.Core.Redis.Config
{
    /// <summary>
    /// Redis配置
    /// </summary>
    public class RedisConfig : ISingletonConfigModel, IFluentlValidatorEntity
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 储存的数据库索引
        /// </summary>
        public int DataBase { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Redis连接池连接数
        /// </summary>
        public int PoolSize { get; set; }

        /// <summary>
        /// Redis默认 Hashkey  过期缓存key前缀
        /// </summary>
        public string OverTimeCacheKeyPre { get; set; }

        /// <summary>
        /// Hash缓存key 范围
        /// </summary>
        public List<string> OverTimeCacheKeys { get; set; }
    }
}
