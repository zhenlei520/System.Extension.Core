// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Aliyun.OSS;
using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.Tools;

namespace EInfrastructure.Core.Aliyun.Storage
{
    /// <summary>
    ///
    /// </summary>
    public class StorageProvider : IStorageProvider
    {
        private readonly ALiYunStorageConfig _aLiYunConfig;

        public StorageProvider(ALiYunStorageConfig aliyunConfig)
        {
            _aLiYunConfig = aliyunConfig;
        }

        #region 返回权重

        /// <summary>
        /// 返回权重
        /// </summary>
        /// <returns></returns>
        public int GetWeights()
        {
            return 98;
        }

        #endregion

        #region 得到唯一标识

        /// <summary>
        /// 得到唯一标识
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            MethodBase method = MethodBase.GetCurrentMethod();
            return method.ReflectedType.Namespace;
        }

        #endregion

        #region 根据文件流上传

        /// <summary>
        /// 根据文件流上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="isResume">是否允许续传</param>
        /// <returns></returns>
        public UploadResultDto UploadStream(UploadByStreamParam param, bool isResume = false)
        {
            var zone = Core.Tools.GetZone(this._aLiYunConfig, param.UploadPersistentOps.Zone);
            var client = _aLiYunConfig.GetClient(zone);
            var bucket = Core.Tools.GetBucket(this._aLiYunConfig, param.UploadPersistentOps.Bucket);
            PutObjectResult ret;
            if (isResume)
            {
                var request = new UploadObjectRequest(bucket, param.Key, param.Stream)
                {
                    PartSize = Core.Maps.GetPartSize(
                        Core.Tools.GetChunkUnit(this._aLiYunConfig, param.UploadPersistentOps.ChunkUnit)),
                };
                ret = client.ResumableUploadObject(request);
            }
            else
            {
                ret = client.PutObject(bucket, param.Key, param.Stream);
            }

            if (ret.HttpStatusCode == HttpStatusCode.OK)
            {
                return new UploadResultDto(true, null, "success");
            }

            return new UploadResultDto(false, ret, $"RequestId：{ret.RequestId}");
        }

        #endregion

        #region 根据文件字节数组上传

        /// <summary>
        /// 根据文件字节数组上传
        /// </summary>
        /// <param name="param">文件流上传配置</param>
        /// <param name="isResume">是否允许续传</param>
        /// <returns></returns>
        public UploadResultDto UploadByteArray(UploadByByteArrayParam param, bool isResume = false)
        {
            var zone = Core.Tools.GetZone(this._aLiYunConfig, param.UploadPersistentOps.Zone);
            var client = _aLiYunConfig.GetClient(zone);
            var bucket = Core.Tools.GetBucket(this._aLiYunConfig, param.UploadPersistentOps.Bucket);
            PutObjectResult ret;
            if (isResume)
            {
                var request = new UploadObjectRequest(bucket, param.Key, param.ByteArray.ConvertToStream())
                {
                    PartSize = Core.Maps.GetPartSize(
                        Core.Tools.GetChunkUnit(this._aLiYunConfig, param.UploadPersistentOps.ChunkUnit)),
                };
                ret = client.ResumableUploadObject(request);
            }
            else
            {
                ret = client.PutObject(bucket, param.Key, param.ByteArray.ConvertToStream());
            }

            if (ret.HttpStatusCode == HttpStatusCode.OK)
            {
                return new UploadResultDto(true, null, "success");
            }

            return new UploadResultDto(false, ret, $"RequestId：{ret.RequestId}");
        }

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public UploadResultDto UploadByToken(UploadByTokenParam param)
        {
            throw new System.NotImplementedException();
        }

        public string GetUploadCredentials(UploadPersistentOpsParam opsParam)
        {
            throw new System.NotImplementedException();
        }

        public string GetManageToken(GetManageTokenParam request)
        {
            throw new System.NotImplementedException();
        }

        public string GetDownloadToken(string url)
        {
            throw new System.NotImplementedException();
        }

        public OperateResultDto Exist(ExistParam request)
        {
            throw new System.NotImplementedException();
        }

        public ListFileItemResultDto ListFiles(ListFileFilter filter)
        {
            throw new System.NotImplementedException();
        }

        public FileInfoDto Get(GetFileParam request)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<FileInfoDto> GetList(GetFileRangeParam request)
        {
            throw new System.NotImplementedException();
        }

        public DeleteResultDto Remove(RemoveParam request)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DeleteResultDto> RemoveRange(RemoveRangeParam request)
        {
            throw new System.NotImplementedException();
        }

        public CopyFileResultDto CopyTo(CopyFileParam copyFileParam)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CopyFileResultDto> CopyRangeTo(CopyFileRangeParam request)
        {
            throw new System.NotImplementedException();
        }

        public MoveFileResultDto Move(MoveFileParam moveFileParam)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<MoveFileResultDto> MoveRange(MoveFileRangeParam request)
        {
            throw new System.NotImplementedException();
        }

        public string GetPublishUrl(GetPublishUrlParam request)
        {
            throw new System.NotImplementedException();
        }

        public string GetPrivateUrl(GetPrivateUrlParam request)
        {
            throw new System.NotImplementedException();
        }

        public DownloadResultDto Download(string url, string savePath)
        {
            throw new System.NotImplementedException();
        }

        public ExpireResultDto SetExpire(SetExpireParam request)
        {
            throw new System.NotImplementedException();
        }

        public List<ExpireResultDto> SetExpireRange(SetExpireRangeParam request)
        {
            throw new System.NotImplementedException();
        }

        public ChangeMimeResultDto ChangeMime(ChangeMimeParam request)
        {
            throw new System.NotImplementedException();
        }

        public List<ChangeMimeResultDto> ChangeMimeRange(ChangeMimeRangeParam request)
        {
            throw new System.NotImplementedException();
        }

        public ChangeTypeResultDto ChangeType(ChangeTypeParam request)
        {
            throw new System.NotImplementedException();
        }

        public List<ChangeTypeResultDto> ChangeTypeRange(ChangeTypeRangeParam request)
        {
            throw new System.NotImplementedException();
        }
    }
}
