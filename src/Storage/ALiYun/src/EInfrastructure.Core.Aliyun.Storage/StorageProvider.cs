// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Aliyun.OSS;
using Aliyun.OSS.Common.Authentication;
using Aliyun.OSS.Model;
using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Aliyun.Storage.Enum;
using EInfrastructure.Core.Aliyun.Storage.Validator.Storage;
using EInfrastructure.Core.Configuration.Enumerations;
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
using LifecycleRule = Aliyun.OSS.LifecycleRule;
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

        #region 上传文件

        #region 根据文件流上传

        /// <summary>
        /// 根据文件流上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="isResume">是否允许续传</param>
        /// <returns></returns>
        public UploadResultDto UploadStream(UploadByStreamParam param, bool isResume = false)
        {
            return ToolCommon.GetResponse(() =>
            {
                var zone = Core.Tools.GetZone(_aLiYunConfig, param.UploadPersistentOps.Zone,
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
                                Core.Tools.GetChunkUnit(_aLiYunConfig, newPersistentOps.ChunkUnit,
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
            }, (message, ex) => new UploadResultDto(false, ex, message));
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
            return ToolCommon.GetResponse(() =>
            {
                var zone = Core.Tools.GetZone(_aLiYunConfig, param.UploadPersistentOps.Zone,
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
                                Core.Tools.GetChunkUnit(_aLiYunConfig, newPersistentOps.ChunkUnit,
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
            }, (message, ex) => new UploadResultDto(false, ex, message));
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
            return ToolCommon.GetResponse(() =>
            {
                var zone = Core.Tools.GetZone(_aLiYunConfig, param.UploadPersistentOps.Zone,
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
                                Core.Tools.GetChunkUnit(_aLiYunConfig, newPersistentOps.ChunkUnit,
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
            }, (message, ex) => new UploadResultDto(false, ex, message));
        }

        #endregion

        #region private methods

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
            return ToolCommon.GetResponse(() =>
            {
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, uploadPersistentOps.Bucket);
                var persistentOps = base.GetUploadPersistentOps(uploadPersistentOps);
                var metadata = GetCallbackMetadata(persistentOps);
                PutObjectResult ret = funcAction.Invoke(bucket, persistentOps, metadata);
                if (ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new UploadResultDto(true, null, "success");
                }

                return new UploadResultDto(false, ret, $"RequestId：{ret.RequestId}");
            }, (message, ex) => new UploadResultDto(false, ex, message));
        }

        #endregion

        #endregion

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
        public string GetManageToken(GetManageTokenParam request)
        {
            ICredentialsProvider provider = (ICredentialsProvider) new DefaultCredentialsProvider(
                (ICredentials) new DefaultCredentials(_aLiYunConfig.AccessKey,
                    _aLiYunConfig.SecretKey, (string) null));
            return provider.GetCredentials().SecurityToken;
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
            return ToolCommon.GetResponse(() =>
            {
                var zone = Core.Tools.GetZone(_aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket);
                var exist = client.DoesObjectExist(bucket, request.Key);
                return new OperateResultDto(exist, "success");
            }, message => new OperateResultDto(false, message));
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
            return ToolCommon.GetResponse(() =>
            {
                new ListFileFilterValidator().Validate(filter).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(_aLiYunConfig, filter.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, filter.PersistentOps.Bucket);
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
                    var list = new ListFileItemResultDto(true, "success")
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

                    if (filter.IsShowHash)
                    {
                        var fileList = GetList(new GetFileRangeParam(list.Items.Select(x => x.Key).ToList(),
                            filter.PersistentOps));
                        list.Items.ForEach(item =>
                        {
                            var fileInfo = fileList.FirstOrDefault(x => x.Key == item.Key);
                            if (fileInfo != null)
                            {
                                item.Hash = fileInfo.Hash;
                            }
                        });
                    }

                    return list;
                }

                return new ListFileItemResultDto(false,
                    $"RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}");
            }, message => new ListFileItemResultDto(false,
                message));
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
            return ToolCommon.GetResponse(() =>
            {
                new GetFileParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(_aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket);
                var ret = client.GetObject(bucket, request.Key);
                if (ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    string fileTypeStr = ret.Metadata.HttpMetadata.Where(x => x.Key == "x-oss-storage-class")
                        .Select(x => x.Value.ToString()).FirstOrDefault();
                    StorageClass fileType = null;
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
            }, message => new FileInfoDto(false, message));
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
            new GetFileRangeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            List<FileInfoDto> list = new List<FileInfoDto>();
            foreach (var key in request.Keys)
            {
                list.Add(Get(new GetFileParam(key, request.PersistentOps)));
            }

            return list;
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
            return ToolCommon.GetResponse(() =>
            {
                new RemoveParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(_aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket);
                var ret = client.DeleteObject(bucket, request.Key);
                if (ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new DeleteResultDto(true, request.Key, "success");
                }

                return new DeleteResultDto(false, request.Key,
                    $"RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}");
            }, message => new DeleteResultDto(false, request.Key, message));
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
            return ToolCommon.GetResponse(() =>
            {
                new RemoveRangeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(_aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket);
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
            }, message =>
                request.Keys.Select(x =>
                    new DeleteResultDto(false, x, message)));
        }

        #endregion

        #endregion

        #region 复制文件

        #region 复制文件（两个文件需要在同一账号下）

        /// <summary>
        /// 复制文件（两个文件需要在同一账号下）
        /// 对于小于1G的文件（不支持跨地域拷贝。例如，不支持将杭州存储空间里的文件拷贝到青岛），另外需将分片设置为4M，其他分类不支持
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CopyFileResultDto CopyTo(CopyFileParam request)
        {
            return ToolCommon.GetResponse(() =>
            {
                new CopyFileParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(_aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket);
                var targetBucket =
                    Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket, request.OptBucket);

                if (!request.IsForce)
                {
                    var newBasePersistentOps = request.PersistentOps.Clone();
                    newBasePersistentOps.Bucket = request.OptBucket;
                    var existRet = this.Exist(new ExistParam(request.OptKey, newBasePersistentOps));
                    if (existRet.State)
                    {
                        return new CopyFileResultDto(false,request.SourceKey,"复制失败，文件已存在");
                    }
                }
                if (Core.Tools.GetChunkUnit(_aLiYunConfig, request.PersistentOps.ChunkUnit).Id !=
                    ChunkUnit.U4096K.Id)
                {
                    return CopySmallFile(client, bucket, request.SourceKey, targetBucket, request.OptKey);
                }

                return CopyBigFile(client, bucket, request.SourceKey, targetBucket, request.OptKey);
            }, message => new CopyFileResultDto(false, request.SourceKey, message));
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
        /// <param name="request"></param>
        /// <returns></returns>
        public MoveFileResultDto Move(MoveFileParam request)
        {
            return ToolCommon.GetResponse(() =>
            {
                new MoveFileParamValidator(_aLiYunConfig).Validate(request).Check(HttpStatus.Err.Name);

                if (!request.IsForce)
                {
                    var newBasePersistentOps = request.PersistentOps.Clone();
                    newBasePersistentOps.Bucket = request.OptBucket;
                    var existRet = this.Exist(new ExistParam(request.OptKey, newBasePersistentOps));
                    if (existRet.State)
                    {
                        return new MoveFileResultDto(false,request.SourceKey,"移动失败，文件已存在");
                    }
                }
                CopyTo(new CopyFileParam(request.SourceKey, request.OptKey, request.OptBucket, request.IsForce,
                    request.PersistentOps));
                Remove(new RemoveParam(request.SourceKey, request.PersistentOps));

                return new MoveFileResultDto(true, request.SourceKey, "success");
            }, message => new MoveFileResultDto(false, request.SourceKey, message));
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
                var ret = Move(new MoveFileParam(file.SourceKey, file.OptBucket,
                    file.OptKey, file.IsForce,
                    request.PersistentOps));
                list.Add(ret);
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
            return ToolCommon.GetResponse(() =>
            {
                new GetVisitUrlParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(_aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket);
                var req = new GeneratePresignedUriRequest(bucket, request.Key, SignHttpMethod.Get);
                var uri = client.GeneratePresignedUri(req);
                return new GetVisitUrlResultDto(uri.ToString(), "success");
            }, message => new GetVisitUrlResultDto(message));
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
            return ToolCommon.GetResponse(() =>
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
            }, (message, ex) => new DownloadResultDto(false, message, ex));
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
            return ToolCommon.GetResponse(() =>
            {
                new FileDownloadStreamParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                Uri uri = new Uri(request.Url);
                string host = $"{uri.Scheme}://{uri.Host}";
                return new DownloadStreamResultDto(true, "success",
                    new HttpClient(host).GetStream(request.Url.Replace(host, "")), null);
            }, message => new DownloadStreamResultDto(message));
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
            return ToolCommon.GetResponse(() =>
            {
                new SetExpireParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(_aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket);
                var setBucketLifecycleRequest = new SetBucketLifecycleRequest(bucket);

                LifecycleRule lcr = new LifecycleRule()
                {
                    ID = "delete " + request.Key + " files",
                    Prefix = request.Key,
                    Status = RuleStatus.Enabled,
                    ExpriationDays = request.Expire
                };
                setBucketLifecycleRequest.AddLifecycleRule(lcr);
                client.SetBucketLifecycle(setBucketLifecycleRequest); //调整生命周期
                return new ExpireResultDto(true, request.Key, "success");
            }, message => new ExpireResultDto(false, request.Key, message));
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
            return ToolCommon.GetResponse(() =>
            {
                var zone = Core.Tools.GetZone(_aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket);
                var setBucketLifecycleRequest = new SetBucketLifecycleRequest(bucket);

                List<ExpireResultDto> list = new List<ExpireResultDto>();
                request.Keys.ForEach(key =>
                {
                    LifecycleRule lcr = new LifecycleRule()
                    {
                        ID = "delete " + key + " files",
                        Prefix = key,
                        Status = RuleStatus.Enabled,
                        ExpriationDays = request.Expire
                    };
                    setBucketLifecycleRequest.AddLifecycleRule(lcr);
                    list.Add(new ExpireResultDto(true, key, "success"));
                });

                client.SetBucketLifecycle(setBucketLifecycleRequest); //调整生命周期
                return list;
            }, message =>
            {
                List<ExpireResultDto> list = new List<ExpireResultDto>();
                request.Keys.ForEach(key => { list.Add(new ExpireResultDto(false, key, message)); });
                return list;
            });
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
            return ToolCommon.GetResponse(() =>
            {
                new ChangeMimeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(_aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket);
                var ret = client.GetObject(bucket, request.Key);
                if (ret != null && ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    ObjectMetadata objectMetadata =Core.Tools.GetObjectMetadataBySourceObjectMetadata(ret.Metadata, "ContentType",
                        request.MimeType);
                    client.ModifyObjectMeta(bucket, request.Key, objectMetadata);
                    return new ChangeMimeResultDto(true, request.Key, "success");
                }

                if (ret != null)
                {
                    return new ChangeMimeResultDto(false, request.Key,
                        $"lose，RequestId：{ret.RequestId}，HttpStatusCode：{ret.HttpStatusCode}");
                }

                return new ChangeMimeResultDto(false, request.Key, "lose");
            }, message => new ChangeMimeResultDto(false, request.Key, message));
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
                mimeResultList.Add(string.IsNullOrEmpty(request.MimeType)
                    ? new ChangeMimeResultDto(false, key, "请输入文件MimeType")
                    : ChangeMime(new ChangeMimeParam(key, request.MimeType, request.PersistentOps)));
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
            return ToolCommon.GetResponse(() =>
            {
                new ChangeTypeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var fileInfo = Get(new GetFileParam(request.Key, request.PersistentOps));
                if (fileInfo.State)
                {
                    var zone = Core.Tools.GetZone(_aLiYunConfig, request.PersistentOps.Zone,
                        () => ZoneEnum.HangZhou);
                    var client = _aLiYunConfig.GetClient(zone);
                    var bucket = Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket);
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
                        ObjectMetadata objectMetadata =
                            Core.Tools.GetObjectMetadataBySourceObjectMetadata(ret.Metadata, "x-oss-storage-class",
                                Core.Tools.GetStorageClass(request.Type).ToString());
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
            }, message => new ChangeTypeResultDto(false, request.Key, message));
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
            List<StorageClass> storageClasList = StorageClass.GetAll<StorageClass>().ToList();
            request.Keys.ForEach(key =>
            {
                resultList.Add(storageClasList.Any(x => x.Id == request.Type.Id)
                    ? ChangeType(new ChangeTypeParam(key, request.Type, request.PersistentOps))
                    : new ChangeTypeResultDto(false, key, "文件存储类型不支持，请输入1或者0"));
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
            return ToolCommon.GetResponse(() =>
            {
                new SetPermissParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(_aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket);
                var permiss = request.Permiss != null
                    ? Core.Tools.GetCannedAccessControl(request.Permiss)
                    : CannedAccessControlList.Default;
                client.SetObjectAcl(bucket, request.Key, permiss);
                return new OperateResultDto(true, "success");
            }, message => new OperateResultDto(false, message));
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
            return ToolCommon.GetResponse(() =>
            {
                new GetFilePermissParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                var zone = Core.Tools.GetZone(_aLiYunConfig, request.PersistentOps.Zone, () => ZoneEnum.HangZhou);
                var client = _aLiYunConfig.GetClient(zone);
                var bucket = Core.Tools.GetBucket(_aLiYunConfig, request.PersistentOps.Bucket);
                var ret = client.GetObjectAcl(bucket, request.Key);

                if (ret.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new FilePermissResultInfo(true, Core.Tools.GetPermiss(ret.ACL), "success");
                }

                return new FilePermissResultInfo(false, null, "文件不存在" + ret.ToString());
            }, message => new FilePermissResultInfo(false, null,
                message));
        }

        #endregion

        #endregion

        #region 抓取资源到空间（不建议上传大文件）

        /// <summary>
        /// 抓取资源到空间（不建议上传大文件）
        /// </summary>
        /// <param name="fetchFileParam"></param>
        /// <returns></returns>
        public FetchFileResultDto FetchFile(FetchFileParam fetchFileParam)
        {
            var ret = DownloadStream(new FileDownloadStreamParam(fetchFileParam.SourceFileKey,
                fetchFileParam.PersistentOps));
            if (ret.State)
            {
                var result = UploadStream(new UploadByStreamParam(fetchFileParam.Key, ret.FileStream,
                    new UploadPersistentOps()
                    {
                        Zone = fetchFileParam.PersistentOps.Zone,
                        Bucket = fetchFileParam.PersistentOps.Bucket,
                        Host = fetchFileParam.PersistentOps.Host,
                        IsUseHttps = fetchFileParam.PersistentOps.IsUseHttps,
                        UseCdnDomains = fetchFileParam.PersistentOps.UseCdnDomains,
                        ChunkUnit = fetchFileParam.PersistentOps.ChunkUnit,
                        MaxRetryTimes = fetchFileParam.PersistentOps.MaxRetryTimes,
                    }));
                return new FetchFileResultDto(result.State,result.Extend,result.Msg);
            }

            return new FetchFileResultDto(ret.State,ret.Extend,ret.Msg);
        }

        #endregion
    }
}
