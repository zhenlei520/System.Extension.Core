// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EInfrastructure.Core.Tools;
using ICSharpCode.SharpZipLib.Zip;
using FileCommon = EInfrastructure.Core.Tools.Common.FileCommon;

namespace EInfrastructure.Core.Compress.ICSharpCode.Zip
{
    /// <summary>
    /// zip压缩
    /// </summary>
    public class ZipCompressService : BaseZipCompressService
    {
        #region 压缩文件

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFilePath">文件指定路径</param>
        /// <param name="zipDirectory">压缩包保存目录,默认与源文件在同一目录</param>
        /// <param name="zipName">压缩文件名称，默认与源文件名称一致</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <param name="isEncrypt">是否加密</param>
        /// <param name="password">密码</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <param name="blockSize">缓存大小（每次写入文件大小，默认 2048）</param>
        /// <returns></returns>
        public override string CompressSingle(string sourceFilePath, string zipDirectory = "", string zipName = "",
            bool overWrite = true,
            bool isEncrypt = false,
            string password = "", int compressionLevel = 5, int blockSize = 2048)
        {
            CheckSourceFilePath(sourceFilePath);

            string fileZipPath = GetCompressZipName(sourceFilePath, zipDirectory, zipName);
            CreateZipFile(overWrite, fileZipPath, sourceFilePath, isEncrypt, password, compressionLevel,
                blockSize);

            return fileZipPath;
        }

        #endregion

        #region 压缩多个文件

        /// <summary>
        /// 压缩多个文件
        /// </summary>
        /// <param name="sourceFileList">多文件指定路径</param>
        /// <param name="zipDirectory">压缩包保存目录,默认与源文件在同一目录</param>
        /// <param name="zipName">压缩文件名称(不能为空)</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <param name="isEncrypt">是否加密</param>
        /// <param name="password">密码</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <param name="zipMaxFile">压缩包内最多文件数量(-1不限)</param>
        /// <param name="blockSize">缓存大小（每次写入文件大小，默认 2048）</param>
        /// <returns></returns>
        public override string[] CompressMulti(string[] sourceFileList, string zipDirectory, string zipName,
            bool overWrite = true, bool isEncrypt = false,
            string password = "", int compressionLevel = 5, int zipMaxFile = -1, int blockSize = 2048)
        {
            var index = 0;

            CheckSourceFilePathList(sourceFileList);
            CheckZipDirectory(zipDirectory);
            CheckZipName(zipName);

            List<string> zipPathList = new List<string>();
            string zipPath = GetCompressZipName("", zipDirectory, zipName);
            string ext = Path.GetExtension(zipPath) ?? "";
            sourceFileList.ToList().ListPager(sourceFiles =>
            {
                if (index > 0)
                {
                    zipPath = $"{zipName.Replace(ext, "")}_{index}{ext}";
                    zipPath = GetCompressZipName("", zipDirectory, zipPath);
                }

                CreateZipFile(overWrite, zipPath, sourceFiles.ToArray(), isEncrypt, password, compressionLevel,
                    blockSize);
                index++;
                zipPathList.Add(zipPath);
            }, zipMaxFile);

            return zipPathList.ToArray();
        }

        #endregion

        #region 压缩指定文件夹下的文件

        /// <summary>
        /// 压缩指定文件夹下的文件
        /// </summary>
        /// <param name="sourceFilePath">文件夹目录</param>
        /// <param name="zipDirectory">压缩包保存目录,默认与源文件在同一目录</param>
        /// <param name="zipName">压缩包保存目录</param>
        /// <param name="searchPattern">要与 path 中的文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
        /// <param name="searchOption">默认当前文件夹下 TopDirectoryOnly，若查询包含所有子目录为AllDirectories</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <param name="isEncrypt">是否加密</param>
        /// <param name="password">密码</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <param name="zipMaxFile">压缩包内最多文件数量(-1不限)</param>
        /// <param name="blockSize">缓存大小（每次写入文件大小，默认 2048）</param>
        /// <returns></returns>
        public override string[] CompressCatalogAndFiltrate(string sourceFilePath, string zipDirectory, string zipName,
            string searchPattern = "*.*",
            SearchOption searchOption = SearchOption.AllDirectories, bool overWrite = true, bool isEncrypt = false,
            string password = "", int compressionLevel = 5, int zipMaxFile = -1, int blockSize = 2048)
        {
            CheckZipSourceDirectory(sourceFilePath);
            CheckZipDirectory(zipDirectory);
            CheckZipName(zipName);

            var files = FileCommon.GetFiles(sourceFilePath, searchPattern, searchOption);
            return CompressMulti(files, zipDirectory, zipName, overWrite, isEncrypt, password, compressionLevel,
                zipMaxFile,
                blockSize);
        }

