// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Aliyun.OSS;
using Aliyun.OSS.Common;
using Aliyun.OSS.Common.Authentication;
using Aliyun.OSS.Model;
using EInfrastructure.Core.Aliyun.Storage.Config;
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
using EInfrastructure.Core.Http;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Enumerations;
using EInfrastructure.Core.Validation.Common;
using ICredentials = Aliyun.OSS.Common.Authentication.ICredentials;
using StorageClass = EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass;

namespace EInfrastructure.Core.Aliyun.Storage
{
    /// <summary>
    ///
    /// </summary>
    public class StorageProvider : BaseStorageProvider, IStorageProvider
    {
        private readonly ALiYunStorageConfig _aLiYunConfig;

        /// <summary>
        ///
        /// </summary>
        /// <param name="aliyunConfig"></param>
        public StorageProvider(ALiYunStorageConfig aliyunConfig) : base(aliyunConfig)
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
                return Upload(isResume, (bucket, newPersistentOps, objectMetadata) =>
                {
                    PutObjectResult ret;
                    if (isResume)
                    {
                        var request = new UploadObjectRequest(bucket, param.Key, param.Stream)
                        {
                            PartSize = Core.Tools.GetPartSize(
                                Core.Tools.GetChunkUnit(this._aLiYunConfig, newPersistentOps.ChunkUnit,
                                    () => ChunkUnit.U2048K)),
                            Metadata = objectMetadata
                        };
                        ret = client.ResumableUploadObject(request);
                    }
                    else
                    {
                        ret = client.PutObject(bucket, param.Key, param.Stream, objectMetadata);
                    }

                    return ret;
                }, param.UploadPersistentOps);
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
                return Upload(isResume, (bucket, newPersistentOps, objectMetadata) =>
                {
                    PutObjectResult ret;
                    if (isResume)
                    {
                        var request = new UploadObjectRequest(bucket, param.Key, param.ByteArray.ConvertToStream())
                        {
                            PartSize = Core.Tools.GetPartSize(
                                Core.Tools.GetChunkUnit(this._aLiYunConfig, newPersistentOps.ChunkUnit,
                                    () => ChunkUnit.U2048K)),
                            Metadata = objectMetadata
                        };
                        ret = client.ResumableUploadObject(request);
                    }
                    else
                    {
                        ret = client.PutObject(bucket, param.Key, param.ByteArray.ConvertToStream(), objectMetadata);
                    }

                    return ret;
                }, param.UploadPersistentOps);
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

        #region 根据文件流以及文件字节数组上传

