// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.HelpCommon.Serialization;
using EInfrastructure.Core.Interface.Log;
using EInfrastructure.Core.Interface.Storage.Config;
using EInfrastructure.Core.Interface.Storage.Enum;
using EInfrastructure.Core.Interface.Storage.Param;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Validator;
using EInfrastructure.Core.Validation.Common;
using Qiniu.Storage;
using Qiniu.Util;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 基类存储实现类
    /// </summary>
    public partial class BaseStorageProvider
    {
        /// <summary>
        /// 存储配置文件
        /// </summary>
        private readonly QiNiuStorageConfig _qiNiuConfig;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ILogService LogService;

        /// <summary>
        /// json服务
        /// </summary>
        protected readonly JsonCommon JsonService;

        /// <summary>
        ///
        /// </summary>
        public BaseStorageProvider(ILogService logService, QiNiuStorageConfig qiNiuConfig)
        {
            JsonService = new JsonCommon();
            LogService = logService;
            _qiNiuConfig = qiNiuConfig;
            if (qiNiuConfig != null)
            {
                new QiNiuConfigValidator().Validate(qiNiuConfig).Check();
            }
        }

        #region 得到七牛配置（方法内不要重复获取此方法，以免产生不同的配置信息）

        /// <summary>
        /// 得到七牛配置（方法内不要重复获取此方法，以免产生不同的配置信息）
        /// </summary>
        /// <param name="config">七牛配置文件json对象(默认查询公共的配置)</param>
        /// <returns></returns>
        internal QiNiuStorageConfig GetQiNiuConfig(string config = null)
        {
            var qiniuConfig = !string.IsNullOrEmpty(config)
                ? QiNiuStorageConfig.GetQiNiuConfig(JsonService, config)
                : _qiNiuConfig;
            return qiniuConfig;
        }

        #endregion

        #region 设置上传策略

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
                    UploadStateEnum  uploadState;
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
        internal Qiniu.Storage.Config GetConfig(UploadPersistentOps uploadPersistentOps = null)
        {
            var config = new Qiniu.Storage.Config()
            {
                Zone = _qiNiuConfig.GetZone(),
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
                int chunkUnits = (int)chunkUnit;
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
            return new BucketManager(_qiNiuConfig.GetMac(), GetConfig());
        }

        #endregion

        #region 得到上传凭证

        /// <summary>
        /// 得到上传凭证
        /// </summary>
        /// <param name="qiNiuConfig">七牛配置文件</param>
        /// <param name="opsParam">上传策略</param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected string GetUploadCredentials(QiNiuStorageConfig qiNiuConfig, UploadPersistentOpsParam opsParam,
            Action<PutPolicy> action = null)
        {
            PutPolicyConfig putPolicyConfig = new PutPolicyConfig(qiNiuConfig);
            putPolicyConfig.SetPutPolicy(opsParam.Key,
                opsParam.UploadPersistentOps.IsAllowOverlap,
                opsParam.UploadPersistentOps.PersistentOps);
            action?.Invoke(putPolicyConfig.GetPutPolicy());
            return Auth.CreateUploadToken(qiNiuConfig.GetMac(), putPolicyConfig.GetPutPolicy().ToJsonString());
        }

        #endregion
    }
}
