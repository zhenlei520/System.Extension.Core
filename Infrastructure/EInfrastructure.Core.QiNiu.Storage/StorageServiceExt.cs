using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Enum;
using Qiniu.Storage;
using Qiniu.Util;

namespace EInfrastructure.Core.QiNiu.Storage
{
    /// <summary>
    /// 存储
    /// </summary>
    public partial class StorageService
    {
        /// <summary>
        /// 存储配置文件
        /// </summary>
        internal QiNiuConfig QiNiuConfig;

        /// <summary>
        /// 七牛配置文件
        /// </summary>
        internal Qiniu.Storage.Config Config;

        /// <summary>
        /// 
        /// </summary>
        internal Mac Mac;

        /// <summary>
        /// 
        /// </summary>
        internal BucketManager BucketManager;

        /// <summary>
        /// 
        /// </summary>
        internal OperationManager OperationManager;

        internal PutPolicy PutPolicy = null;

        private Zone GetZone(ZoneEnum zone)
        {
            switch (zone)
            {
                default:
                    return Zone.ZONE_CN_East;
                case ZoneEnum.ZoneCnNorth:
                    return Zone.ZONE_CN_North;
                case ZoneEnum.ZoneCnSouth:
                    return Zone.ZONE_CN_South;
                case ZoneEnum.ZoneUsNorth:
                    return Zone.ZONE_US_North;
            }
        }

        #region 得到上传策略

        /// <summary>
        /// 得到上传策略
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isAllowOverlap">是否允许覆盖上传</param>
        /// <param name="persistentOps">上传预转持久化</param>
        /// <param name="expireInSeconds">上传策略失效时刻</param>
        /// <param name="deleteAfterDays">文件上传后多少天后自动删除</param>
        /// <returns></returns>
        private void SetPutPolicy(string key, bool isAllowOverlap = false, string persistentOps="", int expireInSeconds = 3600, int? deleteAfterDays = null)
        {
            if (PutPolicy != null)
            {
                return;
            }
            PutPolicy = new PutPolicy();
            if (isAllowOverlap)
            {
                PutPolicy.Scope = QiNiuConfig.Bucket;
            }
            else
            {
                PutPolicy.Scope = QiNiuConfig.Bucket + ":" + key;
            }
            if (!string.IsNullOrEmpty(persistentOps))
            {
                PutPolicy.PersistentOps = persistentOps;
            }
            if (!string.IsNullOrEmpty(QiNiuConfig.PersistentPipeline))
            {
                PutPolicy.PersistentPipeline = QiNiuConfig.PersistentPipeline;
            }
            if (!string.IsNullOrEmpty(QiNiuConfig.PersistentNotifyUrl))
            {
                PutPolicy.PersistentNotifyUrl = QiNiuConfig.PersistentNotifyUrl;
            }
            if (!string.IsNullOrEmpty(QiNiuConfig.CallbackBody))
            {
                PutPolicy.CallbackBody = QiNiuConfig.CallbackBody;
                PutPolicy.CallbackBodyType = QiNiuConfig.CallbackBodyType;
            }
            PutPolicy.SetExpires(expireInSeconds);
            PutPolicy.DeleteAfterDays = deleteAfterDays;
        }
        #endregion
    }
}
