using Aliyun.OSS;
using Aliyun.OSS.Util;
using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Validation.Common;

namespace EInfrastructure.Core.Aliyun.Storage
{
    public class BaseStorageProvider
    {
        /// <summary>
        /// 存储配置文件
        /// </summary>
        private readonly ALiYunStorageConfig _aLiYunStorageConfig;

        /// <summary>
        ///
        /// </summary>
        public BaseStorageProvider(ALiYunStorageConfig aLiYunStorageConfig)
        {
            _aLiYunStorageConfig = aLiYunStorageConfig;
            aLiYunStorageConfig.Check("阿里云存储配置异常", HttpStatus.Err.Name);
        }

        #region 得到上传策略

        /// <summary>
        /// 得到上传策略
        /// </summary>
        /// <param name="uploadPersistentOps">上传策略</param>
        /// <returns></returns>
        protected virtual UploadPersistentOps GetUploadPersistentOps(UploadPersistentOps uploadPersistentOps)
        {
            if (uploadPersistentOps == null)
            {
                uploadPersistentOps = new UploadPersistentOps();
            }

            if ((uploadPersistentOps.EnableCallback == null && _aLiYunStorageConfig.EnableCallback) ||
                uploadPersistentOps.EnableCallback == true)
            {
                if (uploadPersistentOps.EnableCallback == null && _aLiYunStorageConfig.EnableCallback)
                {
                    uploadPersistentOps.SetCallBack(Configuration.Ioc.Plugs.Storage.Enumerations
                                                        .CallbackBodyType
                                                        .FromValue<EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.
                                                            Enumerations.CallbackBodyType>(_aLiYunStorageConfig
                                                            .CallbackBodyType)
                                                        ?.Name ??
                                                    Configuration.Ioc.Plugs.Storage.Enumerations
                                                        .CallbackBodyType.Json.Name, _aLiYunStorageConfig.CallbackHost,
                        _aLiYunStorageConfig.CallbackUrl, _aLiYunStorageConfig.CallbackBody);
                }
                else
                {
                    uploadPersistentOps.SetCallBack(uploadPersistentOps.CallbackBodyType,
                        uploadPersistentOps.CallbackHost, uploadPersistentOps.CallbackUrl,
                        uploadPersistentOps.CallbackBody);
                }
            }

            return uploadPersistentOps;
        }

        #endregion

        #region 获得ObjectMetadata

        /// <summary>
        /// 获得ObjectMetadata
        /// </summary>
        /// <param name="uploadPersistentOps"></param>
        /// <returns></returns>
        protected ObjectMetadata GetCallbackMetadata(UploadPersistentOps uploadPersistentOps)
        {
            if (uploadPersistentOps.EnableCallback != null && uploadPersistentOps.EnableCallback.Value &&
                !string.IsNullOrEmpty(uploadPersistentOps.CallbackBody) &&
                !string.IsNullOrEmpty(uploadPersistentOps.CallbackUrl))
            {
                return GetCallbackMetadata(uploadPersistentOps.CallbackUrl, uploadPersistentOps.CallbackBody);
            }

            return null;
        }

        /// <summary>
        /// 获得ObjectMetadata
        /// </summary>
        /// <param name="callbackUrl">回调地址</param>
        /// <param name="callbackBody"></param>
        /// <returns></returns>
        protected ObjectMetadata GetCallbackMetadata(string callbackUrl, string callbackBody)
        {
            string callbackHeaderBuilder = new CallbackHeaderBuilder(callbackUrl, callbackBody).Build();
            string callbackVariableHeaderBuilder = new CallbackVariableHeaderBuilder().Build();
            var metadata = new ObjectMetadata();
            metadata.AddHeader(HttpHeaders.Callback, callbackHeaderBuilder);
            metadata.AddHeader(HttpHeaders.CallbackVar, callbackVariableHeaderBuilder);
            return metadata;
        }

        #endregion
    }
}
