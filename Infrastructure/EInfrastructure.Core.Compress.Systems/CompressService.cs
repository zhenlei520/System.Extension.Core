using System;
using System.IO;
using System.IO.Compression;
using EInfrastructure.Core.HelpCommon.Files;
using EInfrastructure.Core.Interface.Compress;
using FileInfo = System.IO.FileInfo;

namespace EInfrastructure.Core.Compress.Systems
{
    /// <summary>
    /// 系统压缩
    /// </summary>
    public class CompressService : ICompressService
    {
        #region 压缩文件

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFilePath">文件指定路径</param>
        /// <param name="zipDirectory">文件目录</param>
        /// <param name="zipName">压缩文件名称</param>
        /// <returns></returns>
        public string CompressSingle(string sourceFilePath, string zipDirectory, string zipName)
        {
            string fileZipPath = Path.Combine(zipDirectory, zipName);
            using (FileStream sourceFileStream = new FileInfo(sourceFilePath).OpenRead())
            {
                using (FileStream zipFileStream = File.Create(fileZipPath))
                {
                    using (GZipStream zipStream = new GZipStream(zipFileStream, CompressionMode.Compress))
                    {
                        sourceFileStream.CopyTo(zipStream);
                    }
                }
            }

            return fileZipPath;
        }

        #endregion

        #region 文件路径

        /// <summary>
        /// 文件路径
        /// </summary>
        /// <param name="sourceFileList">多文件指定路径</param>
        /// <param name="zipDirectory">文件目录</param>
        /// <param name="zipName">压缩文件名称</param>
        /// <returns></returns>
        public string CompressMulti(string[] sourceFileList, string zipDirectory, string zipName)
        {
            string fileZipPath = Path.Combine(zipDirectory, zipName);
            MemoryStream ms = new MemoryStream();
            foreach (string filePath in sourceFileList)
            {
                if (File.Exists(filePath))
                {
                    string fileName = Path.GetFileName(filePath);
                    if (string.IsNullOrEmpty(fileName))
                    {
                        continue;
                    }

                    byte[] fileNameBytes = System.Text.Encoding.UTF8.GetBytes(fileName);
                    byte[] sizeBytes = BitConverter.GetBytes(fileNameBytes.Length);
                    ms.Write(sizeBytes, 0, sizeBytes.Length);
                    ms.Write(fileNameBytes, 0, fileNameBytes.Length);
                    byte[] fileContentBytes = System.IO.File.ReadAllBytes(filePath);
                    ms.Write(BitConverter.GetBytes(fileContentBytes.Length), 0, 4);
                    ms.Write(fileContentBytes, 0, fileContentBytes.Length);
                }
            }

            ms.Flush();
            ms.Position = 0;
            using (FileStream zipFileStream = File.Create(fileZipPath))
            {
                using (GZipStream zipStream = new GZipStream(zipFileStream, CompressionMode.Compress))
                {
                    ms.Position = 0;
                    ms.CopyTo(zipStream);
                }
            }

            ms.Close();
            return fileZipPath;
        }

        #endregion

        #region 压缩指定文件夹下的文件

        /// <summary>
        /// 压缩指定文件夹下的文件
        /// </summary>
        /// <param name="sourceFilePath">文件夹目录</param>
        /// <param name="zipDirectory">文件目录</param>
        /// <param name="zipName">压缩文件名称</param>
        /// <param name="isRecursive">是否递归（默认递归）</param>
        /// <param name="searchPattern">要与 path 中的文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（* 和 ?）的组合（请参见“备注”），但不支持正则表达式。</param>
        /// <param name="searchOption">默认当前文件夹下 TopDirectoryOnly，若查询包含所有子目录为AllDirectories</param>
        /// <returns></returns>
        public string CompressCatalog(string sourceFilePath, string zipDirectory, string zipName,
            bool isRecursive = true, string searchPattern = "*.*",
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var files = FileCommon.GetFiles(sourceFilePath, searchPattern, searchOption);
            return CompressMulti(files, zipDirectory, zipName);
        }

        #endregion

        #region 文件压缩解压

        /// <summary>
        /// 文件压缩解压
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="targetPath">解压目录</param>
        public void DeCompress(string zipPath, string targetPath)
        {
            byte[] fileSize = new byte[4];
            if (File.Exists(zipPath))
            {
                using (FileStream fStream = File.Open(zipPath, FileMode.Open))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (GZipStream zipStream = new GZipStream(fStream, CompressionMode.Decompress))
                        {
                            zipStream.CopyTo(ms);
                        }

                        ms.Position = 0;
                        while (ms.Position != ms.Length)
                        {
                            ms.Read(fileSize, 0, fileSize.Length);
                            int fileNameLength = BitConverter.ToInt32(fileSize, 0);
                            byte[] fileNameBytes = new byte[fileNameLength];
                            ms.Read(fileNameBytes, 0, fileNameBytes.Length);
                            string fileName = System.Text.Encoding.UTF8.GetString(fileNameBytes);
                            string fileFulleName = targetPath + fileName;
                            ms.Read(fileSize, 0, 4);
                            int fileContentLength = BitConverter.ToInt32(fileSize, 0);
                            byte[] fileContentBytes = new byte[fileContentLength];
                            ms.Read(fileContentBytes, 0, fileContentBytes.Length);
                            using (FileStream childFileStream = File.Create(fileFulleName))
                            {
                                childFileStream.Write(fileContentBytes, 0, fileContentBytes.Length);
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}