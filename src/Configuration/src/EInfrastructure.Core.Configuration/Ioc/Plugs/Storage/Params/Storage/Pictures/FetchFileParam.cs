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
        /// <param name="sourceFileKey"></param>
        /// <param name="key"></param>
        /// <param name="persistentOps"></param>
        public FetchFileParam(string sourceFileKey, string key, BasePersistentOps persistentOps = null)
        {
            SourceFileKey = sourceFileKey;
            Key = key;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 源图（必填）
        /// </summary>
        public string SourceFileKey { get; private set; }

        /// <summary>
        /// 目标图（必填）
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
