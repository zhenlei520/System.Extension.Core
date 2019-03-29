// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Interface.Log;
using EInfrastructure.Core.Interface.Storage.Config;
using EInfrastructure.Core.Interface.Storage.Enum;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Validator;
using EInfrastructure.Core.Validation.Common;
using Qiniu.Storage;
using Qiniu.Util;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 基类存储实现类
    /// </summary>
    public class BaseStorageProvider
    {
        protected readonly ILogService _logService;

        /// <summary>
        /// 
        /// </summary>
        public BaseStorageProvider(ILogService logService, QiNiuStorageConfig qiNiuConfig)
        {
            _logService = logService;
            
            QiNiuConfig = qiNiuConfig;

            Mac = new Mac(QiNiuConfig.AccessKey, QiNiuConfig.SecretKey);

            #region 上传策略

            PutPolicy = new PutPolicy();

            #region 上传成功后通知

            if (!string.IsNullOrEmpty(QiNiuConfig.CallbackBody))
            {
                PutPolicy.PersistentNotifyUrl = QiNiuConfig.PersistentNotifyUrl;
                PutPolicy.CallbackBody = QiNiuConfig.CallbackBody;
                PutPolicy.CallbackBodyType = QiNiuConfig.CallbackBodyType.GetDescription();
                PutPolicy.CallbackUrl = QiNiuConfig.CallbackUrl;
            }

            if (!string.IsNullOrEmpty(QiNiuConfig.PersistentPipeline))
            {
                PutPolicy.PersistentPipeline = QiNiuConfig.PersistentPipeline;
            }

            #endregion

            new QiNiuConfigValidator().Validate(qiNiuConfig).Check();

            #endregion
        }

        /// <summary>
        /// 存储配置文件
        /// </summary>
        internal QiNiuStorageConfig QiNiuConfig;

        /// <summary>
        /// 
        /// </summary>
        internal Mac Mac;

        /// <summary>
        /// 上传策略
        /// </summary>
        internal PutPolicy PutPolicy = null;

        #region 得到上传策略

        #region 设置上传策略

        /// <summary>
        /// 设置上传策略
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isAllowOverlap">是否允许覆盖上传</param>
        /// <param name="persistentOps">上传预转持久化</param>
        /// <param name="expireInSeconds">上传策略失效时刻</param>
        /// <param name="deleteAfterDays">文件上传后多少天后自动删除</param>
        /// <returns></returns>
        protected void SetPutPolicy(string key, bool isAllowOverlap = false, string persistentOps = "",
            int expireInSeconds = 3600, int? deleteAfterDays = null)
        {
            #region 覆盖上传

            if (isAllowOverlap)
            {
                PutPolicy.Scope = QiNiuConfig.Bucket;
            }
            else
            {
                PutPolicy.Scope = QiNiuConfig.Bucket + ":" + key;
            }

            #endregion

            #region 带数据处理的凭证

            if (!string.IsNullOrEmpty(persistentOps))
            {
                PutPolicy.PersistentOps = persistentOps;
            }

            #endregion

            #region 设置过期时间

            PutPolicy.SetExpires(expireInSeconds);

            #endregion

            #region 多少天后自动删除

            PutPolicy.DeleteAfterDays = deleteAfterDays;

            #endregion
        }

        #endregion

        #region 得到文件上传额外参数

        /// <summary>
        /// 得到文件上传额外参数
        /// </summary>
        /// <param name="uploadPersistentOps"></param>
        /// <returns></returns>
        protected PutExtra GetPutExtra(UploadPersistentOps uploadPersistentOps = null)
        {
            PutExtra putExtra = new PutExtra();
            if (uploadPersistentOps != null)
            {
                if (!string.IsNullOrEmpty(uploadPersistentOps.ResumeRecordFile))
                {
                    putExtra.ResumeRecordFile = uploadPersistentOps.ResumeRecordFile;
                }

                if (uploadPersistentOps.Params != null && uploadPersistentOps.Params.Count > 0)
                {
                    putExtra.Params = uploadPersistentOps.Params;
                }

                if (!string.IsNullOrEmpty(uploadPersistentOps.MimeType))
                {
                    putExtra.MimeType = uploadPersistentOps.MimeType;
                }

                if (uploadPersistentOps.ProgressAction != null)
                {
                    putExtra.ProgressHandler = (long uploadedBytes, long totalBytes) =>
                    {
                        uploadPersistentOps.ProgressAction(uploadedBytes, totalBytes);
                    };
                }

                if (uploadPersistentOps.UploadController != null)
                {
                    var state = putExtra.UploadController();
                    UploadStateEnum uploadState;
                    if (state == UploadControllerAction.Activated)
                    {
                        uploadState = UploadStateEnum.Activated;
                    }
                    else if (state == UploadControllerAction.Aborted)
                    {
                        uploadState = UploadStateEnum.Aborted;
                    }
                    else
                    {
                        uploadState = UploadStateEnum.Suspended;
                    }

                    uploadPersistentOps.UploadController?.Invoke(uploadState);
                }

                if (uploadPersistentOps.MaxRetryTimes != -1)
                {
                    putExtra.MaxRetryTimes = uploadPersistentOps.MaxRetryTimes;
                }
            }

            return putExtra;
        }

        #endregion

        #region 得到七牛配置文件

        /// <summary>
        /// 得到七牛配置文件
        /// </summary>
        /// <returns></returns>
        public Qiniu.Storage.Config GetConfig(UploadPersistentOps uploadPersistentOps = null)
        {
            var config = new Qiniu.Storage.Config()
            {
                Zone = QiNiuConfig.GetZone(),
            };
            if (uploadPersistentOps != null)
            {
                config.UseHttps = uploadPersistentOps.IsUseHttps;

                config.ChunkSize = Get(uploadPersistentOps.ChunkUnit);
                if (uploadPersistentOps.MaxRetryTimes != -1)
                {
                    config.MaxRetryTimes = uploadPersistentOps.MaxRetryTimes;
                }

                if (uploadPersistentOps.MaxRetryTimes != -1)
                {
                    config.MaxRetryTimes = uploadPersistentOps.MaxRetryTimes;
                }
            }

            ChunkUnit Get(ChunkUnitEnum chunkUnit)
            {
                int chunkUnits = (int) chunkUnit;
                return (ChunkUnit) chunkUnits;
            }

            return config;
        }

        #endregion

        #endregion

        #region 得到资源管理

        /// <summary>
        /// 得到资源管理
        /// </summary>
        /// <returns></returns>
        protected BucketManager GetBucketManager()
        {
            return new BucketManager(Mac, GetConfig());
        }

        #endregion
    }
}