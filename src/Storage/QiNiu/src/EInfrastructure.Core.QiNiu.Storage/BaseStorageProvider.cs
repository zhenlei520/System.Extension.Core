// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Param;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.Validation.Common;
using Qiniu.Storage;
using Qiniu.Util;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 基类存储实现类
    /// </summary>
    public abstract class BaseStorageProvider
    {
        /// <summary>
        /// 存储配置文件
        /// </summary>
        private readonly QiNiuStorageConfig _qiNiuConfig;

        /// <summary>
        ///
        /// </summary>
        public BaseStorageProvider(QiNiuStorageConfig qiNiuConfig)
        {
            _qiNiuConfig = qiNiuConfig;
            ValidationCommon.Check(qiNiuConfig, "七牛云存储配置异常", HttpStatus.Err.Name);
        }

        #region 得到七牛配置（方法内不要重复获取此方法，以免产生不同的配置信息）

        /// <summary>
        /// 得到七牛配置（方法内不要重复获取此方法，以免产生不同的配置信息）
        /// </summary>
        /// <returns></returns>
        internal QiNiuStorageConfig QiNiuConfig => _qiNiuConfig;

        #endregion

        #region 设置上传策略

        #region 得到文件上传额外参数

        /// <summary>
        /// 得到文件上传额外参数
        /// </summary>
        /// <param name="uploadPersistentOps"></param>
        /// <returns></returns>
        protected virtual PutExtra GetPutExtra(UploadPersistentOps uploadPersistentOps = null)
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
                    UploadState uploadState;
                    if (state == UploadControllerAction.Activated)
                    {
                        uploadState = UploadState.Activated;
                    }
                    else if (state == UploadControllerAction.Aborted)
                    {
                        uploadState = UploadState.Aborted;
                    }
                    else
                    {
                        uploadState = UploadState.Suspended;
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
        internal virtual Qiniu.Storage.Config GetConfig(UploadPersistentOps uploadPersistentOps = null)
        {
            var config = new Qiniu.Storage.Config()
            {
                Zone = _qiNiuConfig.GetZone(),
            };
            if (uploadPersistentOps != null)
            {
                if (uploadPersistentOps.IsUseHttps != null)
                {
                    config.UseHttps = uploadPersistentOps.IsUseHttps.Value;
                }

                config.ChunkSize = (Qiniu.Storage.ChunkUnit)(uploadPersistentOps.ChunkUnit.Id);
                if (uploadPersistentOps.MaxRetryTimes != -1)
                {
                    config.MaxRetryTimes = uploadPersistentOps.MaxRetryTimes;
                }
            }

            return config;
        }

        #endregion

        #endregion

        #region 得到资源管理

        private BucketManager _bucketManager;

        /// <summary>
        /// 得到资源管理
        /// </summary>
        /// <returns></returns>
        protected virtual BucketManager GetBucketManager()
        {
            if (_bucketManager == null)
                _bucketManager = new BucketManager(_qiNiuConfig.GetMac(), GetConfig());
            return _bucketManager;
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
        protected virtual string GetUploadCredentials(QiNiuStorageConfig qiNiuConfig, UploadPersistentOpsParam opsParam,
            Action<PutPolicy> action = null)
        {
            PutPolicyConfig putPolicyConfig = new PutPolicyConfig(qiNiuConfig);
            putPolicyConfig.SetPutPolicy(opsParam);
            action?.Invoke(putPolicyConfig.GetPutPolicy());
            return Auth.CreateUploadToken(qiNiuConfig.GetMac(), putPolicyConfig.GetPutPolicy().ToJsonString());
        }

        #endregion

        #region 得到上传策略

        /// <summary>
        /// 得到上传策略
        /// </summary>
        /// <param name="uploadPersistentOps">上传策略</param>
        /// <returns></returns>
        protected virtual UploadPersistentOps GetUploadPersistentOps(UploadPersistentOps uploadPersistentOps)
        {
            if (uploadPersistentOps == null)
            {
                uploadPersistentOps = new UploadPersistentOps();
            }

            if (uploadPersistentOps.IsUseHttps == null)
            {
                uploadPersistentOps.IsUseHttps = _qiNiuConfig.IsUseHttps;
            }

            if (uploadPersistentOps.UseCdnDomains == null)
            {
                uploadPersistentOps.UseCdnDomains = _qiNiuConfig.UseCdnDomains;
            }

            if (uploadPersistentOps.IsAllowOverlap == null)
            {
                uploadPersistentOps.IsAllowOverlap = _qiNiuConfig.IsAllowOverlap;
            }

            if (uploadPersistentOps.ChunkUnit == null)
            {
                uploadPersistentOps.ChunkUnit = _qiNiuConfig.ChunkUnit;
            }

            if ((uploadPersistentOps.EnableCallback == null && _qiNiuConfig.EnableCallback) ||
                uploadPersistentOps.EnableCallback == true)
            {
                string callbackHost = string.IsNullOrEmpty(uploadPersistentOps.CallbackHost)
                    ? _qiNiuConfig.CallbackHost
                    : uploadPersistentOps.CallbackHost;
                string callbackUrl = string.IsNullOrEmpty(uploadPersistentOps.CallbackUrl)
                    ? _qiNiuConfig.CallbackUrl
                    : uploadPersistentOps.CallbackUrl;
                string callbackBody = string.IsNullOrEmpty(uploadPersistentOps.CallbackBody)
                    ? _qiNiuConfig.CallbackBody
                    : uploadPersistentOps.CallbackBody;
                string callbackBodyType = string.IsNullOrEmpty(uploadPersistentOps.CallbackBodyType)
                    ? CallbackBodyType
                          .FromValue<CallbackBodyType>(_qiNiuConfig.CallbackBodyType)?.Name ??
                      CallbackBodyType.Json.Name
                    : uploadPersistentOps.CallbackBodyType;
                uploadPersistentOps.SetCallBack(callbackBodyType, callbackHost, callbackUrl, callbackBody);
            }

            if (uploadPersistentOps.EnablePersistentNotifyUrl)
            {
                string persistentNotifyUrl = string.IsNullOrEmpty(uploadPersistentOps.PersistentNotifyUrl)
                    ? _qiNiuConfig.PersistentNotifyUrl
                    : uploadPersistentOps.PersistentNotifyUrl;
                uploadPersistentOps.SetPersistentNotifyUrl(persistentNotifyUrl);
            }

            if (uploadPersistentOps.EnablePersistentPipeline)
            {
                string persistentPipeline = string.IsNullOrEmpty(uploadPersistentOps.PersistentPipeline)
                    ? _qiNiuConfig.PersistentPipeline
                    : uploadPersistentOps.PersistentPipeline;
                uploadPersistentOps.SetPersistentPipeline(persistentPipeline);
            }

            return uploadPersistentOps;
        }

        #endregion

        #region 返回权重

        /// <summary>
        /// 返回权重
        /// </summary>
        /// <returns></returns>
        public virtual int GetWeights()
        {
            return 99;
        }

        #endregion
    }
}
