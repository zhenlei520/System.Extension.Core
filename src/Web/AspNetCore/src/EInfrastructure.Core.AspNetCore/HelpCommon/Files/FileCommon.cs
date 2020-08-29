// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Tools.Enumerations;

namespace EInfrastructure.Core.AspNetCore.HelpCommon.Files
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileCommon : Tools.Files.FileCommon
    {
        #region 得到文件md5

        /// <summary>
        /// 得到文件md5
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetMd5(Microsoft.AspNetCore.Http.IFormFile file)
        {
            return Tools.Files.FileCommon.GetMd5(file.OpenReadStream());
        }

        #endregion

        #region 得到文件的Sha1

        /// <summary>
        /// 得到文件的Sha1
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string GetSha1(Microsoft.AspNetCore.Http.IFormFile file, bool isUpper = true)
        {
            return Tools.Files.FileCommon.GetSha1(file.OpenReadStream(), isUpper);
        }

        #endregion

        #region 得到文件的Sha256

        /// <summary>
        /// 得到文件的Sha256
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string GetSha256(Microsoft.AspNetCore.Http.IFormFile file, bool isUpper = true)
        {
            return Tools.Files.FileCommon.GetSha256(file.OpenReadStream(), isUpper);
        }

        #endregion

        #region 得到文件的Sha384

        /// <summary>
        /// 得到文件的Sha384
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string GetSha384(Microsoft.AspNetCore.Http.IFormFile file, bool isUpper = true)
        {
            return Tools.Files.FileCommon.GetSha384(file.OpenReadStream(), isUpper);
        }

        #endregion

        #region 得到文件的Sha512

        /// <summary>
        /// 得到文件的Sha512
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string GetSha512(Microsoft.AspNetCore.Http.IFormFile file, bool isUpper = true)
        {
            return Tools.Files.FileCommon.GetSha512(file.OpenReadStream(), isUpper);
        }

        #endregion

        #region 得到文件信息

        /// <summary>
        /// 得到文件信息
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="encryptType">加密方式，默认加密方式为Sha256</param>
        /// <returns></returns>
        public static EInfrastructure.Core.Tools.Files.FileInfo Get(Microsoft.AspNetCore.Http.IFormFile formFile,
            EncryptType encryptType = null)
        {
            return Tools.Files.FileCommon.Get(formFile.OpenReadStream(), formFile.FileName, encryptType);
        }

        #endregion
    }
}