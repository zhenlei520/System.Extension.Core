using EInfrastructure.Core.AutoConfig.Interface;
using EInfrastructure.Core.Exception;
using EInfrastructure.Core.HelpCommon.Serialization;

namespace EInfrastructure.Core.AliYun.DaYu.Config
{
    /// <summary>
    /// 短信配置
    /// </summary>
    public class AliSmsConfig
    {
        /// <summary>
        /// 签名名称
        /// </summary>
        public string SignName { get; set; }

        /// <summary>
        /// AccessKey ID
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// 秘钥参数
        /// </summary>
        public string EncryptionKey { get; set; }
    }
}