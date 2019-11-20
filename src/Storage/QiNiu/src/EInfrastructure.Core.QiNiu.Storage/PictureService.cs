// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Reflection;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Interface.IOC;
using EInfrastructure.Core.Interface.Log;
using EInfrastructure.Core.Interface.Storage;
using EInfrastructure.Core.Interface.Storage.Config;
using EInfrastructure.Core.Interface.Storage.Param;
using EInfrastructure.Core.Interface.Storage.Param.Pictures;
using EInfrastructure.Core.QiNiu.Storage.Config;
using Qiniu.Http;
using Qiniu.Storage;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 图片服务
    /// </summary>
    public class PictureService : BaseStorageProvider, IPictureService, ISingleInstance
    {
        /// <summary>
        /// 图片服务
        /// </summary>
        public PictureService(ILogService logService = null,
            QiNiuStorageConfig qiNiuConfig = null) : base(logService,
            qiNiuConfig)
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

        #region 根据图片base64上传

        /// <summary>
        /// 根据图片base64上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Upload(UploadByBase64Param param)
        {
            var qiNiuConfig = GetQiNiuConfig(param.Json);
            string token = base.GetUploadCredentials(qiNiuConfig,
                new UploadPersistentOpsParam(param.ImgPersistentOps.Key, new UploadPersistentOps()
                {
                    IsAllowOverlap = param.ImgPersistentOps.IsAllowOverlap
                }));
            FormUploader target = new FormUploader(GetConfig());
            HttpResult result =
                target.UploadData(param.Base64.ConvertToByte(), param.ImgPersistentOps.Key, token,
                    GetPutExtra());
            return result.Code == (int) HttpCode.OK;
        }

        #endregion

        #region 抓取资源到空间

        /// <summary>
        /// 抓取资源到空间
        /// </summary>
        /// <param name="fetchFileParam">资源信息</param>
        /// <returns></returns>
        public bool FetchFile(FetchFileParam fetchFileParam)
        {
            var qiNiuConfig = GetQiNiuConfig(fetchFileParam.Json);
            FetchResult ret = GetBucketManager()
                .Fetch(fetchFileParam.SourceFileKey, qiNiuConfig.Bucket, fetchFileParam.Key);
            switch (ret.Code)
            {
                case (int) HttpCode.OK:
                case (int) HttpCode.CALLBACK_FAILED:
                    LogService?.Info($"上传code为：{ret.Code}");
                    return true;
                default:
                    return false;
            }
        }

        #endregion
    }
}
