// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.IO;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.QiNiu.Storage.Tests.Base;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EInfrastructure.Core.QiNiu.Storage.Tests
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
            _storageProvider = provider.GetService<IStorageProvider>();
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
            var url = provider.GetService<IStorageProvider>().GetVisitUrl(new GetVisitUrlParam(key));
        }

        #endregion

        #region 文件上传

        [Fact]
        public void UploadStream()
        {
            return;
            using (FileStream fileStream =
                new FileStream("D:/封面.png", FileMode.Open, FileAccess.Read))
            {
                var ret = _storageProvider.UploadStream(new UploadByStreamParam("fengmian.jpg", fileStream));
            }
        }

        #endregion
    }
}
