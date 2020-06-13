using System.Collections.Generic;
using EInfrastructure.Core.Aliyun.Storage.Enum;
using EInfrastructure.Core.Aliyun.Storage.Test.Base;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
using EInfrastructure.Core.Tools;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EInfrastructure.Core.Aliyun.Storage.Test
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

        [Fact]
        public void GetBucketList()
        {
            var bucketList = _bucketProvider.GetBucketList(new GetBucketParam("", "", -1, null));
            bucketList = _bucketProvider.GetBucketList(new GetBucketParam("", "", -1, null, new BasePersistentOps()
            {
                Zone = (int) ZoneEnum.Hongkong
            }));
        }

        [Theory]
        [InlineData("einfrastructuretest2")]
        public void CreateBucket(string bucket)
        {
            var ret = _bucketProvider.Create(new CreateBucketParam(bucket, (int) ZoneEnum.HangZhou,
                StorageClass.Standard));
        }

        [Theory]
        [InlineData("einfrastructuretest2")]
        public void DeleteBucket(string bucket)
        {
            var ret = _bucketProvider.Delete(new DeleteBucketParam(new BasePersistentOps()
            {
                Bucket = bucket,
                Zone = (int) ZoneEnum.HangZhou
            }));
        }

        [Theory]
        [InlineData("einfrastructuretest2", true)]
        public void Exist(string bucket, bool isExist)
        {
            var ret = _bucketProvider.Delete(new DeleteBucketParam(new BasePersistentOps()
            {
                Bucket = bucket,
                Zone = (int) ZoneEnum.HangZhou
            }));
            Check.True(ret.State == isExist, "存储异常");
        }

        [Theory]
        [InlineData("einfrastructuretest2", true)]
        public void SetPermiss(string bucket, bool state)
        {
            var ret = _bucketProvider.SetPermiss(new SetPermissParam(Permiss.Public, new BasePersistentOps()
            {
                Bucket = bucket
            }));
            Check.True(ret.State == state, "设置访问权限失败");
        }

        [Theory]
        [InlineData("einfrastructuretest2", 1)]
        public void GetPermiss(string bucket, int permiss)
        {
            var ret = _bucketProvider.GetPermiss(new BasePersistentOps()
            {
                Bucket = bucket
            });
            Check.True(ret.State, "获取访问权限失败");
            Check.True(ret.Permiss.Id == permiss, "访问权限异常");
        }

        /// <summary>
        /// 设置防盗链
        /// </summary>
        [Theory]
        [InlineData("einfrastructuretest2")]
        public void SetReferer(string bucket)
        {
            var ret = _bucketProvider.SetReferer(new SetRefererParam(true, new List<string>()
            {
                "http://api.deiyoudian.com"
            }, new BasePersistentOps()
            {
                Bucket = bucket
            }));
        }

        [Theory]
        [InlineData("einfrastructuretest2")]
        public void GetReferer(string bucket)
        {
            var ret = _bucketProvider.GetReferer(new GetRefererParam(new BasePersistentOps()
            {
                Bucket = bucket
            }));
        }

        [Theory]
        [InlineData("einfrastructuretest2")]
        public void ClearReferer(string bucket)
        {
            var ret = _bucketProvider.ClearReferer(new ClearRefererParam(new BasePersistentOps()
            {
                Bucket = bucket
            }));
        }

        [Theory]
        [InlineData("einfrastructuretest2")]
        public void SetTag(string bucket)
        {
            var ret = _bucketProvider.SetTag(new SetTagBucketParam(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("auth", "pingzheng")
            }, new BasePersistentOps()
            {
                Bucket = bucket
            }));
        }


        [Theory]
        [InlineData("einfrastructuretest2")]
        public void GetTags(string bucket)
        {
            var ret = _bucketProvider.GetTags(new GetTagsBucketParam(new BasePersistentOps()
            {
                Bucket = bucket
            }));
        }


        [Theory]
        [InlineData("einfrastructuretest2")]
        public void ClearTag(string bucket)
        {
            var ret = _bucketProvider.ClearTag(new ClearTagBucketParam(new BasePersistentOps()
            {
                Bucket = bucket
            }));
        }
    }
}
