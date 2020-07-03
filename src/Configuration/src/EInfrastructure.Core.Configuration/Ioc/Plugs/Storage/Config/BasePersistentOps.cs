// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config
{
    /// <summary>
    /// 基本策略
    /// </summary>
    public class BasePersistentOps
    {
        /// <summary>
        ///
        /// </summary>
        public BasePersistentOps()
        {
            Zone = null;
            Bucket = "";
            Host = "";
            IsUseHttps = null;
            UseCdnDomains = null;
            MaxRetryTimes = null;
            ChunkUnit = null;
        }

        /// <summary>
        /// 空间区域
        /// </summary>
        /// <returns></returns>
        public int? Zone { get; set; }

        /// <summary>
        /// 空间名称
        /// </summary>
        public string Bucket { get; set; }

        /// <summary>
        /// 空间域
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// [可选]是否使用Https域名
        /// </summary>
        public virtual bool? IsUseHttps { get; set; }

        /// <summary>
        /// [可选]是否用Cdn域
        /// </summary>
        public virtual bool? UseCdnDomains { get; set; }

        /// <summary>
        /// [可选]分片上传默认最大值
        /// 阿里云大文件（大于1G）操作时，请将此属性设置为U4096K，以免无法操作
        /// </summary>
        public virtual ChunkUnit ChunkUnit { get; set; }

        /// <summary>
        /// 最大重试次数
        /// </summary>
        public virtual int? MaxRetryTimes { set; get; }

        /// <summary>
        /// 复制一份新的策略
        /// </summary>
        /// <returns></returns>
        public BasePersistentOps Clone()
        {
            return new BasePersistentOps()
            {
                Bucket = Bucket,
                ChunkUnit = ChunkUnit,
                Host = Host,
                IsUseHttps = IsUseHttps,
                MaxRetryTimes = MaxRetryTimes,
                UseCdnDomains = UseCdnDomains,
                Zone = Zone
            };
        }
    }
}
