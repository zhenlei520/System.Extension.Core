// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket
{
    /// <summary>
    /// 设置空间镜像源
    /// </summary>
    public class SetBucketSource
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="imageSource"></param>
        /// <param name="referHost"></param>
        /// <param name="persistentOps"></param>
        public SetBucketSource(string bucketName, string imageSource, string referHost = "",
            BasePersistentOps persistentOps = null)
        {
            BucketName = bucketName;
            ImageSource = imageSource;
            ReferHost = referHost;
            PersistentOps = persistentOps??new BasePersistentOps();
        }

        /// <summary>
        /// 空间名
        /// </summary>
        public string BucketName { get; private set; }

        /// <summary>
        /// 镜像源
        /// </summary>
        public string ImageSource { get; private set; }

        /// <summary>
        /// 回源时使用的Host头部值,可以为空
        /// </summary>
        public string ReferHost { get; private set; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; private set; }
    }
}
