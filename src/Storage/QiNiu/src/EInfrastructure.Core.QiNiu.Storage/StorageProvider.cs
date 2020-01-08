// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Reflection;
using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Config.StorageExtensions;
using EInfrastructure.Core.Config.StorageExtensions.Dto;
using EInfrastructure.Core.Config.StorageExtensions.Param;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.QiNiu.Storage.Config;
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
        public bool UploadStream(UploadByStreamParam param)
        {
            var uploadPersistentOps = GetUploadPersistentOps(param.UploadPersistentOps);
            string token = GetUploadCredentials(QiNiuConfig,
                new UploadPersistentOpsParam(param.Key, uploadPersistentOps));
            FormUploader target = new FormUploader(GetConfig(uploadPersistentOps));
            HttpResult result =
                target.UploadStream(param.Stream, param.Key, token, GetPutExtra(uploadPersistentOps));
            return result.Code == (int) HttpCode.OK;
        }

        #endregion

        #region 根据文件上传

        /// <summary>
        /// 根据文件上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool UploadFile(UploadByFormFileParam param)
        {
            var uploadPersistentOps = GetUploadPersistentOps(param.UploadPersistentOps);
            string token = base.GetUploadCredentials(QiNiuConfig,
                new UploadPersistentOpsParam(param.Key, uploadPersistentOps));
            FormUploader target = new FormUploader(GetConfig(uploadPersistentOps));
            if (param.File != null)
            {
                HttpResult result =
                    target.UploadStream(param.File.OpenReadStream(), param.Key, token,
                        GetPutExtra(uploadPersistentOps));
                return result.Code == (int) HttpCode.OK;
            }

            return false;
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
        public bool Exist(string key)
        {
            BucketManager bucketManager = new BucketManager(QiNiuConfig.GetMac(), base.GetConfig());
            StatResult statResult = bucketManager.Stat(QiNiuConfig.Bucket, key);
            return statResult.Code == (int) HttpCode.OK;
        }

        #endregion

        #region 获取文件信息

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="json">七牛云配置 QiNiuStorageConfig的序列化后的json</param>
        /// <returns></returns>
        public FileInfoDto Get(string key, string json = "")
        {
            BucketManager bucketManager = new BucketManager(QiNiuConfig.GetMac(), base.GetConfig());
            StatResult statRet = bucketManager.Stat(QiNiuConfig.Bucket, key);
            if (statRet.Code != (int) HttpCode.OK)
            {
                return new FileInfoDto()
                {
                    Success = false,
                    Msg = statRet.Text
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

            };
        }

        #endregion
    }
}
