// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.Http;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Validator.Storage;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Validation.Common;
using Microsoft.Extensions.Logging;
using Qiniu.Http;
using Qiniu.Storage;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 文件实现类
    /// </summary>
    public class StorageProvider : BaseStorageProvider, IStorageProvider
    {
        private readonly ILogger _logger;

        /// <summary>
        /// 文件实现类
        /// </summary>
        public StorageProvider(ILogger logger, QiNiuStorageConfig qiNiuConfig) : base(qiNiuConfig)
        {
            this._logger = logger;
        }

        /// <summary>
        /// 文件实现类
        /// </summary>
        public StorageProvider(ILogger<StorageProvider> logger, QiNiuStorageConfig qiNiuConfig) : base(qiNiuConfig)
        {
            this._logger = logger;
        }

        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
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
        /// <param name="isResume">是否允许续传（续传采用非表单提交方式）</param>
        /// <returns></returns>
        public UploadResultDto UploadStream(UploadByStreamParam param, bool isResume = false)
        {
            var uploadPersistentOps = GetUploadPersistentOps(param.UploadPersistentOps);
            string token = GetUploadCredentials(QiNiuConfig,
                new UploadPersistentOpsParam(param.Key, uploadPersistentOps));
            if (isResume)
            {
                ResumableUploader target =
                    new ResumableUploader(Core.Tools.GetConfig(QiNiuConfig, uploadPersistentOps));
                HttpResult result =
                    target.UploadStream(param.Stream, param.Key, token, GetPutExtra(uploadPersistentOps));
                bool res = result.Code == (int) HttpCode.OK;
                return new UploadResultDto(res, result, res ? "成功" : result.ToString());
            }
            else
            {
                FormUploader target = new FormUploader(Core.Tools.GetConfig(QiNiuConfig, uploadPersistentOps));
                HttpResult result =
                    target.UploadStream(param.Stream, param.Key, token, GetPutExtra(uploadPersistentOps));
                bool res = result.Code == (int) HttpCode.OK;
                return new UploadResultDto(res, result, res ? "成功" : result.ToString());
            }
        }

        #endregion

        #region 根据文件字节数组上传

        /// <summary>
        /// 根据文件字节数组上传
        /// </summary>
        /// <param name="param">文件流上传配置</param>
        /// <param name="isResume">是否允许续传（续传采用非表单提交方式）</param>
        /// <returns></returns>
        public UploadResultDto UploadByteArray(UploadByByteArrayParam param, bool isResume = false)
        {
            var uploadPersistentOps = GetUploadPersistentOps(param.UploadPersistentOps);
            string token = GetUploadCredentials(QiNiuConfig,
                new UploadPersistentOpsParam(param.Key, uploadPersistentOps));
            if (isResume)
            {
                ResumableUploader target =
                    new ResumableUploader(Core.Tools.GetConfig(QiNiuConfig, uploadPersistentOps));
                HttpResult result =
                    target.UploadStream(param.ByteArray.ConvertToStream(), param.Key, token,
                        GetPutExtra(uploadPersistentOps));
                bool res = result.Code == (int) HttpCode.OK;
                return new UploadResultDto(res, result, res ? "成功" : result.ToString());
            }
            else
            {
                FormUploader target = new FormUploader(Core.Tools.GetConfig(QiNiuConfig, uploadPersistentOps));
                HttpResult result =
                    target.UploadData(param.ByteArray, param.Key, token, GetPutExtra(uploadPersistentOps));
                bool res = result.Code == (int) HttpCode.OK;
                return new UploadResultDto(res, result, res ? "成功" : result.ToString());
            }
        }

        #endregion

        #region 根据文件token上传

        /// <summary>
        /// 根据文件流以及文件字节数组上传
        /// </summary>
        /// <param name="param">文件流上传配置</param>
        /// <returns></returns>
        public UploadResultDto UploadByToken(UploadByTokenParam param)
        {
            var uploadPersistentOps = GetUploadPersistentOps(param.UploadPersistentOps);
            FormUploader target = new FormUploader(Core.Tools.GetConfig(QiNiuConfig, uploadPersistentOps));
            HttpResult result = null;
            if (param.Stream != null)
            {
                result =
                    target.UploadStream(param.Stream, param.Key, param.Token, GetPutExtra(uploadPersistentOps));

                bool res = result.Code == (int) HttpCode.OK;
                return new UploadResultDto(res, result, res ? "成功" : result.ToString());
            }

            if (param.ByteArray != null)
            {
                result =
                    target.UploadData(param.ByteArray, param.Key, param.Token, GetPutExtra(uploadPersistentOps));

                bool res = result.Code == (int) HttpCode.OK;
                return new UploadResultDto(res, result, res ? "成功" : result.ToString());
            }

            return new UploadResultDto(false, result, "不支持的上传方式");
        }

        #endregion

        #region 得到凭证

        #region 得到上传文件凭证

        /// <summary>
        /// 得到上传文件凭证
        /// </summary>
        /// <param name="opsParam">上传信息</param>
        public string GetUploadCredentials(UploadPersistentOpsParam opsParam)
        {
            new UploadPersistentOpsParamValidator().Validate(opsParam).Check(HttpStatus.Err.Name);
            var uploadPersistentOps = GetUploadPersistentOps(opsParam.UploadPersistentOps);
            return base.GetUploadCredentials(QiNiuConfig,
                new UploadPersistentOpsParam(opsParam.Key, uploadPersistentOps));
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
            new GetManageTokenParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            return GetAuth().CreateManageToken(request.Url);
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
            new ExistParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            var res = Get(new GetFileParam(request.Key, request.PersistentOps));
            return new OperateResultDto(res.State, res.Msg);
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
            new ListFileFilterValidator().Validate(filter).Check(HttpStatus.Err.Name);
            var listRet = base.GetBucketManager(filter.PersistentOps).ListFiles(
                Core.Tools.GetBucket(QiNiuConfig, filter.PersistentOps.Bucket), filter.Prefix, filter.Marker,
                filter.PageSize,
                filter.Delimiter);
            if (listRet.Code == (int) HttpCode.OK)
            {
                return new ListFileItemResultDto(true, "success")
                {
                    CommonPrefixes = listRet.Result.CommonPrefixes,
                    Marker = listRet.Result.Marker,
                    Items = listRet.Result.Items.Select(x => new FileInfoDto(true, "success")
                    {
                        Key = x.Key,
                        Hash = filter.IsShowHash ? x.Hash : "",
                        Size = x.Fsize,
                        PutTime = x.PutTime,
                        MimeType = x.MimeType,
                        FileType = Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass
                            .FromValue<EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass>(
                                x.FileType),
                    }).ToList()
                };
            }

            return new ListFileItemResultDto(false, "lose");
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
            new GetFileParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            StatResult statRet = GetBucketManager(request.PersistentOps)
                .Stat(Core.Tools.GetBucket(QiNiuConfig, request.PersistentOps.Bucket), request.Key);
            if (statRet.Code != (int) HttpCode.OK)
            {
                return new FileInfoDto(false, statRet.ToString());
            }

            return new FileInfoDto(true, "success")
            {
                Size = statRet.Result.Fsize,
                Hash = statRet.Result.Hash,
                MimeType = statRet.Result.MimeType,
                PutTime = statRet.Result.PutTime,
                FileType = Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass
                    .FromValue<EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass>(
                        statRet.Result.FileType),
                Key = request.Key,
            };
        }

        /// <summary>
        /// 获取文件信息集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<FileInfoDto> GetList(GetFileRangeParam request)
        {
            new GetFileRangeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            List<FileInfoDto> res = new List<FileInfoDto>();
            request.Keys.ToList()
                .ListPager((list) => { res.AddRange(GetMulti(list.ToArray(), request.PersistentOps)); }, 1000, 1);
            return res;
        }

        /// <summary>
        /// 获取文件信息集合
        /// </summary>
        /// <param name="keyList">文件key集合</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        private IEnumerable<FileInfoDto> GetMulti(string[] keyList, BasePersistentOps persistentOps)
        {
            List<string> ops = keyList.Select(key =>
                GetBucketManager(persistentOps)
                    .StatOp(Core.Tools.GetBucket(QiNiuConfig, persistentOps.Bucket), key)).ToList();
            BatchResult ret = GetBucketManager(persistentOps).Batch(ops);

            var index = 0;
            foreach (var item in ret.Result)
            {
                index++;
                if (item.Code == (int) HttpCode.OK)
                {
                    yield return new FileInfoDto(true, "success")
                    {
                        Size = item.Data.Fsize,
                        Hash = item.Data.Hash,
                        MimeType = item.Data.MimeType,
                        PutTime = item.Data.PutTime,
                        FileType = Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass
                            .FromValue<EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass>(
                                item.Data.FileType),
                        Key = keyList[index - 1]
                    };
                }
                else
                {
                    yield return new FileInfoDto(false, item.Data.Error)
                    {
                        Key = keyList[index - 1]
                    };
                }
            }
        }

        #endregion

        #region 删除文件

        /// <summary>
        /// 根据文件key删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DeleteResultDto Remove(RemoveParam request)
        {
            new RemoveParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            HttpResult deleteRet = GetBucketManager(request.PersistentOps)
                .Delete(Core.Tools.GetBucket(QiNiuConfig, request.PersistentOps.Bucket), request.Key);
            var res = deleteRet.Code == (int) HttpCode.OK;
            return new DeleteResultDto(res, request.Key, res ? "删除成功" : deleteRet.ToString());
        }

        /// <summary>
        /// 根据文件key集合删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<DeleteResultDto> RemoveRange(RemoveRangeParam request)
        {
            new RemoveRangeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            List<DeleteResultDto> res = new List<DeleteResultDto>();
            request.Keys.ListPager((list) => { res.AddRange(DelMulti(list, request.PersistentOps)); }, 1000, 1);
            return res;
        }

        ///  <summary>
        /// 根据文件key集合删除
        ///  </summary>
        ///  <param name="keyList">文件key集合</param>
        ///  <param name="persistentOps">策略</param>
        ///  <returns></returns>
        private IEnumerable<DeleteResultDto> DelMulti(IEnumerable<string> keyList, BasePersistentOps persistentOps)
        {
            var enumerable = keyList as string[] ?? keyList.ToArray();
            List<string> ops = enumerable.Select(key =>
                    GetBucketManager(persistentOps)
                        .DeleteOp(Core.Tools.GetBucket(QiNiuConfig, persistentOps.Bucket), key))
                .ToList();
            BatchResult ret = GetBucketManager(persistentOps).Batch(ops);
            var index = 0;
            foreach (var item in ret.Result)
            {
                index++;
                if (item.Code == (int) HttpCode.OK)
                {
                    yield return new DeleteResultDto(true, enumerable.ToList()[index - 1], "删除成功");
                }
                else
                {
                    yield return new DeleteResultDto(false, enumerable.ToList()[index - 1], item.Data.Error);
                }
            }
        }

        #endregion

        #region 批量复制文件

        /// <summary>
        /// 复制文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="copyFileParam">复制到新空间的参数</param>
        /// <returns></returns>
        public CopyFileResultDto CopyTo(CopyFileParam copyFileParam)
        {
            new CopyFileParamValidator().Validate(copyFileParam).Check(HttpStatus.Err.Name);
            HttpResult copyRet = GetBucketManager(copyFileParam.PersistentOps).Copy(
                Core.Tools.GetBucket(QiNiuConfig, copyFileParam.PersistentOps.Bucket), copyFileParam.SourceKey,
                Core.Tools.GetBucket(QiNiuConfig, copyFileParam.PersistentOps.Bucket, copyFileParam.OptBucket),
                copyFileParam.OptKey, copyFileParam.IsForce);
            var res = copyRet.Code == (int) HttpCode.OK;
            return new CopyFileResultDto(res, copyFileParam.SourceKey, res ? "复制成功" : copyRet.Text);
        }

        /// <summary>
        /// 复制文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<CopyFileResultDto> CopyRangeTo(CopyFileRangeParam request)
        {
            new CopyFileRangeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            List<CopyFileResultDto> res = new List<CopyFileResultDto>();
            request.CopyFiles.ToList()
                .ListPager(list => { res.AddRange(CopyToMulti(list, request.PersistentOps)); }, 1000, 1);
            return res;
        }

        ///  <summary>
        /// 复制到新空间的参数
        ///  </summary>
        ///  <param name="copyFileParam">复制到新空间的参数</param>
        ///  <param name="persistentOps">策略</param>
        ///  <returns></returns>
        private IEnumerable<CopyFileResultDto> CopyToMulti(ICollection<CopyFileRangeParam.CopyFileParam> copyFileParam,
            BasePersistentOps persistentOps)
        {
            List<string> ops = copyFileParam.Select(x =>
                    GetBucketManager(persistentOps).CopyOp(Core.Tools.GetBucket(QiNiuConfig, persistentOps.Bucket),
                        x.SourceKey,
                        Core.Tools.GetBucket(QiNiuConfig, persistentOps.Bucket, x.OptBucket), x.OptKey, x.IsForce))
                .ToList();
            BatchResult ret = GetBucketManager(persistentOps).Batch(ops);
            var index = 0;
            foreach (BatchInfo info in ret.Result)
            {
                index++;
                if (info.Code == (int) HttpCode.OK)
                {
                    yield return new CopyFileResultDto(true, copyFileParam.ToList()[index - 1].SourceKey,
                        "复制成功");
                }
                else
                {
                    yield return new CopyFileResultDto(false, copyFileParam.ToList()[index - 1].SourceKey,
                        info.Data.Error);
                }
            }
        }

        #endregion

        #region 批量移动文件（两个文件需要在同一账号下）

        /// <summary>
        /// 移动文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="moveFileParam"></param>
        /// <returns></returns>
        public MoveFileResultDto Move(MoveFileParam moveFileParam)
        {
            new MoveFileParamValidator().Validate(moveFileParam).Check(HttpStatus.Err.Name);
            HttpResult copyRet = GetBucketManager(moveFileParam.PersistentOps).Move(
                Core.Tools.GetBucket(QiNiuConfig, moveFileParam.PersistentOps.Bucket), moveFileParam.SourceKey,
                moveFileParam.OptBucket, moveFileParam.OptKey, moveFileParam.IsForce);
            var res = copyRet.Code == (int) HttpCode.OK;
            return new MoveFileResultDto(res, moveFileParam.FileId, res ? "移动成功" : copyRet.Text);
        }

        /// <summary>
        /// 移动文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<MoveFileResultDto> MoveRange(MoveFileRangeParam request)
        {
            new MoveFileParamRangeValidator().Validate(request).Check(HttpStatus.Err.Name);
            List<MoveFileResultDto> res = new List<MoveFileResultDto>();
            request.MoveFiles.ToList()
                .ListPager(list => { res.AddRange(MoveMulti(list, request.PersistentOps)); }, 1000, 1);
            return res;
        }

        /// <summary>
        /// 移动文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="moveFileParamList"></param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        private IEnumerable<MoveFileResultDto> MoveMulti(List<MoveFileRangeParam.MoveFileParam> moveFileParamList,
            BasePersistentOps persistentOps)
        {
            var bucketManager = GetBucketManager(persistentOps);
            List<string> ops = moveFileParamList.Select(x =>
                bucketManager.MoveOp(Core.Tools.GetBucket(QiNiuConfig, persistentOps.Bucket), x.SourceKey,
                    x.OptBucket, x.OptKey, x.IsForce)).ToList();
            BatchResult ret = bucketManager.Batch(ops);
            var index = 0;
            foreach (BatchInfo info in ret.Result)
            {
                index++;
                if (info.Code == (int) HttpCode.OK)
                {
                    yield return new MoveFileResultDto(true, moveFileParamList.ToList()[index - 1].SourceKey,
                        "复制成功");
                }
                else
                {
                    yield return new MoveFileResultDto(false, moveFileParamList.ToList()[index - 1].SourceKey,
                        info.Data.Error);
                }
            }
        }

        #endregion

        #region 得到地址

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
                var url = DownloadManager.CreatePrivateUrl(QiNiuConfig.GetMac(),
                    Core.Tools.GetHost(QiNiuConfig, request.PersistentOps.Host), request.Key, request.Expire);

                if (string.IsNullOrEmpty(url))
                {
                    if (request.Expire <= 0)
                    {
                        request.Expire = 3600;
                    }

                    url = DownloadManager.CreatePublishUrl(
                        Core.Tools.GetHost(QiNiuConfig, request.PersistentOps.Host),
                        request.Key);
                }

                return new GetVisitUrlResultDto(url, "success");
            }
            catch (BusinessException<string>ex)
            {
                this._logger?.LogError(ex.ExtractAllStackTrace());
                return new GetVisitUrlResultDto(ex.Message);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ExtractAllStackTrace());
                return new GetVisitUrlResultDto(Core.Tools.GetMessage(ex));
            }
        }

        #endregion

        #region 下载文件

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DownloadResultDto Download(FileDownloadParam request)
        {
            new FileDownloadParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            var ret = DownloadManager.Download(request.Url, request.SavePath);
            var res = ret.Code == (int) HttpCode.OK;
            return new DownloadResultDto(res, ret.Text, ret);
        }

        /// <summary>
        /// 下载文件流
        /// </summary>
        /// <param name="request"></param>
        public DownloadStreamResultDto DownloadStream(FileDownloadStreamParam request)
        {
            try
            {
                new FileDownloadStreamParamValidator().Validate(request).Check(HttpStatus.Err.Name);
                Uri uri = new Uri(request.Url);
                string host = $"{uri.Scheme}://{uri.Host}";
                Stream stream = new HttpClient(host).GetStream(request.Url.Replace(host, ""));
                return new DownloadStreamResultDto(true, "success", stream, null);
            }
            catch (BusinessException<string> ex)
            {
                this._logger?.LogError(ex.ExtractAllStackTrace());
                return new DownloadStreamResultDto(ex.Message);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(ex.ExtractAllStackTrace());
                return new DownloadStreamResultDto(Core.Tools.GetMessage(ex));
            }
        }

        #endregion

        #region 设置生存时间

        /// <summary>
        /// 设置生存时间（超时会自动删除）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ExpireResultDto SetExpire(SetExpireParam request)
        {
            new SetExpireParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            var expireRet = base.GetBucketManager(request.PersistentOps)
                .DeleteAfterDays(Core.Tools.GetBucket(QiNiuConfig, request.PersistentOps.Bucket), request.Key,
                    request.Expire);
            if (expireRet.Code != (int) HttpCode.OK)
            {
                return new ExpireResultDto(false, request.Key, "lose");
            }

            return new ExpireResultDto(true, request.Key, "success");
        }

        /// <summary>
        /// 批量设置生存时间（超时会自动删除）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ExpireResultDto> SetExpireRange(SetExpireRangeParam request)
        {
            new SetExpireRangeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            List<ExpireResultDto> expireResult = new List<ExpireResultDto>();
            request.Keys.Distinct().ToList()
                .ListPager(
                    (list) =>
                    {
                        expireResult.AddRange(SetExpireMulti(list.ToArray(), request.Expire, request.PersistentOps));
                    },
                    1000,
                    1);
            return expireResult;
        }

        /// <summary>
        /// 设置生存时间
        /// </summary>
        /// <param name="keys">文件key集合</param>
        /// <param name="expire">过期时间 单位：day</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        private IEnumerable<ExpireResultDto> SetExpireMulti(string[] keys, int expire, BasePersistentOps persistentOps)
        {
            var bucketManager = base.GetBucketManager(persistentOps);
            List<string> ops = new List<string>();
            foreach (string key in keys)
            {
                string op = bucketManager.DeleteAfterDaysOp(
                    Core.Tools.GetBucket(QiNiuConfig, persistentOps.Bucket), key, expire);
                ops.Add(op);
            }

            BatchResult ret = bucketManager.Batch(ops);
            var index = 0;
            foreach (BatchInfo info in ret.Result)
            {
                index++;
                if (info.Code == (int) HttpCode.OK)
                {
                    yield return new ExpireResultDto(true, keys.ToList()[index - 1], "success");
                }
                else
                {
                    yield return new ExpireResultDto(false, keys.ToList()[index - 1], "lose");
                }
            }
        }

        #endregion

        #region 修改文件MimeType

        /// <summary>
        /// 修改文件MimeType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ChangeMimeResultDto ChangeMime(ChangeMimeParam request)
        {
            new ChangeMimeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            var ret = base.GetBucketManager(request.PersistentOps)
                .ChangeMime(Core.Tools.GetBucket(QiNiuConfig, request.PersistentOps.Bucket), request.Key,
                    request.MimeType);
            if (ret.Code != (int) HttpCode.OK)
            {
                return new ChangeMimeResultDto(false, request.Key, ret.Text);
            }

            return new ChangeMimeResultDto(true, request.Key, "success");
        }

        /// <summary>
        /// 批量更改文件mime
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ChangeMimeResultDto> ChangeMimeRange(ChangeMimeRangeParam request)
        {
            new ChangeMimeRangeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            List<ChangeMimeResultDto> ret = new List<ChangeMimeResultDto>();
            request.Keys.Distinct().ToList()
                .ListPager(
                    (list) =>
                    {
                        ret.AddRange(ChangeMimeMulti(list.ToArray(), request.MimeType, request.PersistentOps));
                    }, 1000, 1);
            return ret;
        }

        /// <summary>
        /// 批量更改mimeType
        /// </summary>
        /// <param name="keys">文件key</param>
        /// <param name="mime">文件mime</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        private IEnumerable<ChangeMimeResultDto> ChangeMimeMulti(string[] keys, string mime,
            BasePersistentOps persistentOps)
        {
            var bucketManager = base.GetBucketManager(persistentOps);
            List<string> ops = new List<string>();
            foreach (string key in keys)
            {
                string op = bucketManager.ChangeMimeOp(Core.Tools.GetBucket(QiNiuConfig, persistentOps.Bucket),
                    key, mime);
                ops.Add(op);
            }

            BatchResult ret = bucketManager.Batch(ops);
            var index = 0;
            foreach (BatchInfo info in ret.Result)
            {
                index++;
                if (info.Code == (int) HttpCode.OK)
                {
                    yield return new ChangeMimeResultDto(true, keys.ToList()[index - 1], "success");
                }
                else
                {
                    yield return new ChangeMimeResultDto(false, keys.ToList()[index - 1], "lose");
                }
            }
        }

        #endregion

        #region 修改文件的存储类型

        /// <summary>
        /// 修改文件存储类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ChangeTypeResultDto ChangeType(ChangeTypeParam request)
        {
            new ChangeTypeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            HttpResult ret = base.GetBucketManager(request.PersistentOps)
                .ChangeType(Core.Tools.GetBucket(QiNiuConfig, request.PersistentOps.Bucket), request.Key,
                    request.Type.Id);
            if (ret.Code == (int) HttpCode.OK)
            {
                return new ChangeTypeResultDto(true, request.Key, "success");
            }

            return new ChangeTypeResultDto(false, request.Key, ret.Text);
        }

        /// <summary>
        /// 批量更改文件类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ChangeTypeResultDto> ChangeTypeRange(ChangeTypeRangeParam request)
        {
            new ChangeTypeRangeParamValidator().Validate(request).Check(HttpStatus.Err.Name);
            List<ChangeTypeResultDto> ret = new List<ChangeTypeResultDto>();
            request.Keys.Distinct().ToList()
                .ListPager(
                    (list) =>
                    {
                        ret.AddRange(ChangeTypeMulti(list.ToArray(), request.Type.Id, request.PersistentOps));
                    },
                    1000, 1);
            return ret;
        }

        /// <summary>
        /// 批量更改文件Type
        /// </summary>
        /// <param name="keys">文件key</param>
        /// <param name="type">0表示普通存储，1表示低频存储</param>
        /// <param name="persistentOps">策略</param>
        /// <returns></returns>
        private IEnumerable<ChangeTypeResultDto> ChangeTypeMulti(string[] keys, int type,
            BasePersistentOps persistentOps)
        {
            var bucketManager = base.GetBucketManager(persistentOps);
            List<string> ops = new List<string>();
            foreach (string key in keys)
            {
                string op = bucketManager.ChangeTypeOp(Core.Tools.GetBucket(QiNiuConfig, persistentOps.Bucket),
                    key, type);
                ops.Add(op);
            }

            BatchResult ret = bucketManager.Batch(ops);
            var index = 0;
            foreach (BatchInfo info in ret.Result)
            {
                index++;
                if (info.Code == (int) HttpCode.OK)
                {
                    yield return new ChangeTypeResultDto(true, keys.ToList()[index - 1], "success");
                }
                else
                {
                    yield return new ChangeTypeResultDto(false, keys.ToList()[index - 1], "lose");
                }
            }
        }

        #endregion

        #region 文件权限

        #region 设置文件权限

        /// <summary>
        /// 设置文件权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OperateResultDto SetPermiss(SetPermissParam request)
        {
            return new OperateResultDto(false, "不支持设置文件权限");
        }

        #endregion

        #region 获取文件的访问权限

        /// <summary>
        /// 获取文件的访问权限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public FilePermissResultInfo GetPermiss(GetFilePermissParam request)
        {
            return new FilePermissResultInfo(false, null, "不支持获取文件权限");
        }

        #endregion

        #endregion

        #region 抓取资源到空间

        /// <summary>
        /// 抓取资源到空间
        /// </summary>
        /// <param name="fetchFileParam">资源信息</param>
        /// <returns></returns>
        public FetchFileResultDto FetchFile(FetchFileParam fetchFileParam)
        {
            FetchResult ret = GetBucketManager(fetchFileParam.PersistentOps)
                .Fetch(fetchFileParam.SourceFileKey,
                    Core.Tools.GetBucket(QiNiuConfig, fetchFileParam.PersistentOps.Bucket), fetchFileParam.Key);
            switch (ret.Code)
            {
                case (int) HttpCode.OK:
                case (int) HttpCode.CALLBACK_FAILED:
                    return new FetchFileResultDto(true, null, "success");
                default:
                    return new FetchFileResultDto(false, ret, ret.ToString());
            }
        }

        #endregion
    }
}
