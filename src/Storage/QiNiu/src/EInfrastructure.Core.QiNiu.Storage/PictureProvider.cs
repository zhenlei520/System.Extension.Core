// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Reflection;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage.Pictures;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.Tools;
using Microsoft.Extensions.Logging;
using Qiniu.Http;
using Qiniu.Storage;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 图片服务
    /// </summary>
    public class PictureProvider : BaseStorageProvider, IPictureProvider
    {
        private readonly ILogger _logger;

        /// <summary>
        /// 图片服务
        /// </summary>
        public PictureProvider(ILogger<PictureProvider> logger, QiNiuStorageConfig qiNiuConfig) : base(
            qiNiuConfig)
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
            FormUploader target = new FormUploader(Core.Tools.GetConfig(QiNiuConfig, param.ImgPersistentOps));
            HttpResult result =
                target.UploadData(param.Base64.ConvertToByte(), param.ImgPersistentOps.Key, token,
                    GetPutExtra());
            if (result.Code == (int) HttpCode.OK)
            {
                return true;
            }

            this._logger?.LogInformation(result.ToString());
            return false;
        }

        #endregion
    }
}
