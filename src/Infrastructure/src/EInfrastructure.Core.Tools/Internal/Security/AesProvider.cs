// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Security.Cryptography;
using System.Text;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools.Configuration;

namespace EInfrastructure.Core.Tools.Internal.Security
{
    /// <summary>
    /// Aes
    /// 加解密
    /// </summary>
    public class AesProvider : IdentifyDefault, ISecurityProvider
    {
        /// <summary>
        /// 加密方式
        /// </summary>
        public SecurityType Type => SecurityType.Aes;

        #region Aes加密

        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <param name="encryptInfos">秘钥信息</param>
        /// <returns>返回加密后的字符串</returns>
        public string Encrypt(string str, EncryptInfos encryptInfos)
        {
            Check(encryptInfos);
            var cryptoTransform = GetCryptoTransform(encryptInfos.Key.SafeString(), encryptInfos.Iv.SafeString(),
                CipherMode.ECB,
                PaddingMode.PKCS7, encryptInfos.Encoding, true);
            var toEncryptArray = str.ConvertToByteArray(encryptInfos.Encoding);
            return GetResult(str, cryptoTransform, toEncryptArray).ConvertToBase64(0);
        }

        #endregion

        #region Aes解密

        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="str">待解密的字符串</param>
        /// <param name="encryptInfos">秘钥信息</param>
        /// <returns>返回解密后的字符串</returns>
        public string Decrypt(string str, EncryptInfos encryptInfos)
        {
            Check(encryptInfos);
            var cryptoTransform = GetCryptoTransform(encryptInfos.Key.SafeString(), encryptInfos.Iv.SafeString(),
                CipherMode.ECB,
                PaddingMode.PKCS7, encryptInfos.Encoding, false);
            var toEncryptArray = str.ConvertToBase64ByteArray();
            return GetResult(str, cryptoTransform,toEncryptArray).ConvertToString(encryptInfos.Encoding);
        }

        #endregion

        #region private methods

        #region 校验秘钥

        /// <summary>
        /// 校验秘钥
        /// </summary>
        /// <param name="encryptInfos"></param>
        private void Check(EncryptInfos encryptInfos)
        {
            if (encryptInfos == null)
            {
                throw new ArgumentNullException(nameof(encryptInfos));
            }

            if (encryptInfos.Key.IsNullOrWhiteSpace())
            {
                throw new BusinessException("The Aes secret key cannot be empty");
            }

            if (encryptInfos.Key.SafeString().Length != 32)
            {
                throw new BusinessException("Aes secret key length must be 32 bits");
            }
        }

        #endregion

        #region 得到ICryptoTransform

        /// <summary>
        /// 得到ICryptoTransform
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <param name="encoding"></param>
        /// <param name="isEncrypt">是否加密，加密：true，解密：false</param>
        /// <returns></returns>
        private ICryptoTransform GetCryptoTransform(string key, string iv, CipherMode cipherMode,
            PaddingMode paddingMode, Encoding encoding, bool isEncrypt)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Key = key.ConvertToByteArray(encoding),
                Mode = cipherMode,
                Padding = paddingMode
            };
            if (!iv.IsNullOrWhiteSpace())
            {
                rijndaelManaged.IV = iv.ConvertToByteArray(encoding);
            }

            return isEncrypt ? rijndaelManaged.CreateEncryptor() : rijndaelManaged.CreateDecryptor();
        }

        #endregion

        #region 得到结果

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <param name="str">待加密/解密的字符串</param>
        /// <param name="cryptoTransform"></param>
        /// <param name="toEncryptArray"></param>
        /// <returns></returns>
        private byte[] GetResult(string str, ICryptoTransform cryptoTransform, byte[] toEncryptArray)
        {
            if (str.IsNullOrWhiteSpace())
            {
                return null;
            }

            return cryptoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        }

        #endregion

        #endregion
    }
}
