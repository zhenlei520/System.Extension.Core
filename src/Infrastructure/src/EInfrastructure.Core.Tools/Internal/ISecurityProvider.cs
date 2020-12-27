using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools.Configuration;

namespace EInfrastructure.Core.Tools.Internal
{
    /// <summary>
    ///
    /// </summary>
    public interface ISecurityProvider : ISingleInstance, IIdentify
    {
        /// <summary>
        /// 加密方式
        /// </summary>
        SecurityType Type { get; }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <param name="encryptInfos">秘钥信息</param>
        /// <returns>返回加密后的字符串</returns>
        string Encrypt(string str, EncryptInfos encryptInfos);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="str">待解密的字符串</param>
        /// <param name="encryptInfos">秘钥信息</param>
        /// <returns>返回解密后的字符串</returns>
        string Decrypt(string str, EncryptInfos encryptInfos);
    }
}
