// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Net;
using EInfrastructure.Core.Config.IdentificationExtensions.Dto;
using Microsoft.AspNetCore.Http;

namespace EInfrastructure.Core.Config.IdentificationExtensions
{
    /// <summary>
    /// 鉴定服务（体验版）
    /// </summary>
    public interface IAuthenticateDemoService
    {
        #region 得到实现类唯一标示

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        string GetIdentify();

        #endregion

        #region 鉴定图片信息

        /// <summary>
        /// 鉴定图片信息
        /// </summary>
        /// <param name="url">图片地址</param>
        /// <param name="webProxy">代理信息</param>
        /// <param name="cookie">cookie信息</param>
        /// <returns></returns>
        ContentInfoDto ImgAuthenticateByUrl(string url, WebProxy webProxy = null, string cookie = "");

        #endregion

        #region 鉴定图片信息（根据图片base64,带data:image/jpeg;base64,）

        /// <summary>
        /// 鉴定图片信息（根据图片base64,带data:image/jpeg;base64,）
        /// </summary>
        /// <param name="base64">图片base64</param>
        /// <param name="webProxy">代理信息</param>
        /// <param name="cookie">cookie信息</param>
        /// <returns></returns>
        ContentInfoDto ImgAuthenticateByBase64(string base64, WebProxy webProxy = null, string cookie = "");

        #endregion

        #region 鉴定图片信息

        /// <summary>
        /// 鉴定图片信息
        /// </summary>
        /// <param name="formFile">文件信息</param>
        /// <param name="webProxy">代理信息</param>
        /// <param name="cookie">cookie信息</param>
        /// <returns></returns>
        ContentInfoDto ImgAuthenticateByFile(IFormFile formFile, WebProxy webProxy = null, string cookie = "");

        #endregion
    }
}
