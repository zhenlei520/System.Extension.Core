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
        /// <param name="bucketName"></param>
        /// <param name="persistentOps"></param>
        public GetBucketHostParam(string bucketName, BasePersistentOps persistentOps = null)
        {
            BucketName = bucketName;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 空间名
        /// </summary>
        public string BucketName { get;private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get;private set; }
    }
}
