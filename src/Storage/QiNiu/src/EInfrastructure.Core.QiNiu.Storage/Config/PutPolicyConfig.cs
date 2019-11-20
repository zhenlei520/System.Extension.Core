using EInfrastructure.Core.HelpCommon;
using Qiniu.Storage;

namespace EInfrastructure.Core.QiNiu.Storage.Config
{
    /// <summary>
    /// 上传策略配置
    /// </summary>
    public class PutPolicyConfig
    {
        /// <summary>
        /// 七牛自定义配置
        /// </summary>
        private readonly QiNiuStorageConfig _qiNiuConfig;

        /// <summary>
        ///
        /// </summary>
        /// <param name="qiNiuConfig">七牛自定义配置</param>
        public PutPolicyConfig(QiNiuStorageConfig qiNiuConfig)
        {
            _qiNiuConfig = qiNiuConfig;
        }

        #region 得到上传策略

        private PutPolicy _putPolicy;

        /// <summary>
        /// 得到上传策略
        /// </summary>
        /// <returns></returns>
        internal PutPolicy GetPutPolicy()
        {
            if (_putPolicy == null)
            {
                _putPolicy = new PutPolicy();
                if (!string.IsNullOrEmpty(_qiNiuConfig.CallbackBody))
                {
                    _putPolicy.PersistentNotifyUrl = _qiNiuConfig.PersistentNotifyUrl;
                    _putPolicy.CallbackBody = _qiNiuConfig.CallbackBody;
                    _putPolicy.CallbackBodyType = _qiNiuConfig.CallbackBodyType.GetDescription();
                    _putPolicy.CallbackUrl = _qiNiuConfig.CallbackUrl;
                }

                if (!string.IsNullOrEmpty(_qiNiuConfig.PersistentPipeline))
                {
                    _putPolicy.PersistentPipeline = _qiNiuConfig.PersistentPipeline;
                }
            }

            return _putPolicy;
        }

        #endregion

        #region 设置上传策略

        /// <summary>
        /// 设置上传策略
        /// </summary>
        /// <param name="key">文件名</param>
        /// <param name="isAllowOverlap">是否允许覆盖上传</param>
        /// <param name="persistentOps">上传预转持久化</param>
        /// <param name="expireInSeconds">上传策略失效时刻</param>
        /// <param name="deleteAfterDays">文件上传后多少天后自动删除</param>
        internal void SetPutPolicy(string key, bool isAllowOverlap = false,
            string persistentOps = "",
            int expireInSeconds = 3600, int? deleteAfterDays = null)
        {
            GetPutPolicy();
            _putPolicy.SaveKey = key;

            #region 覆盖上传

            if (!isAllowOverlap)
            {
                _putPolicy.Scope = _qiNiuConfig.Bucket;
            }
            else
            {
                _putPolicy.Scope = _qiNiuConfig.Bucket + ":" + key;
            }

            #endregion

            #region 带数据处理的凭证

            if (!string.IsNullOrEmpty(persistentOps))
            {
                _putPolicy.PersistentOps = persistentOps;
            }

            #endregion

            #region 设置过期时间

            _putPolicy.SetExpires(expireInSeconds);

            #endregion

            #region 多少天后自动删除

            _putPolicy.DeleteAfterDays = deleteAfterDays;

            #endregion
        }

        #endregion
    }
}
