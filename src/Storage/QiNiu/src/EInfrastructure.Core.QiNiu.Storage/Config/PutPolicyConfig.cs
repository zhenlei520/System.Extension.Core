using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Interface.Storage.Param;
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
        /// <param name="opsParam">上传策略</param>
        internal void SetPutPolicy(UploadPersistentOpsParam opsParam)
        {
            GetPutPolicy();
            _putPolicy.SaveKey = opsParam.Key;

            #region 覆盖上传

            if (!opsParam.UploadPersistentOps.IsAllowOverlap)
            {
                _putPolicy.Scope = _qiNiuConfig.Bucket;
            }
            else
            {
                _putPolicy.Scope = _qiNiuConfig.Bucket + ":" + opsParam.Key;
            }

            #endregion

            #region 带数据处理的凭证

            if (!string.IsNullOrEmpty(opsParam.UploadPersistentOps.PersistentOps))
            {
                _putPolicy.PersistentOps = opsParam.UploadPersistentOps.PersistentOps;
            }

            #endregion

            #region 设置过期时间

            _putPolicy.SetExpires(opsParam.UploadPersistentOps.ExpireInSeconds);

            #endregion

            #region 多少天后自动删除

            _putPolicy.DeleteAfterDays = opsParam.UploadPersistentOps.DeleteAfterDays;

            #endregion

            _putPolicy.FileType = opsParam.UploadPersistentOps.FileType;
            _putPolicy.DetectMime = opsParam.UploadPersistentOps.DetectMime;
            if (!string.IsNullOrEmpty(opsParam.UploadPersistentOps.MimeLimit))
            {
                _putPolicy.MimeLimit = opsParam.UploadPersistentOps.MimeLimit;
            }

            _putPolicy.FsizeMin = opsParam.UploadPersistentOps.FsizeMin;
            _putPolicy.FsizeLimit = opsParam.UploadPersistentOps.FsizeLimit;

            if (!string.IsNullOrEmpty(opsParam.UploadPersistentOps.ReturnUrl))
            {
                _putPolicy.ReturnUrl = opsParam.UploadPersistentOps.ReturnUrl;
            }

            if (!string.IsNullOrEmpty(opsParam.UploadPersistentOps.ReturnBody))
            {
                _putPolicy.ReturnBody = opsParam.UploadPersistentOps.ReturnBody;
            }

            if (!string.IsNullOrEmpty(opsParam.UploadPersistentOps.CallbackUrl))
            {
                _putPolicy.CallbackUrl = opsParam.UploadPersistentOps.CallbackUrl;
            }

            if (!string.IsNullOrEmpty(opsParam.UploadPersistentOps.CallbackHost))
            {
                _putPolicy.CallbackHost = opsParam.UploadPersistentOps.CallbackHost;
            }

            if (!string.IsNullOrEmpty(opsParam.UploadPersistentOps.CallbackBody))
            {
                _putPolicy.CallbackBody = opsParam.UploadPersistentOps.CallbackBody;
            }

            if (!string.IsNullOrEmpty(opsParam.UploadPersistentOps.CallbackBodyType))
            {
                _putPolicy.CallbackBodyType = opsParam.UploadPersistentOps.CallbackBodyType;
            }

            if (!string.IsNullOrEmpty(opsParam.UploadPersistentOps.PersistentNotifyUrl))
            {
                _putPolicy.PersistentNotifyUrl = opsParam.UploadPersistentOps.PersistentNotifyUrl;
            }

            if (!string.IsNullOrEmpty(opsParam.UploadPersistentOps.PersistentPipeline))
            {
                _putPolicy.PersistentPipeline = opsParam.UploadPersistentOps.PersistentPipeline;
            }
        }

        #endregion
    }
}