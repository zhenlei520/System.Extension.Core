// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Config.StorageExtensions;
using EInfrastructure.Core.Config.StorageExtensions.Param;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.HelpCommon.Systems;
using EInfrastructure.Core.QiNiu.Storage.Config;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 文件实现类
    /// </summary>
    public class StorageProvider : BaseStorageProvider, IStorageService, ISingleInstance
    {
        /// <summary>
        /// 文件实现类
        /// </summary>
        public StorageProvider(ILogService logService, QiNiuStorageConfig qiNiuConfig) : base(logService, qiNiuConfig)
        {
        }

        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
        {
            return AssemblyCommon.GetReflectedInfo().Namespace;
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
            SetPutPolicy(param.Key, param.UploadPersistentOps.IsAllowOverlap, param.UploadPersistentOps.PersistentOps);
            string token = Auth.CreateUploadToken(Mac, PutPolicy.ToJsonString());
            FormUploader target = new FormUploader(GetConfig(param.UploadPersistentOps));
            HttpResult result =
                target.UploadStream(param.Stream, param.Key, token, GetPutExtra(param.UploadPersistentOps));
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
            SetPutPolicy(param.Key, param.UploadPersistentOps.IsAllowOverlap, param.UploadPersistentOps.PersistentOps);
            string token = Auth.CreateUploadToken(Mac, PutPolicy.ToJsonString());
            FormUploader target = new FormUploader(GetConfig(param.UploadPersistentOps));
            if (param.File != null)
            {
                HttpResult result =
                    target.UploadStream(param.File.OpenReadStream(), param.Key, token,
                        GetPutExtra(param.UploadPersistentOps));
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
        /// <param name="func"></param>
        public string GetUploadCredentials(UploadPersistentOpsParam opsParam, Func<string> func)
        {
            SetPutPolicy(opsParam.Key, opsParam.UploadPersistentOps.IsAllowOverlap,
                opsParam.UploadPersistentOps.PersistentOps);
            PutPolicy.CallbackBody = func?.Invoke();
            return Auth.CreateUploadToken(Mac, PutPolicy.ToJsonString());
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
            BucketManager bucketManager = new BucketManager(base.Mac, base.GetConfig());

            bool isExist = false;

            // 存储空间名
            string Bucket = base.QiNiuConfig.Bucket;
            //     待查询文件名
            StatResult statRet = bucketManager.Stat(Bucket, key);

            if (statRet.Code == (int)HttpCode.OK)
            {
                isExist = true;
            }

            return isExist;

        }

        #endregion

    }
}
