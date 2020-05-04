// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage.Pictures
{
    /// <summary>
    /// 图片抓取
    /// </summary>
    public class FetchFileParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="sourceFileKey">源图,必须是完整的图片路径，有访问权限（必填）</param>
        /// <param name="key">目标图的文件key（必填）</param>
        /// <param name="persistentOps">策略</param>
        public FetchFileParam(string sourceFileKey, string key, BasePersistentOps persistentOps = null)
        {
            SourceFileKey = sourceFileKey;
            Key = key;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 源图,必须是完整的图片路径，有访问权限（必填）
        /// </summary>
        public string SourceFileKey { get; }

        /// <summary>
        /// 目标图的文件key（必填）
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }
}