        /// <summary>
        /// 根据文件流以及文件字节数组上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public UploadResultDto UploadByToken(UploadByTokenParam param)
        {
            try
            {
                var zone = Core.Tools.GetZone(this._aLiYunConfig, param.UploadPersistentOps.Zone,
                    () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone, param.Token);
                return Upload(param.IsResume, (bucket, newPersistentOps, objectMetadata) =>
                {
                    PutObjectResult ret;
                    if (param.IsResume)
                    {
                        var request = new UploadObjectRequest(bucket, param.Key, param.Stream)
                        {
                            PartSize = Core.Tools.GetPartSize(
                                Core.Tools.GetChunkUnit(this._aLiYunConfig, newPersistentOps.ChunkUnit,
                                    () => ChunkUnit.U2048K)),
                            Metadata = objectMetadata
                        };
                        ret = client.ResumableUploadObject(request);
                    }
                    else
                    {
                        ret = client.PutObject(bucket, param.Key, param.Stream, objectMetadata);
                    }

                    return ret;
                }, param.UploadPersistentOps);
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

        #region 上传文件

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="isResume"></param>
        /// <param name="funcAction"></param>
        /// <param name="uploadPersistentOps"></param>
        /// <returns></returns>
        private UploadResultDto Upload(bool isResume,
            Func<string, UploadPersistentOps, ObjectMetadata, PutObjectResult> funcAction,
            UploadPersistentOps uploadPersistentOps)
        {
            try
            {
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, uploadPersistentOps.Bucket);
                var persistentOps = base.GetUploadPersistentOps(uploadPersistentOps);
                var metadata = base.GetCallbackMetadata(persistentOps);
                PutObjectResult ret = funcAction.Invoke(bucket, persistentOps, metadata);
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

        #region 获得凭证

        #region 得到上传文件凭证

        /// <summary>
        /// 得到上传文件凭证
        /// </summary>
        /// <param name="opsParam">上传信息</param>
        public string GetUploadCredentials(UploadPersistentOpsParam opsParam)
        {
            return GetManageToken(new GetManageTokenParam(opsParam.Key, opsParam.UploadPersistentOps));
        }

        #endregion

        #region 得到管理凭证

        /// <summary>
        /// 得到管理凭证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetManageToken(GetManageTokenParam request)
        {
            ICredentialsProvider provider = (ICredentialsProvider) new DefaultCredentialsProvider(
                (ICredentials) new DefaultCredentials(_aLiYunConfig.AccessKey,
                    _aLiYunConfig.SecretKey, (string) null));
            return provider.GetCredentials().SecurityToken;
        }

        #endregion

        #region 得到下载凭证

        /// <summary>
        /// 得到下载凭证
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetDownloadToken(string url)
        {
            return GetManageToken(new GetManageTokenParam(url, new BasePersistentOps()));
        }

        #endregion

        #endregion

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
                            FileType = Core.Tools.GetStorageClass(x.StorageClass),
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

        #region 获取文件信息

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public FileInfoDto Get(GetFileParam request)
        {
            try
            {
                new GetFileParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
                var ret = client.GetObject(bucket, request.Key);
                if (ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    string fileTypeStr = ret.Metadata.HttpMetadata.Where(x => x.Key == "x-oss-storage-class")
                        .Select(x => x.Value.ToString()).FirstOrDefault();
                    EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass fileType = null;
                    if (!string.IsNullOrEmpty(fileTypeStr))
                    {
                        fileType = Core.Tools.GetStorageClass(fileTypeStr);
                    }

                    return new FileInfoDto(true, "success")
                    {
                        Hash = ret.Metadata.ContentMd5,
                        Key = ret.Key,
                        Size = ret.Metadata.ContentLength,
                        PutTime = ret.Metadata.LastModified.ToUnixTimestamp(TimestampType.Second),
                        MimeType = ret.Metadata.ContentType,
                        FileType = fileType
                    };
                }

                return new FileInfoDto(false, $"lose RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}")
                {
                    Key = request.Key
                };
            }
            catch (BusinessException<string> ex)
            {
                return new FileInfoDto(false, ex.Message);
            }
            catch (Exception ex)
            {
                return new FileInfoDto(false, Core.Tools.GetMessage(ex));
            }
        }

        #endregion

        #region 批量获取文件信息

        /// <summary>
        /// 批量获取文件信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<FileInfoDto> GetList(GetFileRangeParam request)
        {
            try
            {
                new GetFileRangeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                List<FileInfoDto> list = new List<FileInfoDto>();
                foreach (var key in request.Keys)
                {
                    list.Add(Get(new GetFileParam(key, request.PersistentOps)));
                }

                return list;
            }
            catch (Exception e)
            {
                return new List<FileInfoDto>();
            }
        }

        #endregion

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

        #region 复制文件

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
        /// 拷贝大文件
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

        #endregion

        #region 移动文件

        #region 移动文件（两个文件需要在同一账号下）

        /// <summary>
        /// 移动文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="moveFileParam"></param>
        /// <returns></returns>
        public MoveFileResultDto Move(MoveFileParam moveFileParam)
        {
            new MoveFileParamValidator().Validate(moveFileParam).Check(HttpStatus.Err.Name);
            CopyTo(new CopyFileParam(moveFileParam.SourceKey, moveFileParam.OptKey, moveFileParam.OptBucket, true,
                moveFileParam.PersistentOps));
            Remove(new RemoveParam(moveFileParam.SourceKey, moveFileParam.PersistentOps));
            return new MoveFileResultDto(true, moveFileParam.OptKey, "success");
        }

        #endregion

        #region 批量移动文件（两个文件需要在同一账号下）

        /// <summary>
        /// 批量移动文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<MoveFileResultDto> MoveRange(MoveFileRangeParam request)
        {
            new MoveFileParamRangeValidator().Validate(request).Check(HttpStatus.Err.Name);
            List<MoveFileResultDto> list = new List<MoveFileResultDto>();
            request.MoveFiles.ForEach(file =>
            {
                list.Add(Move(new MoveFileParam(file.SourceKey, file.OptBucket, file.OptKey, file.IsForce,
                    request.PersistentOps)));
            });
            return list;
        }

        #endregion

        #endregion

        #region 得到访问地址

        /// <summary>
        /// 得到访问地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetVisitUrlResultDto GetVisitUrl(GetVisitUrlParam request)
        {
            try
            {
                new GetVisitUrlParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
                var req = new GeneratePresignedUriRequest(bucket, request.Key, SignHttpMethod.Get);
                var uri = client.GeneratePresignedUri(req);
                return new GetVisitUrlResultDto(uri.ToString(), "success");
            }
            catch (BusinessException<string>ex)
            {
                return new GetVisitUrlResultDto(ex.Message);
            }
            catch (Exception ex)
            {
                return new GetVisitUrlResultDto(Core.Tools.GetMessage(ex));
            }
        }

        #endregion

        #region 下载文件

        #region 下载文件（根据已授权的地址）

        /// <summary>
        /// 下载文件（根据已授权的地址）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DownloadResultDto Download(FileDownloadParam request)
        {
            try
            {
                new FileDownloadParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                Uri uri = new Uri(request.Url);
                string host = $"{uri.Scheme}://{uri.Host}";
                using (var file = File.Open(request.SavePath, FileMode.OpenOrCreate))
                {
                    using (Stream stream = new HttpClient(host).GetStream(request.Url.Replace(host, "")))
                    {
                        int length = 4 * 1024;
                        var buf = new byte[length];
                        do
                        {
                            length = stream.Read(buf, 0, length);
                            file.Write(buf, 0, length);
                        } while (length != 0);
                    }
                }

                return new DownloadResultDto(true, "success");
            }
            catch (BusinessException<string>ex)
            {
                return new DownloadResultDto(false, ex.Message, ex);
            }
            catch (Exception ex)
            {
                return new DownloadResultDto(false, Core.Tools.GetMessage(ex));
            }
        }

        #endregion

        #region 获取文件流（根据已授权的地址）

        /// <summary>
        /// 获取文件流（根据已授权的地址）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DownloadStreamResultDto DownloadStream(FileDownloadStreamParam request)
        {
            try
            {
                new FileDownloadStreamParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                Uri uri = new Uri(request.Url);
                string host = $"{uri.Scheme}://{uri.Host}";
                return new DownloadStreamResultDto(true, "success",
                    new HttpClient(host).GetStream(request.Url.Replace(host, "")), null);
            }
            catch (BusinessException<string>ex)
            {
                return new DownloadStreamResultDto(ex.Message);
            }
            catch (Exception ex)
            {
                return new DownloadStreamResultDto(Core.Tools.GetMessage(ex));
            }
        }

        #endregion

        #endregion

        #region 生存管理

        #region 设置生存时间（超时会自动删除）

        /// <summary>
        /// 设置生存时间（超时会自动删除）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ExpireResultDto SetExpire(SetExpireParam request)
        {
            return new ExpireResultDto(false, request.Key, "不支持设置过期时间");
        }

        #endregion

        #region 批量设置生存时间（超时会自动删除）

        /// <summary>
        /// 批量设置生存时间（超时会自动删除）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ExpireResultDto> SetExpireRange(SetExpireRangeParam request)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #endregion

        #region 修改文件MimeType

        #region 修改文件MimeType

        /// <summary>
        /// 修改文件MimeType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ChangeMimeResultDto ChangeMime(ChangeMimeParam request)
        {
            try
            {
                new ChangeMimeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
                var ret = client.GetObject(bucket, request.Key);
                if (ret != null && ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    ObjectMetadata objectMetadata = ret.Metadata;
                    if (objectMetadata == null)
                    {
                        objectMetadata = new ObjectMetadata();
                    }

                    objectMetadata.ContentType = request.MimeType;
                    client.ModifyObjectMeta(bucket, request.Key, objectMetadata);
                    return new ChangeMimeResultDto(true, request.Key, "success");
                }

                if (ret != null)
                {
                    return new ChangeMimeResultDto(false, request.Key,
                        $"lose，RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}");
                }

                return new ChangeMimeResultDto(false, request.Key, "lose");
            }
            catch (BusinessException<string>ex)
            {
                return new ChangeMimeResultDto(false, request.Key, ex.Message);
            }
            catch (Exception ex)
            {
                return new ChangeMimeResultDto(false, request.Key, Core.Tools.GetMessage(ex));
            }
        }

        #endregion

        #region 批量更改文件mime

        /// <summary>
        /// 批量更改文件mime
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ChangeMimeResultDto> ChangeMimeRange(ChangeMimeRangeParam request)
        {
            new ChangeMimeRangeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            List<ChangeMimeResultDto> mimeResultList = new List<ChangeMimeResultDto>();
            request.Keys.ForEach(key =>
            {
                mimeResultList.Add(ChangeMime(new ChangeMimeParam(key, request.MimeType, request.PersistentOps)));
            });
            return mimeResultList;
        }

        #endregion

        #endregion

        #region 文件存储类型

        #region 修改文件存储类型

        /// <summary>
        /// 修改文件存储类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ChangeTypeResultDto ChangeType(ChangeTypeParam request)
        {
            try
            {
                new ChangeTypeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var fileInfo = Get(new GetFileParam(request.Key, request.PersistentOps));
                if (fileInfo.State)
                {
                    var zone = Core.Tools.GetZone(this._aLiYunConfig, request.PersistentOps.Zone,
                        () => ZoneEnum.HangZhou);
                    var client = _aLiYunConfig.GetClient(zone);
                    var bucket = Core.Tools.GetBucket(this._aLiYunConfig, request.PersistentOps.Bucket);
                    if (fileInfo.FileType.Id == StorageClass.Archive.Id)
                    {
                        //解除归档
                        RestoreObjectResult result = client.RestoreObject(bucket, request.Key);
                        if (result != null && result.HttpStatusCode == HttpStatusCode.OK)
                        {
                            return new ChangeTypeResultDto(false, request.Key, "解除归档成功，请稍后再尝试更改文件存储类型");
                        }

                        if (result != null)
                        {
                            return new ChangeTypeResultDto(false, request.Key,
                                $"解除归档失败，请稍后重试，RequestId：{result.RequestId}，HttpStatusCode：{result.HttpStatusCode}");
                        }

                        return new ChangeTypeResultDto(false, request.Key, "解除归档失败，请稍后重试");
                    }

                    var ret = client.GetObject(bucket, request.Key);
                    if (ret != null && ret.HttpStatusCode == HttpStatusCode.OK)
                    {
                        ObjectMetadata objectMetadata = new ObjectMetadata();
                        foreach (var metadata in ret.Metadata.HttpMetadata)
                        {
                            if (metadata.Key != "x-oss-storage-class")
                            {
                                objectMetadata.AddHeader(metadata.Key, metadata.Value);
                            }
                            else
                            {
                                objectMetadata.AddHeader(metadata.Key,
                                    Core.Tools.GetStorageClass(request.Type).ToString());
                            }
                        }

                        client.ModifyObjectMeta(bucket, request.Key, objectMetadata);
                        return new ChangeTypeResultDto(true, request.Key, "success");
                    }

                    if (ret != null)
                    {
                        return new ChangeTypeResultDto(false, request.Key,
                            $"lose，RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}");
                    }

                    return new ChangeTypeResultDto(false, request.Key, "lose");
                }

                return new ChangeTypeResultDto(false, request.Key, fileInfo.Msg);
            }
            catch (BusinessException<string>ex)
            {
                return new ChangeTypeResultDto(false, request.Key, ex.Message);
            }
            catch (Exception ex)
            {
                return new ChangeTypeResultDto(false, request.Key, Core.Tools.GetMessage(ex));
            }
        }

        #endregion

        #region 批量更改文件类型

        /// <summary>
        /// 批量更改文件类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ChangeTypeResultDto> ChangeTypeRange(ChangeTypeRangeParam request)
        {
            new ChangeTypeRangeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            List<ChangeTypeResultDto> resultList = new List<ChangeTypeResultDto>();
            request.Keys.ForEach(key =>
            {
                resultList.Add(ChangeType(new ChangeTypeParam(key, request.Type, request.PersistentOps)));
            });
            return resultList;
        }

        #endregion

        #endregion

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
                    ? Core.Tools.GetCannedAccessControl(request.Permiss)
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
                    return new FilePermissResultInfo(true, Core.Tools.GetPermiss(ret.ACL), "success");
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

        #region 抓取资源到空间

        /// <summary>
        /// 抓取资源到空间
        /// </summary>
        /// <param name="fetchFileParam"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool FetchFile(FetchFileParam fetchFileParam)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
