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
        /// <param name="imageSource">镜像源</param>
        /// <param name="referHost">回源时使用的Host头部值,可以为空</param>
        /// <param name="persistentOps">策略（如果需要修改其他空间的镜像源，可以修改此对象的Bucket）</param>
        public SetBucketSource(string imageSource, string referHost = "",
            BasePersistentOps persistentOps = null)
        {
            ImageSource = imageSource;
            ReferHost = referHost;
            PersistentOps = persistentOps??new BasePersistentOps();
        }

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
