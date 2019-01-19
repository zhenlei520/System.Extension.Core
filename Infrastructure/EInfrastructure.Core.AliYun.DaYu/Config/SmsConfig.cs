using EInfrastructure.Core.AutoConfig.Interface;

namespace EInfrastructure.Core.AliYun.DaYu.Config
{
    /// <summary>
    /// 短信配置
    /// </summary>
    public class SmsConfig : IScopedConfigModel
    {
        /// <summary>
        /// 秘钥参数
        /// </summary>
        public string EncryptionKey { get; set; }
    }
}