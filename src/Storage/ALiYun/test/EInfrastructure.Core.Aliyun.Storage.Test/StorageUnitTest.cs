// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.IO;
using EInfrastructure.Core.Aliyun.Storage.Test.Base;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config.Pictures;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.Tools;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EInfrastructure.Core.Aliyun.Storage.Test
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

        #region 根据文件流上传

        /// <summary>
        /// 根据文件流上传
        /// </summary>
        /// <param name="sourceKey">源文件信息</param>
        /// <param name="key">新文件key</param>
        /// <param name="bucket">空间名</param>
        /// <param name="isResume">开启断点续传</param>
        [Theory]
        [InlineData("1.jpg", "D:\\temp\\1.jpeg", "einfrastructuretest", false)]
        [InlineData("2.jpg", "D:\\temp\\2.jpeg", "einfrastructuretest", true)]
        public void UploadStream(string key, string sourceKey, string bucket, bool isResume)
        {
            var stream = File.OpenRead(sourceKey);
            var ret = _storageProvider.UploadStream(new UploadByStreamParam(key, stream, new ImgPersistentOps()
            {
                Bucket = bucket
            }), isResume);
        }

        #endregion

        #region 是否存在

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="bucket">空间名</param>
        /// <param name="isExist">是否存在</param>
        [Theory]
        [InlineData("1.jpg", "einfrastructuretest", true)]
        public void Exist(string key, string bucket, bool isExist)
        {
            var ret = _storageProvider.Exist(new ExistParam(key, new BasePersistentOps()
            {
                Bucket = bucket
            }));
            Check.True(ret.State == isExist, "校验不一致");
        }

        #endregion

        #region 设置文件访问权限

        /// <summary>
        /// 设置文件访问权限
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="bucket">空间名</param>
        [Theory]
        [InlineData("3.jpg", "einfrastructuretest")]
        public void SetPermiss(string key, string bucket)
        {
            var ret = _storageProvider.SetPermiss(new SetPermissParam(key, Permiss.Public, new BasePersistentOps()
            {
                Bucket = bucket
            }));
            Check.True(ret.State, "校验不一致");
        }

        #endregion

        #region 获取文件访问权限

        /// <summary>
        /// 获取文件访问权限
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="bucket">空间名</param>
        [Theory]
        [InlineData("3.jpg", "einfrastructuretest")]
        public void GetPermiss(string key, string bucket)
        {
            var ret = _storageProvider.GetPermiss(new GetFilePermissParam(key, new BasePersistentOps()
            {
                Bucket = bucket
            }));
            Check.True(ret.State, "校验不一致");
        }

        #endregion

        #region 获取指定前缀的文件列表

        /// <summary>
        /// 获取指定前缀的文件列表
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <returns></returns>
        [Theory]
        [InlineData( "einfrastructuretest")]
        public void ListFiles(string bucket)
        {
            var ret = _storageProvider.ListFiles(new ListFileFilter("","","",100,new BasePersistentOps()
            {
                Bucket = bucket
            }));
            Check.True(ret.State, "校验不一致");
        }

        #endregion
    }
}
