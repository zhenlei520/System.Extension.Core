// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Dto;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Param;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Validator;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Validation.Common;
using Qiniu.Http;
using Qiniu.Storage;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 文件实现类
    /// </summary>
    public class StorageProvider : BaseStorageProvider, IStorageService, IPerRequest
    {
        /// <summary>
        /// 文件实现类
        /// </summary>
        public StorageProvider(QiNiuStorageConfig qiNiuConfig = null) : base(qiNiuConfig)
        {
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
        /// <returns></returns>
        public UploadResultDto UploadStream(UploadByStreamParam param)
        {
            var uploadPersistentOps = GetUploadPersistentOps(param.UploadPersistentOps);
            string token = GetUploadCredentials(QiNiuConfig,
                new UploadPersistentOpsParam(param.Key, uploadPersistentOps));
            FormUploader target = new FormUploader(GetConfig(uploadPersistentOps));
            HttpResult result =
                target.UploadStream(param.Stream, param.Key, token, GetPutExtra(uploadPersistentOps));
            bool res = result.Code == (int) HttpCode.OK;
            return new UploadResultDto(res, res ? "成功" : result.ToString());
        }

        #endregion

        #region 根据文件字节数组上传

        /// <summary>
        /// 根据文件字节数组上传
        /// </summary>
        /// <param name="param">文件流上传配置</param>
        /// <returns></returns>
        public UploadResultDto UploadByteArray(UploadByByteArrayParam param)
        {
            var uploadPersistentOps = GetUploadPersistentOps(param.UploadPersistentOps);
            string token = GetUploadCredentials(QiNiuConfig,
                new UploadPersistentOpsParam(param.Key, uploadPersistentOps));
            FormUploader target = new FormUploader(GetConfig(uploadPersistentOps));
            HttpResult result =
                target.UploadData(param.ByteArray, param.Key, token, GetPutExtra(uploadPersistentOps));
            bool res = result.Code == (int) HttpCode.OK;
            return new UploadResultDto(res, res ? "成功" : result.ToString());
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
            FormUploader target = new FormUploader(GetConfig(uploadPersistentOps));
            HttpResult result = null;
            if (param.Stream == null)
            {
                result =
                    target.UploadStream(param.Stream, param.Key, param.Token, GetPutExtra(uploadPersistentOps));

                bool res = result.Code == (int) HttpCode.OK;
                return new UploadResultDto(res, res ? "成功" : result.ToString());
            }

            if (param.ByteArray == null)
            {
                result =
                    target.UploadData(param.ByteArray, param.Key, param.Token, GetPutExtra(uploadPersistentOps));

                bool res = result.Code == (int) HttpCode.OK;
                return new UploadResultDto(res, res ? "成功" : result.ToString());
            }

            return new UploadResultDto(false, "不支持的上传方式");
        }

        #endregion

        #region 得到上传文件策略信息

        /// <summary>
        /// 得到上传文件策略信息
        /// </summary>
        /// <param name="opsParam">上传信息</param>
        public string GetUploadCredentials(UploadPersistentOpsParam opsParam)
        {
            return base.GetUploadCredentials(QiNiuConfig, opsParam);
        }

        /// <summary>
        /// 得到上传文件策略信息
        /// </summary>
        /// <param name="opsParam">上传信息</param>
        /// <param name="func"></param>
        public string GetUploadCredentials(UploadPersistentOpsParam opsParam, Func<string> func)
        {
            return base.GetUploadCredentials(QiNiuConfig, opsParam,
                (putPolicy) => { putPolicy.CallbackBody = func?.Invoke(); });
        }

        #endregion

        #region 检查文件是否存在

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="key">文件key</param>
        /// <returns></returns>
        public OperateResultDto Exist(string key)
        {
            var res = Get(key);
            return new OperateResultDto(res.Success, res.Msg);
        }

        #endregion

        #region 获取文件信息

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="key">文件key</param>
        /// <returns></returns>
        public FileInfoDto Get(string key)
        {
            StatResult statRet = GetBucketManager().Stat(QiNiuConfig.Bucket, key);
            if (statRet.Code != (int) HttpCode.OK)
            {
                return new FileInfoDto()
                {
                    Success = false,
                    Msg = statRet.ToString()
                };
            }

            return new FileInfoDto()
            {
                Size = statRet.Result.Fsize,
                Hash = statRet.Result.Hash,
                MimeType = statRet.Result.MimeType,
                PutTime = statRet.Result.PutTime,
                FileType = statRet.Result.FileType,
                Success = true,
                Host = QiNiuConfig.Host,
                Path = key,
                Msg = "成功"
            };
        }

        /// <summary>
        /// 获取文件信息集合
        /// </summary>
        /// <param name="keyList">文件key集合</param>
        /// <returns></returns>
        public IEnumerable<FileInfoDto> GetList(IEnumerable<string> keyList)
        {
            var enumerable = keyList as string[] ?? keyList.ToArray();

            List<FileInfoDto> res = new List<FileInfoDto>();
            enumerable.ToList().ListPager((list) => { res.AddRange(GetMulti(list)); }, 1000, 1);
            return res;
        }

        /// <summary>
        /// 获取文件信息集合
        /// </summary>
        /// <param name="keyList">文件key集合</param>
        /// <returns></returns>
        private IEnumerable<FileInfoDto> GetMulti(IEnumerable<string> keyList)
        {
            var enumerable = keyList as string[] ?? keyList.ToArray();
            List<string> ops = enumerable.Select(key => GetBucketManager().StatOp(QiNiuConfig.Bucket, key)).ToList();
            BatchResult ret = GetBucketManager().Batch(ops);

            var index = 0;
            foreach (var item in ret.Result)
            {
                index++;
                if (item.Code == (int) HttpCode.OK)
                {
                    yield return new FileInfoDto()
                    {
                        Size = item.Data.Fsize,
                        Hash = item.Data.Hash,
                        MimeType = item.Data.MimeType,
                        PutTime = item.Data.PutTime,
                        FileType = item.Data.FileType,
                        Success = true,
                        Host = QiNiuConfig.Host,
                        Path = enumerable.ToList()[index - 1],
                        Msg = "成功"
                    };
                }
                else
                {
                    yield return new FileInfoDto()
                    {
                        Success = false,
                        Msg = item.Data.Error,
                        Host = QiNiuConfig.Host,
                        Path = enumerable.ToList()[index - 1]
                    };
                }
            }
        }

        #endregion

        #region 删除文件

        /// <summary>
        /// 根据文件key删除
        /// </summary>
        /// <param name="key">文件key</param>
        /// <returns></returns>
        public DeleteResultDto Del(string key)
        {
            HttpResult deleteRet = GetBucketManager().Delete(QiNiuConfig.Bucket, key);
            var res = deleteRet.Code == (int) HttpCode.OK;
            return new DeleteResultDto(res, key, res ? "删除成功" : deleteRet.ToString());
        }

        /// <summary>
        /// 根据文件key集合删除
        /// </summary>
        /// <param name="keyList">文件key集合</param>
        /// <returns></returns>
        public IEnumerable<DeleteResultDto> DelList(IEnumerable<string> keyList)
        {
            var enumerable = keyList as string[] ?? keyList.ToArray();

            List<DeleteResultDto> res = new List<DeleteResultDto>();
            enumerable.ToList().ListPager((list) => { res.AddRange(DelMulti(list)); }, 1000, 1);
            return res;
        }

        /// <summary>
        ///根据文件key集合删除
        /// </summary>
        /// <param name="keyList">文件key集合</param>
        /// <returns></returns>
        private IEnumerable<DeleteResultDto> DelMulti(IEnumerable<string> keyList)
        {
            var enumerable = keyList as string[] ?? keyList.ToArray();
            List<string> ops = enumerable.Select(key => GetBucketManager().DeleteOp(QiNiuConfig.Bucket, key)).ToList();
            BatchResult ret = GetBucketManager().Batch(ops);
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
            HttpResult copyRet = GetBucketManager().Copy(copyFileParam.SourceBucket, copyFileParam.SourceKey,
                copyFileParam.OptBucket, copyFileParam.OptKey, copyFileParam.IsForce);
            var res = copyRet.Code == (int) HttpCode.OK;
            return new CopyFileResultDto(res, copyFileParam.FileId, res ? "复制成功" : copyRet.Text);
        }

        /// <summary>
        /// 复制文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="copyFileParam">复制到新空间的参数</param>
        /// <returns></returns>
        public IEnumerable<CopyFileResultDto> CopyToList(ICollection<CopyFileParam> copyFileParam)
        {
            List<CopyFileResultDto> res = new List<CopyFileResultDto>();
            copyFileParam.ToList().ListPager(list => { res.AddRange(CopyToMulti(list)); }, 1000, 1);
            return res;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="copyFileParam">复制到新空间的参数</param>
        /// <returns></returns>
        private IEnumerable<CopyFileResultDto> CopyToMulti(ICollection<CopyFileParam> copyFileParam)
        {
            copyFileParam.ToList().ForEach(item =>
            {
                new CopyFileParamValidator().Validate(item).Check(HttpStatus.Err.Name);
            });

            List<string> ops = copyFileParam.Select(x =>
                GetBucketManager().CopyOp(x.SourceBucket, x.SourceKey, x.OptBucket, x.OptKey, x.IsForce)).ToList();
            BatchResult ret = GetBucketManager().Batch(ops);
            var index = 0;
            foreach (BatchInfo info in ret.Result)
            {
                index++;
                if (info.Code == (int) HttpCode.OK)
                {
                    yield return new CopyFileResultDto(true, copyFileParam.ToList()[index - 1].FileId,
                        "复制成功");
                }
                else
                {
                    yield return new CopyFileResultDto(false, copyFileParam.ToList()[index - 1].FileId,
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
            HttpResult copyRet = GetBucketManager().Move(moveFileParam.SourceBucket, moveFileParam.SourceKey,
                moveFileParam.OptBucket, moveFileParam.OptKey, moveFileParam.IsForce);
            var res = copyRet.Code == (int) HttpCode.OK;
            return new MoveFileResultDto(res, moveFileParam.FileId, res ? "移动成功" : copyRet.Text);
        }

        /// <summary>
        /// 移动文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="moveFileParamList"></param>
        /// <returns></returns>
        public IEnumerable<MoveFileResultDto> MoveList(List<MoveFileParam> moveFileParamList)
        {
            List<MoveFileResultDto> res = new List<MoveFileResultDto>();
            moveFileParamList.ToList().ListPager(list => { res.AddRange(MoveMulti(list)); }, 1000, 1);
            return res;
        }

        /// <summary>
        /// 移动文件（两个文件需要在同一账号下）
        /// </summary>
        /// <param name="moveFileParamList"></param>
        /// <returns></returns>
        private IEnumerable<MoveFileResultDto> MoveMulti(List<MoveFileParam> moveFileParamList)
        {
            moveFileParamList.ToList().ForEach(item =>
            {
                new MoveFileParamValidator().Validate(item).Check(HttpStatus.Err.Name);
            });

            List<string> ops = moveFileParamList.Select(x =>
                GetBucketManager().MoveOp(x.SourceBucket, x.SourceKey, x.OptBucket, x.OptKey, x.IsForce)).ToList();
            BatchResult ret = GetBucketManager().Batch(ops);
            var index = 0;
            foreach (BatchInfo info in ret.Result)
            {
                index++;
                if (info.Code == (int) HttpCode.OK)
                {
                    yield return new MoveFileResultDto(true, moveFileParamList.ToList()[index - 1].FileId,
                        "复制成功");
                }
                else
                {
                    yield return new MoveFileResultDto(false, moveFileParamList.ToList()[index - 1].FileId,
                        info.Data.Error);
                }
            }
        }

        #endregion
    }
}
