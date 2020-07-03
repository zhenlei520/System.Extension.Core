// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 批量移动或重命名文件
    /// </summary>
    public class MoveFileRangeParam
    {
        /// <summary>
        /// 移动或重命名文件
        /// </summary>
        /// <param name="moveFiles"></param>
        /// <param name="persistentOps">策略</param>
        public MoveFileRangeParam(List<MoveFileParam> moveFiles,
            BasePersistentOps persistentOps = null)
        {
            MoveFiles = moveFiles ?? new List<MoveFileParam>();
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 移动文件信息
        /// </summary>
        public List<MoveFileParam> MoveFiles { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }

        /// <summary>
        /// 移动或重命名文件
        /// </summary>
        public class MoveFileParam
        {
            /// <summary>
            /// 移动或重命名文件
            /// </summary>
            /// <param name="optBucket">目标空间</param>
            /// <param name="sourceKey">源文件key</param>
            /// <param name="optKey">目标文件key</param>
            /// <param name="isForce">是否覆盖</param>
            public MoveFileParam(string optBucket, string sourceKey, string optKey, bool isForce)
            {
                OptBucket = optBucket;
                SourceKey = sourceKey;
                OptKey = optKey;
                IsForce = isForce;
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
        }
    }
}
