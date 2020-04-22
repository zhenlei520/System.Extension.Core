// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params
{
    /// <summary>
    /// 移动或重命名文件
    /// </summary>
    public class MoveFileParam
    {
        /// <summary>
        /// 移动或重命名文件
        /// </summary>
        /// <param name="sourceBucket">源空间</param>
        /// <param name="optBucket">目标空间</param>
        /// <param name="sourceKey">源文件key</param>
        /// <param name="optKey">目标文件key</param>
        /// <param name="isForce">是否覆盖</param>
        public MoveFileParam(string sourceBucket, string optBucket, string sourceKey, string optKey, bool isForce)
        {
            FileId = Guid.NewGuid().ToString("N");
            SourceBucket = sourceBucket;
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
        /// 源空间
        /// </summary>
        public string SourceBucket { get; }

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
