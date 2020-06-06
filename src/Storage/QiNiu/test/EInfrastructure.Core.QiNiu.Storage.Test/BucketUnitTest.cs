// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
using EInfrastructure.Core.QiNiu.Storage.Test.Base;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EInfrastructure.Core.QiNiu.Storage.Test
{
    /// <summary>
    ///
    /// </summary>
    public class BucketUnitTest : BaseUnitTest
    {
        private readonly IBucketProvider _bucketProvider;

        public BucketUnitTest() : base()
        {
            _bucketProvider = provider.GetService<IBucketProvider>();
        }

        #region 得到空间列表

        /// <summary>
        /// 得到空间列表
        /// </summary>
        [Fact]
        public void GetBucketList()
        {
            var bucketList = _bucketProvider.GetBucketList(new GetBucketParam("te","",1));
        }

        #endregion

        #region 创建空间

        /// <summary>
        /// 创建空间
        /// </summary>
        /// <param name="bucketName">空间名</param>
        /// <param name="region">区域名：对应七牛枚举中的值</param>
        [Theory]
        [InlineData("einfrastructuretest", 1)]
        public void CreateBucket(string bucketName, int region)
        {
            var resultDto = _bucketProvider.Create(new CreateBucketParam(bucketName, region));
        }

        #endregion

        #region 设置镜像源

        /// <summary>
        ///
        /// </summary>
        /// <param name="bucketName">空间名</param>
        /// <param name="source">镜像源</param>
        [Theory]
        [InlineData("test", "http://img2.deiyou.net")]
        public void SetSource(string bucketName, string source)
        {
            var ret = _bucketProvider.SetSource(new SetBucketSource(bucketName, source));
        }

        #endregion

        #region 删除空间

        /// <summary>
        /// 删除空间
        /// </summary>
        /// <param name="bucketName">空间名</param>
        [Theory]
        [InlineData("einfrastructuretest")]
        public void DeteleBucket(string bucketName)
        {
            var resultDto = _bucketProvider.Delete(new DeleteBucketParam(new BasePersistentOps()
            {
                Bucket = bucketName
            }));
        }

        #endregion

        #region 得到空间域名

        /// <summary>
        /// 得到空间域名
        /// </summary>
        /// <param name="bucketName">空间名</param>
        [Theory]
        [InlineData("test")]
        public void GetHost(string bucketName)
        {
            var host = _bucketProvider.GetHost(new GetBucketHostParam(new BasePersistentOps()
            {
                Bucket = bucketName
            }));
        }

        #endregion

        #region 设置空间权限

        [Theory]
        [InlineData("test", 0)]
        public void SetPermiss(string bucket, int permiss)
        {
            var ret = _bucketProvider.SetPermiss(new SetPermissParam(Permiss.FromValue<Permiss>(permiss),
                new BasePersistentOps()
                {
                    Bucket = bucket,
                }));
        }

        #endregion

        #region 设置空间标签

        /// <summary>
        /// 设置空间标签
        /// </summary>
        /// <param name="bucket">空间名</param>
        [Theory]
        [InlineData("test")]
        public void SetTag(string bucket)
        {
            var ret = _bucketProvider.SetTag(new SetTagBucketParam(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("name", "test"),
            }, new BasePersistentOps()
            {
                Bucket = bucket
            }));
        }

        #endregion

        #region 得到空间标签

        /// <summary>
        /// 得到空间标签
        /// </summary>
        /// <param name="bucket">空间名</param>
        [Theory]
        [InlineData("test")]
        public void GetTags(string bucket)
        {
            var ret = _bucketProvider.GetTags(new GetTagsBucketParam(new BasePersistentOps()
            {
                Bucket = bucket
            }));
        }

        #endregion

        #region 清空空间标签

        /// <summary>
        /// 清空空间标签
        /// </summary>
        /// <param name="bucket">空间名</param>
        [Theory]
        [InlineData("test")]
        public void ClearTag(string bucket)
        {
            var ret = _bucketProvider.ClearTag(new ClearTagBucketParam(new BasePersistentOps()
            {
                Bucket = bucket
            }));
        }

        #endregion
    }
}
