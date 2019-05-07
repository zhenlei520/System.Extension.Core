// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Config.StorageExtensions.Param.Pictures;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Config.StorageExtensions
{
    /// <summary>
    /// 图片
    /// </summary>
    public interface IPictureService : IIdentify
    {
        #region 根据图片base64流上传图片

        /// <summary>
        /// 根据图片base64流上传图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool Upload(UploadByBase64Param param);

        #endregion

        #region 抓取资源到空间

        /// <summary>
        /// 抓取资源到空间
        /// </summary>
        /// <param name="fetchFileParam">资源信息</param>
        /// <returns></returns>
        bool FetchFile(FetchFileParam fetchFileParam);

        #endregion
    }
}
