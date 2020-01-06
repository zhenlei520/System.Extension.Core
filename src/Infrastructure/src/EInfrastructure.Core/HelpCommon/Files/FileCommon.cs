// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Enumerations;
using Microsoft.AspNetCore.Http;

namespace EInfrastructure.Core.HelpCommon.Files
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
        public static string GetMd5(IFormFile file)
        {
            int bufferSize = 1024 * 16; //自定义缓冲区大小16K
            HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
            int readLength = 0; //每次读取长度
            var output = new byte[bufferSize];
            Stream inputStream = file.OpenReadStream();
            byte[] buffer = new byte[bufferSize];
            while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                //计算MD5
                hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
            }

            //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
            string md5 = BitConverter.ToString(hashAlgorithm.Hash);
            hashAlgorithm.Clear();
            inputStream.Close();
            md5 = md5.Replace("-", "");
            return md5;
        }

        #endregion

        #region 得到文件的Sha1

        /// <summary>
        /// 得到文件的Sha1
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string GetSha1(IFormFile file, bool isUpper = true)
        {
            return GetSha(file, new SHA1CryptoServiceProvider(), isUpper);
        }

        #endregion

        #region 得到文件的Sha256

        /// <summary>
        /// 得到文件的Sha256
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string GetSha256(IFormFile file, bool isUpper = true)
        {
            return GetSha(file, new SHA256CryptoServiceProvider(), isUpper);
        }

        #endregion

        #region 得到文件的Sha384

        /// <summary>
        /// 得到文件的Sha512
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string GetSha384(IFormFile file, bool isUpper = true)
        {
            return GetSha(file, new SHA384CryptoServiceProvider(), isUpper);
        }

        #endregion

        #region 得到文件的Sha512

        /// <summary>
        /// 得到文件的Sha512
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string GetSha512(IFormFile file, bool isUpper = true)
        {
            return GetSha(file, new SHA512CryptoServiceProvider(), isUpper);
        }

        #endregion

        #region 得到sha系列加密信息

        /// <summary>
        /// 得到sha系列加密信息
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="hashAlgorithm"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        private static string GetSha(IFormFile formFile, HashAlgorithm hashAlgorithm, bool isUpper)
        {
            var stream = formFile.OpenReadStream();
            byte[] retval = hashAlgorithm.ComputeHash(stream);
            stream.Close();
            return SecurityCommon.GetSha(retval, hashAlgorithm, isUpper);
        }

        #endregion

        #region 得到文件信息

        /// <summary>
        /// 得到文件信息
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="encryptType">加密方式，默认加密方式为Sha256</param>
        /// <returns></returns>
        public static EInfrastructure.Core.Tools.Files.FileInfo Get(IFormFile formFile,
            EncryptType encryptType = null)
        {
            string conditionCode = "";
            if (encryptType != null)
            {
                if (encryptType.Id == EncryptType.Md5.Id)
                {
                    conditionCode = GetMd5(formFile);
                }
                else if (encryptType.Id == EncryptType.Sha1.Id)
                {
                    conditionCode = GetSha1(formFile);
                }
                else if (encryptType.Id == EncryptType.Sha256.Id)
                {
                    conditionCode = GetSha256(formFile);
                }
                else if (encryptType.Id == EncryptType.Sha384.Id)
                {
                    conditionCode = GetSha384(formFile);
                }
                else if (encryptType.Id == EncryptType.Sha512.Id)
                {
                    conditionCode = GetSha512(formFile);
                }
            }
            else
            {
                conditionCode = GetSha256(formFile);
            }

            return new EInfrastructure.Core.Tools.Files.FileInfo(formFile.FileName, conditionCode);
        }

        #endregion
    }
}
