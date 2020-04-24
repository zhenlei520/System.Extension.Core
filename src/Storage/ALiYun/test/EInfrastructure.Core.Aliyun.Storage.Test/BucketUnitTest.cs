using EInfrastructure.Core.Aliyun.Storage.Test.Base;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
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
            _bucketProvider = base.provider.GetService<IBucketProvider>();
        }

        [Fact]
        public void GetBucketList()
        {
            var bucketList = _bucketProvider.GetBucketList(new GetBucketParam("te","",1));
        }
    }
}
