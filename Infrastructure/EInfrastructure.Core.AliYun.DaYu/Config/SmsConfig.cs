using EInfrastructure.Core.AutoConfig.Interface;
using EInfrastructure.Core.Exception;
using EInfrastructure.Core.HelpCommon.Serialization;

namespace EInfrastructure.Core.AliYun.DaYu.Config
{
    /// <summary>
    /// 短信配置
    /// </summary>
    public class SmsConfig
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

        /// <summary>
        /// 是否第一次获取
        /// </summary>
        private static bool IsFirst = true;

        /// <summary>
        /// 短信配置
        /// </summary>
        private static SmsConfig Config = new SmsConfig();

        /// <summary>
        /// 设置短信配置
        /// </summary>
        public void Set()
        {
            Config = this;
        }

        /// <summary>
        /// 读取短信配置
        /// </summary>
        /// <returns></returns>
        internal static SmsConfig Get(string smsConfigJson = "")
        {
            if (string.IsNullOrEmpty(smsConfigJson))
            {
                if (Config.Equals(new SmsConfig()) && !IsFirst)
                {
                    throw new BusinessException("未配置短信");
                }

                IsFirst = false;
                return Config;
            }

            return new JsonCommon().Deserialize<SmsConfig>(smsConfigJson, null,
                (System.Exception ex) => throw new BusinessException("短信配置异常"));
        }
    }
}