// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 更改文件mime
    /// </summary>
    public class ChangeMimeParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="mimeType">文件mimeType</param>
        /// <param name="persistentOps">策略</param>
        public ChangeMimeParam(string key, string mimeType, BasePersistentOps persistentOps = null)
        {
            Key = key;
            MimeType = mimeType;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 文件mimeType
        /// </summary>
        public string MimeType { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }
}
