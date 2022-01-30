// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.IO;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;

namespace EInfrastructure.Core.Compress.ICSharpCode
{
    /// <summary>
    /// 基类压缩方法
    /// </summary>
    public abstract class BaseCompressProvider
    {
        #region 压缩文件

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFilePath">文件指定路径</param>
        /// <param name="zipDirectory">压缩包保存目录,默认与源文件在同一目录</param>
        /// <param name="zipName">压缩文件名称，默认与源文件名称一致</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <param name="password">密码</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <param name="blockSize">缓存大小（每次写入文件大小，默认 2048）</param>
        /// <returns></returns>
        public virtual string CompressSingle(string sourceFilePath, string zipDirectory = "", string zipName = "",
            bool overWrite = true,
            string password = "", int compressionLevel = 5, int blockSize = 2048)
        {
            throw new BusinessException("不支持的压缩方式",HttpStatus.Err.Id);
        }

        #endregion

        #region 压缩多个文件

        /// <summary>
        /// 压缩多个文件
        /// </summary>
        /// <param name="sourceFileList">多文件指定路径</param>
        /// <param name="zipDirectory">压缩包保存目录</param>
        /// <param name="zipName">压缩文件名称(不能为空)</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <param name="password">密码</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <param name="zipMaxFile">压缩包内最多文件数量(-1：所有文件是一个压缩包)</param>
        /// <param name="blockSize">缓存大小（每次写入文件大小，默认 2048）</param>
        /// <returns></returns>
        public virtual string[] CompressMulti(
            string[] sourceFileList,
            string zipDirectory,
            string zipName,
            bool overWrite = true,
            string password = "",
            int compressionLevel = 5,
            int zipMaxFile = -1,
            int blockSize = 2048)
        {
            throw new BusinessException("不支持的压缩方式",HttpStatus.Err.Id);
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
        /// <param name="password">密码</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <param name="zipMaxFile">压缩包内最多文件数量(-1不限)</param>
        /// <param name="blockSize">缓存大小（每次写入文件大小，默认 2048）</param>
        /// <returns></returns>
        public virtual string[] CompressCatalogAndFiltrate(
            string sourceFilePath,
            string zipDirectory,
            string zipName,
            string searchPattern = "*.*",
            SearchOption searchOption = SearchOption.AllDirectories,
            bool overWrite = true,
            string password = "",
            int compressionLevel = 5,
            int zipMaxFile = -1,
            int blockSize = 2048)
        {
            throw new BusinessException("不支持的压缩方式",HttpStatus.Err.Id);
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
        /// <param name="password">密码</param>
        /// <param name="compressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
        /// <returns></returns>
        public virtual string CompressCatalog(
            string sourceFilePath,
            string zipDirectory,
            string zipName,
            bool isRecursive = true,
            bool overWrite = true,
            string password = "",
            int compressionLevel = 5)
        {
            throw new BusinessException("不支持的压缩方式",HttpStatus.Err.Id);
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
        public virtual void DeCompress(string zipFile, string targetDirectory, string password = "", bool overWrite = true)
        {
            throw new BusinessException("不支持的压缩方式",HttpStatus.Err.Id);
        }

        #endregion
    }
}
