// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 复制文件到新的空间
    /// </summary>
    public class CopyFileRangeParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="copyFiles">复制文件集合</param>
        /// <param name="persistentOps">策略，（源空间也需要在此配置）</param>
        public CopyFileRangeParam(List<CopyFileParam> copyFiles, BasePersistentOps persistentOps)
        {
            CopyFiles = copyFiles ?? new List<CopyFileParam>();
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 复制文件集合
        /// </summary>
        public List<CopyFileParam> CopyFiles { get; private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }

        /// <summary>
        /// 复制文件到新的空间
        /// </summary>
        public class CopyFileParam
        {
            /// <summary>
            ///
            /// </summary>
            /// <param name="optBucket">目标空间</param>
            /// <param name="sourceKey">源文件key</param>
            /// <param name="optKey">目标文件key</param>
            /// <param name="isForce">是否覆盖</param>
            public CopyFileParam(string optBucket, string sourceKey, string optKey, bool isForce)
            {
                FileId = Guid.NewGuid().ToString("N");
                OptBucket = optBucket;
                SourceKey = sourceKey;
                OptKey = optKey;
                IsForce = isForce;
            }

            /// <summary>
            /// 文件标识
            /// </summary>
            public string FileId { get; }

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
        }
    }
}
