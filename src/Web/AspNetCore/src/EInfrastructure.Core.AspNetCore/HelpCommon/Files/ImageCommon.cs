// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Tools;

namespace EInfrastructure.Core.AspNetCore.HelpCommon.Files
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImageCommon
    {
        #region 根据上传文件获取图片base64

        /// <summary>
        /// 根据上传文件获取图片base64
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public static string GetBase64(Microsoft.AspNetCore.Http.IFormFile formFile)
        {
            string encoding = Base64Common.GetBaseEncoding(formFile.ContentType);
            encoding.IsNullOrEmptyTip("未知的文件类型");
            return $"{encoding},{formFile.OpenReadStream().ConvertToBase64()}";
        }

        #endregion
    }
}