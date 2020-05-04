using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage
{
    /// <summary>
    /// 下载文件流(根据可访问的地址)
    /// </summary>
    public class FileDownloadStreamParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="url">文件访问地址（有权限可以访问的地址，如果你知道key的话，也可通过获取访问地址先获取到文件访问地址）</param>
        /// <param name="persistentOps">策略</param>
        public FileDownloadStreamParam(string url, BasePersistentOps persistentOps=null)
        {
            Url = url;
            PersistentOps = persistentOps??new BasePersistentOps();
        }

        /// <summary>
        /// 文件绝对地址（可访问，有授权）
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }

    /// <summary>
    /// 下载文件流(根据文件key)
    /// </summary>
    public class FileDownloadStreamByKeyParam
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="key">文件key</param>
        /// <param name="persistentOps">策略</param>
        public FileDownloadStreamByKeyParam(string key, BasePersistentOps persistentOps)
        {
            Key = key;
            PersistentOps = persistentOps;
        }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }
}
