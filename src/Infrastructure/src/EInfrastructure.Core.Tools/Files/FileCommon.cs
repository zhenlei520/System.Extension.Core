// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using EInfrastructure.Core.Config.ExceptionExtensions;

namespace EInfrastructure.Core.Tools.Files
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileCommon
    {
        #region 得到文件md5

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
        /// 根据本地文件地址得到文件的Sha1
        /// </summary>
        /// <param name="localFilePath">文件绝对地址</param>
        /// <returns></returns>
        public static string GetSha1(string localFilePath)
        {
            using (FileStream fileStream =
                new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
            {
                return SecurityCommon.GetSha(fileStream, new SHA1CryptoServiceProvider());
            }
        }

        #endregion

        #region 得到文件的Sha256

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
                return SecurityCommon.GetSha(fileStream, new SHA256CryptoServiceProvider());
            }
        }

        #endregion

        #region 得到文件的Sha384

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
                return SecurityCommon.GetSha(fileStream, new SHA384CryptoServiceProvider());
            }
        }

        #endregion

        #region 得到文件的Sha512

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
                return SecurityCommon.GetSha(fileStream, new SHA512CryptoServiceProvider());
            }
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
        /// <param name="encoding">编码格式,默认为Encoding.Default</param>
        /// <returns></returns>
        public static string GetFileContent(string filePath, Encoding encoding = null)
        {
            string result = "";
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fs, encoding ?? Encoding.Default))
                    {
                        result = reader.ReadToEnd();
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
