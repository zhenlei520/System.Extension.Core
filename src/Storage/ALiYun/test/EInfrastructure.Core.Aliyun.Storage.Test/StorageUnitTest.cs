// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.IO;
using System.Linq;
using EInfrastructure.Core.Aliyun.Storage.Test.Base;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
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
            _storageProvider = provider.GetService<IStorageProvider>();
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
        [InlineData("1.jpg", "D:\\temp\\1.jpg", "einfrastructuretest", false)]
        [InlineData("2.jpg", "D:\\temp\\2.jpg", "einfrastructuretest", true)]
        public void UploadStream(string key, string sourceKey, string bucket, bool isResume)
        {
            var stream = File.OpenRead(sourceKey);
            var ret = _storageProvider.UploadStream(new UploadByStreamParam(key, stream, new UploadPersistentOps()
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
        [InlineData("2.jpg", "einfrastructuretest")]
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
        [InlineData("2.jpg", "einfrastructuretest")]
        public void GetPermiss(string key, string bucket)
        {
            var ret = _storageProvider.GetPermiss(new GetFilePermissParam(key, new BasePersistentOps()
            {
                Bucket = bucket
            }));
            Check.True(ret.State, "校验不一致");
        }

        #endregion

        #region 获取文件信息

        /// <summary>
        /// 获取文件信息
        /// </summary>
        [Theory]
        [InlineData("2.jpg", "einfrastructuretest")]
        public void Get(string key, string bucket)
        {
            var ret = _storageProvider.Get(new GetFileParam(key, new BasePersistentOps()
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
        /// <param name="bucket">筛选</param>
        /// <returns></returns>
        [Theory]
        [InlineData("einfrastructuretest")]
        public void ListFiles(string bucket)
        {
            var ret = _storageProvider.ListFiles(new ListFileFilter("", "", true, "", 100, new BasePersistentOps()
            {
                Bucket = bucket
            }));
            Check.True(ret.State, "校验不一致");
        }

        #endregion

        #region 获取文件的访问地址

        /// <summary>
        /// 获取文件的访问地址（不区分权限）
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="bucket">空间名</param>
        [Theory]
        [InlineData("2.jpg", "einfrastructuretest")]
        public void GetVisitUrl(string key, string bucket)
        {
            var ret = _storageProvider.GetVisitUrl(new GetVisitUrlParam(key, null, new BasePersistentOps()
            {
                Bucket = bucket
            }));
            Check.True(ret.State, "校验不一致");
        }

        #endregion

        #region 生存时间

        #region 设置生存时间

        /// <summary>
        /// 设置生存时间
        /// </summary>
        /// <param name="bucuket"></param>
        /// <param name="key">文件key</param>
        [Theory]
        [InlineData("einfrastructuretest", "2.jpg")]
        public void SetExpire(string bucuket, string key)
        {
            var ret = this._storageProvider.SetExpire(new SetExpireParam(key, 7, new BasePersistentOps()
            {
                Bucket = bucuket,
            }));
        }

        #endregion

        #region 批量设置文件key的过期时间

        /// <summary>
        /// 批量设置文件key的过期时间
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="bucuket"></param>
        [Theory]
        [InlineData("2.jpg,1.jpg", "einfrastructuretest")]
        public void SetExpireRange(string keys, string bucuket)
        {
            var ret = this._storageProvider.SetExpireRange(new SetExpireRangeParam(keys.ConvertStrToList<string>(), 7,
                new BasePersistentOps()
                {
                    Bucket = bucuket
                }));
        }

        #endregion

        #endregion

        #region 修改文件MimeType

        #region 修改文件MimeType

        /// <summary>
        /// 修改文件MimeType
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <param name="mimeType"></param>
        [Theory]
        [InlineData("einfrastructuretest", "2.jpg", "image/png")]
        public void ChangeMime(string bucket, string key, string mimeType)
        {
            var ret = this._storageProvider.ChangeMime(new ChangeMimeParam(key, mimeType, new BasePersistentOps()
            {
                Bucket = bucket
            }));
        }

        #endregion

        #region 批量更改文件mime

        /// <summary>
        /// 批量更改文件mime
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <param name="mimeType"></param>
        [Theory]
        [InlineData("einfrastructuretest", "1.jpg,2.jpg", "image/png")]
        public void ChangeMimeRange(string bucket, string key, string mimeType)
        {
            var ret = this._storageProvider.ChangeMimeRange(new ChangeMimeRangeParam(key.ConvertStrToList<string>(),
                mimeType,
                new BasePersistentOps()
                {
                    Bucket = bucket
                }));
        }

        #endregion

        #endregion

        #region 更改文件存储类型

        /// <summary>
        /// 更改文件存储类型
        /// </summary>
        /// <param name="bucket">空间名</param>
        /// <param name="key">文件key</param>
        /// <param name="storageClass">存储类型</param>
        [Theory]
        [InlineData("einfrastructuretest", "1.jpg", 1)]
        public void ChangeType(string bucket, string key, int storageClass)
        {
            var ret = this._storageProvider.ChangeType(new ChangeTypeParam(key,
                StorageClass.GetAll<StorageClass>().FirstOrDefault(x => x.Id == storageClass), new BasePersistentOps()
                {
                    Bucket = bucket
                }));
        }

        #endregion

        #region 批量更改文件类型

        /// <summary>
        /// 批量更改文件类型
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="keys"></param>
        /// <param name="storageClass"></param>
        [Theory]
        [InlineData("einfrastructuretest", "1.jpg,2.jpg", 1)]
        public void ChangeTypeRange(string bucket, string keys, int storageClass)
        {
            var ret = this._storageProvider.ChangeTypeRange(new ChangeTypeRangeParam(keys.ConvertStrToList<string>(),
                StorageClass.GetAll<StorageClass>().FirstOrDefault(x => x.Id == storageClass), new BasePersistentOps()
                {
                    Bucket = bucket
                }));
        }

        #endregion

        #region 下载文件到本地

        /// <summary>
        /// 下载文件到本地
        /// </summary>
        [Theory]
        [InlineData("einfrastructuretest", "1.jpg", "d:/3.jpg")]
        public void Download(string bucket, string sourceFileKey, string locakKey)
        {
            var ret = this._storageProvider.GetVisitUrl(new GetVisitUrlParam(sourceFileKey, null,
                new BasePersistentOps()
                {
                    Bucket = bucket
                }));
            if (ret.State)
            {
                var downloadRes = this._storageProvider.Download(new FileDownloadParam(ret.Url, locakKey,
                    new BasePersistentOps()
                    {
                        Bucket = bucket
                    }));
            }
        }

        #endregion

        #region 下载文件到本地

        /// <summary>
        /// 下载文件流
        /// </summary>
        [Theory]
        [InlineData("einfrastructuretest", "1.jpg")]
        public void DownloadStream(string bucket, string sourceFileKey)
        {
            var ret = this._storageProvider.GetVisitUrl(new GetVisitUrlParam(sourceFileKey, null,
                new BasePersistentOps()
                {
                    Bucket = bucket
                }));
            if (ret.State)
            {
                var downloadStreamRes = this._storageProvider.DownloadStream(new FileDownloadStreamParam(ret.Url,
                    new BasePersistentOps()
                    {
                        Bucket = bucket
                    }));
            }
        }

        #endregion

        #region 抓取文件

        /// <summary>
        /// 抓取文件
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="sourceFileKey"></param>
        /// <param name="key"></param>
        [Theory]
        [InlineData("einfrastructuretest", "2.jpg", "4.jpg")]
        public void FetchFile(string bucket, string sourceFileKey, string key)
        {
            var ret = this._storageProvider.GetVisitUrl(new GetVisitUrlParam(sourceFileKey, null,
                new BasePersistentOps()
                {
                    Bucket = bucket
                }));
            if (ret.State)
            {
                var fetchFileRes = this._storageProvider.FetchFile(new FetchFileParam(ret.Url, key,
                    new BasePersistentOps()
                    {
                        Bucket = bucket
                    }));
                Check.True(fetchFileRes.State, "抓取失败");
            }
            else
            {
                throw new BusinessException($"抓取失败，文件信息异常，{ret.Msg}");
            }
        }

        #endregion

        #region 复制文件

        /// <summary>
        ///
        /// 对于小于1G的文件（不支持跨地域拷贝。例如，不支持将杭州存储空间里的文件拷贝到青岛），另外需将分片设置为4M，其他分类不支持
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <param name="sourceBucket"></param>
        /// <param name="optKey"></param>
        /// <param name="optBucket"></param>
        [Theory]
        [InlineData("1.jpg", "einfrastructuretest", "3.jpg", "einfrastructuretest2")]
        public void CopyFile(string sourceKey, string sourceBucket, string optKey, string optBucket)
        {
            var ret = this._storageProvider.CopyTo(new CopyFileParam(sourceKey, optKey, optBucket, false,
                new BasePersistentOps()
                {
                    Bucket = sourceBucket,
                    ChunkUnit = ChunkUnit.U4096K //如果文件大于1G，则需要设置此属性为U4096K
                }));
        }

        #endregion

        #region 移动文件

        [Theory]
        [InlineData("4", "einfrastructuretest2", "4.jpg", "einfrastructuretest2")]
        public void Move(string sourceKey, string sourceBucket, string optKey, string optBucket)
        {
            var ret = this._storageProvider.Move(new MoveFileParam(sourceKey, optBucket, optKey, false,
                new BasePersistentOps()
                {
                    Bucket = sourceBucket
                }));
        }

        #endregion

        #region 清空仓库

        /// <summary>
        /// 清空仓库
        /// </summary>
        /// <param name="bucket">筛选</param>
        /// <returns></returns>
        [Theory]
        [InlineData("einfrastructuretest2")]
        public void Clear(string bucket)
        {
            var ret = _storageProvider.ListFiles(new ListFileFilter("", "", true, "", 100, new BasePersistentOps()
            {
                Bucket = bucket
            }));
            if (ret.State)
            {
                var clearRet = this._storageProvider.RemoveRange(new RemoveRangeParam(ret.Items.Select(x => x.Key).ToList(),
                    new BasePersistentOps()
                    {
                        Bucket = bucket
                    }));
            }

            Check.True(ret.State, "校验不一致");
        }

        #endregion
    }
}
