// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using Aliyun.OSS;
using Aliyun.OSS.Common;
using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Aliyun.Storage.Core;
using EInfrastructure.Core.Aliyun.Storage.Enum;
using EInfrastructure.Core.Aliyun.Storage.Validator.Storage;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Enumerations;
using EInfrastructure.Core.Validation.Common;

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
            try
            {
                var zone = Core.Tools.GetZone(this._aLiYunConfig, param.UploadPersistentOps.Zone,
                    () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, param.UploadPersistentOps.Bucket);
                PutObjectResult ret;
                if (isResume)
                {
                    var request = new UploadObjectRequest(bucket, param.Key, param.Stream)
                    {
                        PartSize = Core.Tools.GetPartSize(
                            Core.Tools.GetChunkUnit(this._aLiYunConfig, param.UploadPersistentOps.ChunkUnit,
                                () => ChunkUnit.U2048K)),
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
            catch (BusinessException<string>e)
            {
                return new UploadResultDto(false, e, e.Message);
            }
            catch (Exception e)
            {
                return new UploadResultDto(false, e,
                    Core.Tools.GetMessage(e));
            }
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
            try
            {
                var zone = Core.Tools.GetZone(this._aLiYunConfig, param.UploadPersistentOps.Zone,
                    () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, param.UploadPersistentOps.Bucket);
                PutObjectResult ret;
                if (isResume)
                {
                    var request = new UploadObjectRequest(bucket, param.Key, param.ByteArray.ConvertToStream())
                    {
                        PartSize = Core.Tools.GetPartSize(
                            Core.Tools.GetChunkUnit(this._aLiYunConfig, param.UploadPersistentOps.ChunkUnit,
                                () => ChunkUnit.U2048K)),
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
            catch (BusinessException<string>e)
            {
                return new UploadResultDto(false, e, e.Message);
            }
            catch (Exception e)
            {
                return new UploadResultDto(false, e,
                    Core.Tools.GetMessage(e));
            }
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

        #region 检查文件是否存在

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto Exist(ExistParam request)
        {
            try
            {
                var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
                var exist = client.DoesObjectExist(bucket, request.Key);
                return new OperateResultDto(exist, "success");
            }
            catch (OssException ex)
            {
                return new OperateResultDto(false,
                    $"Failed with error code: {ex.ErrorCode}; Error info: {ex.Message}. \nRequestID:{ex.RequestId}\tHostID:{ex.HostId}");
            }
            catch (BusinessException<string>e)
            {
                return new UploadResultDto(false, e, e.Message);
            }
            catch (Exception ex)
            {
                return new OperateResultDto(false, Core.Tools.GetMessage(ex));
            }
        }

        #endregion

        #region 获取指定前缀的文件列表

        /// <summary>
        /// 获取指定前缀的文件列表
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <returns></returns>
        public ListFileItemResultDto ListFiles(ListFileFilter filter)
        {
            try
            {
                new ListFileFilterValidator().Validate(filter).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(this._aLiYunConfig, filter.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, filter.PersistentOps.Bucket);
                var listObjectsRequest = new ListObjectsRequest(bucket)
                {
                    MaxKeys = filter.PageSize
                };
                if (!string.IsNullOrEmpty(filter.Delimiter))
                {
                    listObjectsRequest.Delimiter = filter.Delimiter;
                }

                if (!string.IsNullOrEmpty(filter.Marker))
                {
                    listObjectsRequest.Marker = filter.Marker;
                }

                if (!string.IsNullOrEmpty(filter.Prefix))
                {
                    listObjectsRequest.Prefix = filter.Prefix;
                }

                var ret = client.ListObjects(listObjectsRequest);
                if (ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new ListFileItemResultDto(true, "success")
                    {
                        CommonPrefixes = ret.CommonPrefixes?.ToList() ?? new List<string>(),
                        Marker = ret.NextMarker,
                        Items = ret.ObjectSummaries.Select(x => new FileInfoDto(true, "success")
                        {
                            Key = x.Key,
                            Hash = "",
                            Size = x.Size,
                            PutTime = x.LastModified.ToUnixTimestamp(TimestampType.Millisecond),
                            MimeType = "",
                            FileType = Core.Tools.GetStorageClass(x.StorageClass).Id,
                        }).ToList()
                    };
                }

                return new ListFileItemResultDto(false,
                    $"RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}");
            }
            catch (BusinessException<string>ex)
            {
                return new ListFileItemResultDto(false, ex.Message);
            }
            catch (Exception ex)
            {
                return new ListFileItemResultDto(false, Core.Tools.GetMessage(ex));
            }
        }

        #endregion

        public FileInfoDto Get(GetFileParam request)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<FileInfoDto> GetList(GetFileRangeParam request)
        {
            throw new System.NotImplementedException();
        }

        #region 删除文件

        #region 根据key删除文件

        /// <summary>
        /// 根据key删除文件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DeleteResultDto Remove(RemoveParam request)
        {
            try
            {
                new RemoveParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
                var ret = client.DeleteObject(bucket, request.Key);
                if (ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new DeleteResultDto(true, request.Key, "success");
                }

                return new DeleteResultDto(false, request.Key,
                    $"RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}");
            }
            catch (BusinessException<string> ex)
            {
                return new DeleteResultDto(false, request.Key, ex.Message);
            }
            catch (Exception ex)
            {
                return new DeleteResultDto(false, request.Key, Core.Tools.GetMessage(ex));
            }
        }

        #endregion

        #region 根据文件key集合删除

        /// <summary>
        /// 根据文件key集合删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<DeleteResultDto> RemoveRange(RemoveRangeParam request)
        {
            try
            {
                new RemoveRangeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
                var ret = client.DeleteObjects(new DeleteObjectsRequest(bucket, request.Keys, false));
                if (ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    List<DeleteResultDto> deleteResultList = new List<DeleteResultDto>();
                    request.Keys.ForEach(key =>
                    {
                        if (ret.Keys.Any(x => x.Key == key))
                        {
                            deleteResultList.Add(new DeleteResultDto(true, key, "success"));
                        }
                        else
                        {
                            deleteResultList.Add(new DeleteResultDto(true, key,
                                $"delete lose，RequestId：{ret.RequestId}"));
                        }
                    });
                    return deleteResultList;
                }

                return request.Keys.Select(x =>
                    new DeleteResultDto(false, x,
                        $"delete lose，RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}"));
            }
            catch (BusinessException<string> ex)
            {
                return request.Keys.Select(x =>
                    new DeleteResultDto(false, x, ex.Message));
            }
            catch (Exception ex)
            {
                return request.Keys.Select(x =>
                    new DeleteResultDto(false, x, Core.Tools.GetMessage(ex)));
            }
        }

        #endregion

        #endregion

        #region 复制文件（两个文件需要在同一账号下）

        /// <summary>
        /// 复制文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CopyFileResultDto CopyTo(CopyFileParam request)
        {
            try
            {
                new CopyFileParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
                var targetBucket =
                    Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket, request.OptBucket);

                if (Core.Tools.GetChunkUnit(this._aLiYunConfig, request.PersistentOps.ChunkUnit).Id !=
                    ChunkUnit.U4096K.Id)
                {
                    return CopySmallFile(client, bucket, request.SourceKey, targetBucket, request.OptKey);
                }

                return CopyBigFile(client, bucket, request.SourceKey, targetBucket, request.OptKey);
            }
            catch (BusinessException<string>ex)
            {
                return new CopyFileResultDto(false, request.SourceKey, ex.Message);
            }
            catch (Exception ex)
            {
                return new CopyFileResultDto(false, request.SourceKey, Core.Tools.GetMessage(ex));
            }
        }

        /// <summary>
        /// 拷贝小文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sourceBucket">源空间</param>
        /// <param name="sourceKey">源文件</param>
        /// <param name="optBucket">目标空间</param>
        /// <param name="optKey">目标文件</param>
        /// <returns></returns>
        private CopyFileResultDto CopySmallFile(OssClient client, string sourceBucket, string sourceKey,
            string optBucket, string optKey)
        {
            var req = new CopyObjectRequest(sourceBucket, sourceKey, optBucket, optKey)
            {
                NewObjectMetadata =
                    null // 如果NewObjectMetadata为null则为COPY模式（即拷贝源文件的元信息），非null则为REPLACE模式（覆盖源文件的元信息）。
            };
            // 拷贝文件。
            var ret = client.CopyObject(req);
            if (ret.HttpStatusCode == HttpStatusCode.OK)
            {
                return new CopyFileResultDto(true, sourceKey, "success");
            }

            return new CopyFileResultDto(false, sourceKey,
                $"RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sourceBucket">源空间</param>
        /// <param name="sourceKey">源文件</param>
        /// <param name="optBucket">目标空间</param>
        /// <param name="optKey">目标文件</param>
        /// <returns></returns>
        private CopyFileResultDto CopyBigFile(OssClient client, string sourceBucket, string sourceKey,
            string optBucket, string optKey)
        {
            var initiateMultipartUploadRequest = new InitiateMultipartUploadRequest(optBucket, optKey);
            var result = client.InitiateMultipartUpload(initiateMultipartUploadRequest);

            var partSize = Core.Tools.GetPartSize(ChunkUnit.U4096K);
            var metadata = client.GetObjectMetadata(sourceBucket, sourceKey);
            var fileSize = metadata.ContentLength;
            var partCount = (int) fileSize / partSize;
            if (fileSize % partSize != 0)
            {
                partCount++;
            }

            // 开始分片拷贝。
            var partETags = new List<PartETag>();
            for (var i = 0; i < partCount; i++)
            {
                var skipBytes = (long) partSize * i;
                var size = (partSize < fileSize - skipBytes) ? partSize : (fileSize - skipBytes);
                // 创建UploadPartCopyRequest。可以通过UploadPartCopyRequest指定限定条件。
                var uploadPartCopyRequest =
                    new UploadPartCopyRequest(optBucket, optKey, sourceBucket, sourceKey,
                        result.UploadId)
                    {
                        PartSize = size,
                        PartNumber = i + 1,
                        // BeginIndex用来定位此次上传分片开始所对应的位置。
                        BeginIndex = skipBytes
                    };
                // 调用uploadPartCopy方法来拷贝每一个分片。
                var uploadPartCopyResult = client.UploadPartCopy(uploadPartCopyRequest);
                Console.WriteLine("UploadPartCopy : {0}", i);
                partETags.Add(uploadPartCopyResult.PartETag);
            }

            // 完成分片拷贝。
            var completeMultipartUploadRequest =
                new CompleteMultipartUploadRequest(optBucket, optKey, result.UploadId);
            // partETags为分片上传中保存的partETag的列表，OSS收到用户提交的此列表后，会逐一验证每个数据分片的有效性。全部验证通过后，OSS会将这些分片合成一个完整的文件。
            foreach (var partETag in partETags)
            {
                completeMultipartUploadRequest.PartETags.Add(partETag);
            }

            var ret = client.CompleteMultipartUpload(completeMultipartUploadRequest);
            if (ret.HttpStatusCode == HttpStatusCode.OK)
            {
                return new CopyFileResultDto(true, sourceKey, "success");
            }

            return new CopyFileResultDto(false, sourceKey,
                $"RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}");
        }

        #endregion

        #region 批量复制文件（两个文件需要在同一账号下）

        /// <summary>
        /// 批量复制文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<CopyFileResultDto> CopyRangeTo(CopyFileRangeParam request)
        {
            foreach (var copyFile in request.CopyFiles)
            {
                yield return CopyTo(new CopyFileParam(copyFile.SourceKey, copyFile.OptKey, copyFile.OptBucket,
                    copyFile.IsForce, request.PersistentOps));
            }
        }

        #endregion

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
            new GetPrivateUrlParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
            var client = _aLiYunConfig.GetClient(zone);
            var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
            var req = new GeneratePresignedUriRequest(bucket, request.Key, SignHttpMethod.Get);

            var uri = client.GeneratePresignedUri(req);
            OssObject ossObject = client.GetObject(uri);

            return uri.ToString();
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

        #region 文件访问权限

        #region 设置文件访问权限

        /// <summary>
        /// 设置文件访问权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto SetPermiss(SetPermissParam request)
        {
            try
            {
                new SetPermissParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
                var permiss = request.Permiss != null
                    ? Maps.GetCannedAccessControl(request.Permiss)
                    : CannedAccessControlList.Default;
                client.SetObjectAcl(bucket, request.Key, permiss);
                return new OperateResultDto(true, "success");
            }
            catch (Exception e)
            {
                return new OperateResultDto(false,
                    Core.Tools.GetMessage(e));
            }
        }

        #endregion

        #region 得到文件访问权限

        /// <summary>
        /// 获取文件权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public FilePermissResultInfo GetPermiss(GetFilePermissParam request)
        {
            try
            {
                new GetFilePermissParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
                var ret = client.GetObjectAcl(bucket, request.Key);

                if (ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new FilePermissResultInfo(true, Maps.GetPermiss(ret.ACL), "success");
                }

                return new FilePermissResultInfo(false, null, "文件不存在" + ret.ToString());
            }
            catch (Exception e)
            {
                return new FilePermissResultInfo(false, null,
                    Core.Tools.GetMessage(e));
            }
        }

        #endregion

        #endregion
    }
}
