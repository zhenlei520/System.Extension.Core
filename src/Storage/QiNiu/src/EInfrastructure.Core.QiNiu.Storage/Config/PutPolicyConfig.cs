using System.Linq;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params;
using EInfrastructure.Core.Tools;
using Qiniu.Storage;

namespace EInfrastructure.Core.QiNiu.Storage.Config
{
    /// <summary>
    /// 上传策略配置
    /// </summary>
    internal class PutPolicyConfig
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
            _putPolicy = new PutPolicy();
        }

        #region 得到上传策略

        private PutPolicy _putPolicy;

        /// <summary>
        /// 得到上传策略
        /// </summary>
        /// <returns></returns>
        internal PutPolicy GetPutPolicy()
        {
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
            _putPolicy.SaveKey = opsParam.Key;

            #region 覆盖上传

            if (opsParam.UploadPersistentOps.IsAllowOverlap == null ||
                !opsParam.UploadPersistentOps.IsAllowOverlap.Value)
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

            if (opsParam.UploadPersistentOps.EnableCallback!=null&&opsParam.UploadPersistentOps.EnableCallback.Value&&!string.IsNullOrEmpty(opsParam.UploadPersistentOps.CallbackHost) &&
                !string.IsNullOrEmpty(opsParam.UploadPersistentOps.CallbackUrl))
            {
                string url = opsParam.UploadPersistentOps.CallbackUrl.ConvertStrToList<string>(';')
                    .Select(x => opsParam.UploadPersistentOps.CallbackHost + "" + x).ConvertListToString(';');
                _putPolicy.CallbackUrl = url;
            }

            // if (!string.IsNullOrEmpty(opsParam.UploadPersistentOps.CallbackHost))
            // {
            //     _putPolicy.CallbackHost = opsParam.UploadPersistentOps.CallbackHost;
            // }

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
