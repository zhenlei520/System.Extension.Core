// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket
{
    /// <summary>
    /// 删除空间
    /// </summary>
    public class DeleteBucketParam
    {
        /// <summary>
        /// 删除空间
        /// </summary>
        /// <param name="persistentOps">策略（待删除的空间名也在其中设置，如果不设置，则默认当前配置空间）</param>
        public DeleteBucketParam(BasePersistentOps persistentOps = null)
        {
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
