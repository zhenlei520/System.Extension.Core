using EInfrastructure.Core.Aliyun.Storage.Enum;
using EInfrastructure.Core.Aliyun.Storage.Test.Base;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
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
                Zone = (int)ZoneEnum.BeiJing
            }));
        }
    }
}
