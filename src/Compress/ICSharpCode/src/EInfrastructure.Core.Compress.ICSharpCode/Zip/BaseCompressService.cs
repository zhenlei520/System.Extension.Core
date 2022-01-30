// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Linq;
using System.Text;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Tools;
using ICSharpCode.SharpZipLib.Zip;

namespace EInfrastructure.Core.Compress.ICSharpCode.Zip
{
    /// <summary>
    ///
    /// </summary>
    public class BaseZipCompressService : BaseCompressProvider
    {
        #region 检查是否存在

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="file">文件地址</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <param name="action">委托方法(执行压缩方法)</param>
        protected void CheckFile(string file, bool overWrite, Action action)
        {
            if (File.Exists(file))
            {
                if (overWrite)
                {
                    File.Delete(file);
                    action.Invoke();
                }
            }
            else
            {
                action.Invoke();
            }
        }

        #endregion

        #region 得到压缩包名称

        /// <summary>
        /// 得到压缩包名称
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="zipDirectory"></param>
        /// <param name="zipName"></param>
        protected string GetCompressZipName(string sourceFilePath, string zipDirectory = "", string zipName = "")
        {
            if (string.IsNullOrEmpty(zipDirectory))
            {
                zipDirectory = new FileInfo(sourceFilePath).DirectoryName;
            }

            if (string.IsNullOrEmpty(zipName))
            {
                zipName = new FileInfo(sourceFilePath).Name;
            }

            return Path.Combine(zipDirectory, zipName);
        }

        #endregion

        #region 压缩文件

        /// <summary>
        /// 文件压缩
        /// </summary>
        /// <param name="strDirectory">文件夹目录</param>
        /// <param name="stream"></param>
        /// <param name="parentPath"></param>
        /// <param name="isRecursive">是否递归（默认递归）</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        protected void ZipSetp(string strDirectory, ZipOutputStream stream, string parentPath,
            bool isRecursive = true, int compressionLevel = 5)
        {
            if (strDirectory[strDirectory.Length - 1] != Path.DirectorySeparatorChar)
            {
                strDirectory += Path.DirectorySeparatorChar;
            }

            string[] filenames = Directory.GetFileSystemEntries(strDirectory);
            var index = 0;
            foreach (string file in filenames) // 遍历所有的文件和目录
            {
                if (!isRecursive && index >= 1)
                {
                    return; //不递归
                }

                if (Directory.Exists(file)) // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {
                    string pPath = parentPath;
                    pPath += file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                    pPath += "\\";
                    ZipSetp(file, stream, pPath, isRecursive, compressionLevel);
                }
                else // 否则直接压缩文件
                {
                    Compress(file, stream);
                }

                index++;
            }

