// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.QiNiu.Storage.Test.Base;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EInfrastructure.Core.QiNiu.Storage.Test
{
    /// <summary>
    ///
    /// </summary>
    public class QiNiuConfigUnitTest : BaseUnitTest
    {
        public QiNiuConfigUnitTest() : base()
        {
        }

        [Fact]
        public void GetBucketList()
        {
            var bucketProvider = base.provider.GetService<IBucketProvider>();
            bucketProvider.GetBucketList(new List<KeyValuePair<string, string>>());
        }
    }
}
