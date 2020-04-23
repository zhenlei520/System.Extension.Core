// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.QiNiu.Storage.Test.Base;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EInfrastructure.Core.QiNiu.Storage.Test
{
    /// <summary>
    /// 存储
    /// </summary>
    public class StorageUnitTest : BaseUnitTest
    {
        private readonly IStorageProvider _storageProvider;

        /// <summary>
        ///
        /// </summary>
        public StorageUnitTest() : base()
        {
            _storageProvider = base.provider.GetService<IStorageProvider>();
        }


        #region 得到私有空间的文件链接

        /// <summary>
        /// 得到私有空间的文件链接
        /// </summary>
        /// <param name="key"></param>
        [Theory]
        [InlineData("upload/seller/userlogo/image/3.jpg")]
        public void GetPrivateUrl(string key)
        {
            var url = base.provider.GetService<IStorageProvider>().GetPrivateUrl(new GetPrivateUrlParam(key));
        }

        #endregion
    }
}
