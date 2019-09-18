// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace EInfrastructure.Core.HelpCommon.Files
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
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

        /// <summary>
        /// 根据本地文件地址得到文件md5
        /// </summary>
        /// <param name="localFilePath">文件绝对地址</param>
        /// <returns></returns>
        public static string GetMd5(string localFilePath)
        {
            String hashMd5 = String.Empty;
            //检查文件是否存在，如果文件存在则进行计算，否则返回空值
            if (File.Exists(localFilePath))
            {
                using (FileStream fileStream =
                    new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
                {
                    //计算文件的MD5值
                    MD5 calculator = MD5.Create();
                    Byte[] buffer = calculator.ComputeHash(fileStream);
                    calculator.Clear();
                    //将字节数组转换成十六进制的字符串形式
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var t in buffer)
                    {
                        stringBuilder.Append(t.ToString("x2"));
                    }

                    hashMd5 = stringBuilder.ToString();
                }
            }

            return hashMd5;
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

        /// <summary>
        /// 根据本地文件地址得到文件的Sha1
        /// </summary>
        /// <param name="localFilePath">文件绝对地址</param>
        /// <returns></returns>
        public static string GetSha1(string localFilePath)
        {
            using (FileStream fileStream =
                new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
            {
                return GetSha(fileStream, new SHA1CryptoServiceProvider());
            }
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

        /// <summary>
        /// 根据本地文件地址得到文件的Sha256
        /// </summary>
        /// <param name="localFilePath">文件绝对地址</param>
        /// <returns></returns>
        public static string GetSha256(string localFilePath)
        {
            using (FileStream fileStream =
                new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
            {
                return GetSha(fileStream, new SHA256CryptoServiceProvider());
            }
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

        /// <summary>
        /// 根据本地文件地址得到文件的Sha384
        /// </summary>
        /// <param name="localFilePath">文件绝对地址</param>
        /// <returns></returns>
        public static string GetSha384(string localFilePath)
        {
            using (FileStream fileStream =
                new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
            {
                return GetSha(fileStream, new SHA384CryptoServiceProvider());
            }
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

        /// <summary>
        /// 根据本地文件地址得到文件的Sha512
        /// </summary>
        /// <param name="localFilePath">文件绝对地址</param>
        /// <returns></returns>
        public static string GetSha512(string localFilePath)
        {
            using (FileStream fileStream =
                new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
            {
                return GetSha(fileStream, new SHA512CryptoServiceProvider());
            }
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
            return SecurityCommon.GetSha(retval, hashAlgorithm);
        }

        /// <summary>
        /// 得到sha系列加密信息
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="hashAlgorithm"></param>
        /// <returns></returns>
        private static string GetSha(FileStream fileStream, HashAlgorithm hashAlgorithm)
        {
            byte[] retval = hashAlgorithm.ComputeHash(fileStream);
            fileStream?.Close();
            return SecurityCommon.GetSha(retval, hashAlgorithm);
        }

        #endregion

        #region 得到文件信息

        /// <summary>
        /// 得到文件信息
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="encryptType">加密方式，默认加密方式为Sha256</param>
        /// <returns></returns>
        public static FileInfo Get(IFormFile formFile,
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

            return new FileInfo
            {
                Name = formFile.FileName,
                ConditionCode = conditionCode
            };
        }

        #endregion

        #region 得到文件地址信息

        /// <summary>
        /// 得到当前文件夹下的所有文件地址
        /// </summary>
        /// <param name="path">要搜索的目录的相对或绝对路径</param>
        /// <returns></returns>
        public static string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        /// <summary>
        /// 根据通配符搜索文件下的所有地址信息，可选择查询所有层级的或者当前层级的
        /// </summary>
        /// <param name="path">要搜索的目录的相对或绝对路径</param>
        /// <param name="searchPattern">要与 path 中的文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
        /// <param name="searchOption">默认当前文件夹下 TopDirectoryOnly，若查询包含所有子目录为AllDirectories</param>
        /// <returns></returns>
        public static string[] GetFiles(string path, string searchPattern,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Directory.GetFiles(path, searchPattern, searchOption);
        }

        #endregion

        #region 将文件转换成Base64格式

        /// <summary>
        /// 将文件转换成Base64格式
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <returns></returns>
        public static string FileToBase64(string filePath)
        {
            string result = "";
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    return fs.ConvertToBase64();
                }
            }
            catch
            {
                result = "";
            }

            return result;
        }

        #endregion

        #region 将文件转换为byte数组

        #region 将文件转换成byte[]数组

        /// <summary>
        /// 将文件转换成byte[]数组
        /// </summary>
        /// <param name="localFilePath">文件路径文件名称</param>
        /// <returns>byte[]数组</returns>
        public static byte[] ConvertFileToByte(string localFilePath)
        {
            try
            {
                using (FileStream fs = new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] byteArray = new byte[fs.Length];
                    fs.Read(byteArray, 0, byteArray.Length);
                    return byteArray;
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 将byte[]数组保存成文件

        /// <summary>
        /// 将byte[]数组保存成文件
        /// </summary>
        /// <param name="byteArray">byte[]数组</param>
        /// <param name="localFilePath">保存至硬盘的文件路径</param>
        /// <returns></returns>
        public static bool SaveByteToFile(byte[] byteArray, string localFilePath)
        {
            bool result = false;
            try
            {
                using (FileStream fs = new FileStream(localFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        #endregion

        #endregion

        #region 获取文件内容

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <returns></returns>
        public static string GetFileContent(string filePath)
        {
            string result = "";
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        result = reader.ReadLine();
                    }
                }
            }
            catch
            {
                result = "";
            }

            return result;
        }

        #endregion
    }
}
