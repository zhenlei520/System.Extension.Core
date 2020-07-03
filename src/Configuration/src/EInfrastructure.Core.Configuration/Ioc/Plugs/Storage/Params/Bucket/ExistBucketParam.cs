// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket
{
    /// <summary>
    /// 判断空间是否存在
    /// </summary>
    public class ExistBucketParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="persistentOps">等待判断的空间是此对象的bucket属性，如果不赋值，则判断默认空间</param>
        public ExistBucketParam(BasePersistentOps persistentOps)
        {
            PersistentOps = persistentOps;
        }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
