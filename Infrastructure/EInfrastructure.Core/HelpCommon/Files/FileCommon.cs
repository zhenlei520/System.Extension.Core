using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace EInfrastructure.Core.HelpCommon.Files
{
    public class FileCommon
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
        /// <returns></returns>
        public static string GetSha1(IFormFile file)
        {
            return GetSha(file, new SHA1CryptoServiceProvider());
        }

        #endregion

        #region 得到文件的Sha256

        /// <summary>
        /// 得到文件的Sha256
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetSha256(IFormFile file)
        {
            return GetSha(file, new SHA256CryptoServiceProvider());
        }

        #endregion

        #region 得到文件的Sha384

        /// <summary>
        /// 得到文件的Sha512
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetSha384(IFormFile file)
        {
            return GetSha(file, new SHA384CryptoServiceProvider());
        }

        #endregion

        #region 得到文件的Sha512

        /// <summary>
        /// 得到文件的Sha512
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetSha512(IFormFile file)
        {
            return GetSha(file, new SHA512CryptoServiceProvider());
        }

        #endregion

        #region 得到sha系列加密信息

        /// <summary>
        /// 得到sha系列加密信息
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="hashAlgorithm"></param>
        /// <returns></returns>
        private static string GetSha(IFormFile formFile, HashAlgorithm hashAlgorithm)
        {
            var stream = formFile.OpenReadStream();
            byte[] retval = hashAlgorithm.ComputeHash(stream);
            stream.Close();

            StringBuilder sc = new StringBuilder();
            for (int i = 0; i < retval.Length; i++)
            {
                sc.Append(retval[i].ToString("X2"));
            }

            return sc.ToString();
        }

        #endregion

        #region 得到文件信息

        /// <summary>
        /// 得到文件信息
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="encryptType">加密方式，默认加密方式为Sha256</param>
        /// <returns></returns>
        public EInfrastructure.Core.HelpCommon.Files.FileInfo Get(IFormFile formFile,
            EncryptTypeEnum encryptType = EncryptTypeEnum.Sha256)
        {
            string conditionCode;
            switch (encryptType)
            {
                case EncryptTypeEnum.Md5:
                    conditionCode = GetMd5(formFile);
                    break;
                case EncryptTypeEnum.Sha1:
                    conditionCode = GetSha1(formFile);
                    break;
                case EncryptTypeEnum.Sha256:
                default:
                    conditionCode = GetSha256(formFile);
                    break;
                case EncryptTypeEnum.Sha384:
                    conditionCode = GetSha384(formFile);
                    break;
                case EncryptTypeEnum.Sha512:
                    conditionCode = GetSha512(formFile);
                    break;
            }

            return new EInfrastructure.Core.HelpCommon.Files.FileInfo()
            {
                Name = formFile.FileName,
                ConditionCode = conditionCode
            };
        }

        #endregion
    }
}