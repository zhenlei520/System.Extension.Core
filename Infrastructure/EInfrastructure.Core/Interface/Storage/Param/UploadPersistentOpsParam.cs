using EInfrastructure.Core.Interface.Storage.Config;

namespace EInfrastructure.Core.Interface.Storage.Param
{
    /// <summary>
    /// 上传文件（前端上传，后台生成策略信息）
    /// </summary>
    public class UploadPersistentOpsParam
    {
        public UploadPersistentOpsParam(string key, UploadPersistentOps uploadPersistentOps = null)
        {
            Key = key;
            UploadPersistentOps = uploadPersistentOps;
        }

        /// <summary>
        /// 待上传文件名称
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 上传策略
        /// </summary>
        public UploadPersistentOps UploadPersistentOps { get; private set; }
    }
}