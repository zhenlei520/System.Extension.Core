// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket
{
    /// <summary>
    /// 得到空间域名
    /// </summary>
    public class GetBucketHostParam
    {
        /// <summary>
        ///
        /// </summary>
        public GetBucketHostParam()
        {
        }

        /// <summary>
        /// 得到空间域名
        /// </summary>
        /// <param name="persistentOps">策略（如果想指定与默认配置中不一样的空间，则修改此对象的bucket属性）</param>
        public GetBucketHostParam(BasePersistentOps persistentOps = null)
        {
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get;private set; }
    }
}
