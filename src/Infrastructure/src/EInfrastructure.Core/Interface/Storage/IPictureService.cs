// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Interface.Storage.Param.Pictures;

namespace EInfrastructure.Core.Interface.Storage
{
    /// <summary>
    /// 图片
    /// </summary>
    public interface IPictureService
    {
        /// <summary>
        /// 根据图片base64流上传图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        bool Upload(UploadByBase64Param param);

        /// <summary>
        /// 抓取资源到空间
        /// </summary>
        /// <param name="fetchFileParam">资源信息</param>
        /// <returns></returns>
        bool FetchFile(FetchFileParam fetchFileParam);
    }
}