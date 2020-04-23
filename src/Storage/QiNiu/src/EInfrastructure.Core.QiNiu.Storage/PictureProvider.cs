// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Reflection;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage.Pictures;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.Tools;
using Qiniu.Http;
using Qiniu.Storage;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 图片服务
    /// </summary>
    public class PictureProvider : BaseStorageProvider, IPictureProvider
    {
        /// <summary>
        /// 图片服务
        /// </summary>
        public PictureProvider(QiNiuStorageConfig qiNiuConfig = null) : base(
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
            string token = base.GetUploadCredentials(QiNiuConfig,
                new UploadPersistentOpsParam(param.ImgPersistentOps.Key, param.ImgPersistentOps));
            FormUploader target = new FormUploader(Core.Tools.GetConfig(this.QiNiuConfig));
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
            FetchResult ret = GetBucketManager()
                .Fetch(fetchFileParam.SourceFileKey, QiNiuConfig.Bucket, fetchFileParam.Key);
            switch (ret.Code)
            {
                case (int) HttpCode.OK:
                case (int) HttpCode.CALLBACK_FAILED:
                    return true;
                default:
                    return false;
            }
        }

        #endregion
    }
}
