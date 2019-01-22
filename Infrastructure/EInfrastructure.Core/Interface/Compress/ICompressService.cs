using System.IO;
using EInfrastructure.Core.Interface.IOC;

namespace EInfrastructure.Core.Interface.Compress
{
    /// <summary>
    /// 压缩文件
    /// </summary>
    public interface ICompressService : ISingleInstance
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFilePath">文件指定路径</param>
        /// <param name="zipDirectory">文件目录</param>
        /// <param name="zipName">压缩文件名称</param>
        /// <returns></returns>
        string CompressSingle(string sourceFilePath, string zipDirectory, string zipName);

        /// <summary>
        /// 文件路径
        /// </summary>
        /// <param name="sourceFileList">多文件指定路径</param>
        /// <param name="zipDirectory">文件目录</param>
        /// <param name="zipName">压缩文件名称</param>
        /// <returns></returns>
        string CompressMulti(string[] sourceFileList, string zipDirectory, string zipName);

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
        string CompressCatalog(string sourceFilePath, string zipDirectory, string zipName, bool isRecursive = true,
            string searchPattern = "*.*",
            SearchOption searchOption = SearchOption.TopDirectoryOnly);

        /// <summary>
        /// 文件压缩解压
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="targetPath">解压目录</param>
        void DeCompress(string zipPath, string targetPath);
    }
}