// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 复制文件到新的空间
    /// </summary>
    public class CopyFileParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="sourceKey">源文件key</param>
        /// <param name="optKey">目标文件key</param>
        /// <param name="optBucket">目标空间（如果为空，则认为与源空间一致）</param>
        /// <param name="isForce">是否覆盖</param>
        /// <param name="persistentOps">策略（分片为4MB复制时，采用大文件复制）</param>
        public CopyFileParam(string sourceKey, string optKey, string optBucket, bool isForce = true,
            BasePersistentOps persistentOps = null)
        {
            OptBucket = optBucket;
            SourceKey = sourceKey;
            OptKey = optKey;
            IsForce = isForce;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 目标空间
        /// </summary>
        public string OptBucket { get; }

        /// <summary>
        /// 源文件key
        /// </summary>
        public string SourceKey { get; }

        /// <summary>
        /// 目标文件key
        /// </summary>
        public string OptKey { get; }

        /// <summary>
        /// 是否覆盖
        /// </summary>
        public bool IsForce { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }
}
