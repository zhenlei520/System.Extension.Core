using System.Collections.Generic;
using EInfrastructure.Core.Validation;

namespace EInfrastructure.Core.Redis.Config
{
    /// <summary>
    /// Redis配置
    /// </summary>
    public class RedisConfig : IFluentlValidatorEntity
    {
        /// <summary>
        ///
        /// </summary>
        public RedisConfig()
        {
            Timer = 500;
        }

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
        /// 缓存前缀
        /// </summary>
        public string Prefix { get; set; }

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

        /// <summary>
        /// 定时清理Hash的时间 默认为500ms
        /// </summary>
        public int Timer { get; set; }
    }
}