            stream.SetLevel(compressionLevel);
        }

        #endregion

        #region 创建并检查压缩文件

        /// <summary>
        /// 创建压缩文件（单文件）
        /// </summary>
        /// <param name="overWrite">是否覆盖</param>
        /// <param name="fileZipPath">压缩包地址</param>
        /// <param name="sourceFile">源文件地址</param>
        /// <param name="password">密码</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <param name="blockSize">缓存大小（每次写入文件大小，默认 2048）</param>
        /// <exception cref="Exception"></exception>
        protected void CreateZipFile(
            bool overWrite,
            string fileZipPath,
            string sourceFile,
            string password = "",
            int compressionLevel = 5,
            int blockSize = 2048)
        {
            CreateZipFile(overWrite, fileZipPath, new[] {sourceFile}, password, compressionLevel, blockSize);
        }

        /// <summary>
        /// 创建压缩文件(多文件集合)
        /// </summary>
        /// <param name="overWrite">是否覆盖</param>
        /// <param name="fileZipPath">压缩包地址</param>
        /// <param name="sourceFileList">源文件地址列表</param>
        /// <param name="password">密码</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <param name="blockSize">缓存大小（每次写入文件大小，默认 2048）</param>
        /// <exception cref="Exception"></exception>
        protected void CreateZipFile(
            bool overWrite,
            string fileZipPath,
            string[] sourceFileList,
            string password = "",
            int compressionLevel = 5,
            int blockSize = 2048)
        {
            CheckFile(fileZipPath, overWrite,
                () =>
                {
                    CreateZipFile(fileZipPath, sourceFileList, password, compressionLevel, blockSize);
                });
        }

        #endregion

        #region 检查文件

        #region 检查文件地址

        /// <summary>
        /// 检查文件地址
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <exception cref="BusinessException"></exception>
        protected void CheckSourceFilePath(string filePath)
        {
            filePath.IsNullOrEmptyTip("请指定压缩文件");
        }

        #endregion

        #region 检查多文件地址

        /// <summary>
        /// 检查压缩文件目录
        /// </summary>
        /// <param name="filePaths"></param>
        protected void CheckSourceFilePathList(object[] filePaths)
        {
            filePaths.IsNullOrEmptyTip("请指定压缩文件");
        }

        #endregion

        #region 检查压缩文件夹目录

        /// <summary>
        /// 检查压缩文件目录
        /// </summary>
        /// <param name="zipSourceDirectory"></param>
        protected void CheckZipSourceDirectory(string zipSourceDirectory)
        {
            zipSourceDirectory.IsNullOrEmptyTip("请指定压缩目录");
        }

        #endregion

        #region 检查压缩文件目录

        /// <summary>
        /// 检查压缩文件目录
        /// </summary>
        /// <param name="zipDirectory"></param>
        protected void CheckZipDirectory(string zipDirectory)
        {
            zipDirectory.IsNullOrEmptyTip("请指定压缩包保存目录");
        }

        #endregion

        #region 检查压缩包名称

        /// <summary>
        /// 检查压缩文件目录
        /// </summary>
        /// <param name="zipName">压缩包名称</param>
        protected void CheckZipName(string zipName)
        {
            zipName.IsNullOrEmptyTip("请指定压缩包名称");
        }

        #endregion

        #endregion

        #region private methods

        #region 创建压缩文件

        /// <summary>
        /// 创建压缩文件
        /// </summary>
        /// <param name="fileZipPath">压缩包地址</param>
        /// <param name="sourceFileList">源文件地址列表</param>
        /// <param name="password">密码</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <param name="blockSize">缓存大小（每次写入文件大小，默认 2048）</param>
        /// <exception cref="Exception"></exception>
        private void CreateZipFile(
            string fileZipPath,
            string[] sourceFileList,
            string password = "",
            int compressionLevel = 5,
            int blockSize = 2048)
        {
            using (FileStream zipFile = File.Create(fileZipPath))
            {
                Encoding encoding = Encoding.UTF8;
                ZipStrings.CodePage = encoding.CodePage;
                using (ZipOutputStream zipStream = new ZipOutputStream(zipFile))
                {
                    zipStream.SetLevel(compressionLevel); //设置压缩级别
                    if (!password.IsNullOrEmpty())
                    {
                        //压缩文件加密
                        zipStream.Password = password;
                    }

                    sourceFileList.ToList().ForEach(filePath => { Compress(filePath, zipStream, blockSize); });

                    zipStream.Finish();
                    zipStream.Close();
                }

                zipFile.Close();
            }
        }

        #endregion

        #region 压缩文件

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFile">文件地址</param>
        /// <param name="stream">文件流</param>
        /// <param name="blockSize">缓存大小（每次写入文件大小，默认 4048）</param>
        private void Compress(string sourceFile, ZipOutputStream stream, int blockSize = 4048)
        {
            //打开压缩文件
            using (FileStream fs = File.OpenRead(sourceFile))
            {
                byte[] buffer = new byte[blockSize];
                string fileName = Path.GetFileName(sourceFile);
                ZipEntry entry = new ZipEntry(fileName)
                {
                    DateTime = DateTime.Now
                };
                stream.PutNextEntry(entry);

                int sourceBytes;
                do
                {
                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, sourceBytes);
                } while (sourceBytes > 0);

                fs.Close();
            }
        }

        #endregion

        #endregion
    }
}
