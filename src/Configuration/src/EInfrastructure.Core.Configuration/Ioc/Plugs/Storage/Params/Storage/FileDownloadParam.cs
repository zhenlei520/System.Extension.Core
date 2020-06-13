using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 文件下载（根据文件地址，可访问，有授权）
    /// </summary>
    public class FileDownloadParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="url">文件访问地址（有权限可以访问的地址，如果你知道key的话，也可通过获取访问地址先获取到文件访问地址）</param>
        /// <param name="savePath">保存文件</param>
        /// <param name="persistentOps">策略</param>
        public FileDownloadParam(string url, string savePath,BasePersistentOps persistentOps)
        {
            Url = url;
            SavePath = savePath;
            PersistentOps = persistentOps;
        }

        /// <summary>
        /// 文件绝对地址（可访问，有授权）
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// 保存文件地址
        /// </summary>
        public string SavePath { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }

    /// <summary>
    /// 文件下载（根据文件key）
    /// </summary>
    public class FileDownloadByKeyParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key">文件key，非访问地址</param>
        /// <param name="savePath">保存文件</param>
        /// <param name="persistentOps">策略</param>
        public FileDownloadByKeyParam(string key, string savePath,BasePersistentOps persistentOps)
        {
            Key = key;
            SavePath = savePath;
            PersistentOps = persistentOps;
        }

        /// <summary>
        /// 文件key，非访问地址
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 保存文件地址
        /// </summary>
        public string SavePath { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }
}
