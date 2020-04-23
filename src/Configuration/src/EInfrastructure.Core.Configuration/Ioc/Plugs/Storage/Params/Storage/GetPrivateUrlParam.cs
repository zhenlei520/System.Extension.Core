// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 得到私有空间的地址
    /// </summary>
    public class GetPrivateUrlParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="expire">过期时间 单位：s</param>
        /// <param name="persistentOps"></param>
        public GetPrivateUrlParam(string key, int expire = 3600, BasePersistentOps persistentOps = null)
        {
            Key = key;
            Expire = expire;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 过期时间 单位：s
        /// </summary>
        public int Expire { get; private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