        #endregion

        #region 压缩指定文件夹（压缩为单个压缩包）

        /// <summary>
        /// 压缩指定文件夹（压缩为单个压缩包）
        /// </summary>
        /// <param name="sourceFilePath">文件夹目录</param>
        /// <param name="zipDirectory">压缩包保存目录,默认与源文件在同一目录</param>
        /// <param name="zipName">压缩文件名称</param>
        /// <param name="isRecursive">是否递归（默认递归）</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <param name="isEncrypt">是否加密</param>
        /// <param name="password">密码</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <returns></returns>
        public override string CompressCatalog(string sourceFilePath, string zipDirectory, string zipName,
            bool isRecursive = true, bool overWrite = true, bool isEncrypt = false,
            string password = "", int compressionLevel = 5)
        {
            string fileZipPath = GetCompressZipName(sourceFilePath, zipDirectory, zipName);

            CheckFile(fileZipPath, overWrite, () =>
            {
                using (FileStream zipFile = File.Create(fileZipPath))
                {
                    Encoding encoding = Encoding.UTF8;
                    ZipStrings.CodePage = encoding.CodePage;
                    using (ZipOutputStream stream = new ZipOutputStream(zipFile))
                    {
                        if (isEncrypt)
                        {
                            //压缩文件加密
                            stream.Password = password;
                        }

                        ZipSetp(sourceFilePath, stream, new FileInfo(sourceFilePath).DirectoryName,
                            isRecursive,
                            compressionLevel);
                    }
                }
            });

            return fileZipPath;
        }

        #endregion

        #region 文件压缩解压

        /// <summary>
        /// 文件压缩解压
        /// </summary>
        /// <param name="zipFile">压缩文件绝对地址</param>
        /// <param name="targetDirectory">解压目录</param>
        /// <param name="password">密码</param>
        /// <param name="overWrite">是否覆盖</param>
        public override void DeCompress(string zipFile, string targetDirectory, string password = "",
            bool overWrite = true)
        {
            //如果解压到的目录不存在，则报错
            if (!Directory.Exists(targetDirectory))
            {
                throw new FileNotFoundException($"指定的目录:{targetDirectory}不存在!");
            }

            //目录结尾
            if (!targetDirectory.EndsWith("\\"))
            {
                targetDirectory = targetDirectory + "\\";
            }

            Encoding encoding = Encoding.UTF8;
            ZipConstants.DefaultCodePage = encoding.CodePage;
            using (ZipInputStream zipfiles = new ZipInputStream(File.OpenRead(zipFile)))
            {
                zipfiles.Password = password;
                ZipEntry theEntry;

                while ((theEntry = zipfiles.GetNextEntry()) != null)
                {
                    string directoryName = "";
                    string pathToZip = "";
                    pathToZip = theEntry.Name;

                    if (pathToZip != "")
                        directoryName = Path.GetDirectoryName(pathToZip) + "\\";

                    string fileName = Path.GetFileName(pathToZip);

                    Directory.CreateDirectory(Path.Combine(targetDirectory, directoryName));

                    if (fileName != "")
                    {
                        if ((File.Exists(targetDirectory + directoryName + fileName) && overWrite) ||
                            (!File.Exists(targetDirectory + directoryName + fileName)))
                        {
                            using (FileStream streamWriter = File.Create(targetDirectory + directoryName + fileName))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = zipfiles.Read(data, 0, data.Length);

                                    if (size > 0)
                                        streamWriter.Write(data, 0, size);
                                    else
                                        break;
                                }

                                streamWriter.Close();
                            }
                        }
                    }
                }

                zipfiles.Close();
            }
        }

        #endregion
    }
}
