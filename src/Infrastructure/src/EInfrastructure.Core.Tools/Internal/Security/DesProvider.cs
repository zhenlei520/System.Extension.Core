// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools.Configuration;

namespace EInfrastructure.Core.Tools.Internal.Security
{
    /// <summary>
    /// Des加解密
    /// </summary>
    public class DesProvider : IdentifyDefault, ISecurityProvider
    {
        /// <summary>
        /// 默认Iv
        /// </summary>
        private static byte[] Iv= { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// 加密方式
        /// </summary>
        public SecurityType Type => SecurityType.Des;

        /// <summary>
        /// Des加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encryptInfos"></param>
        /// <returns></returns>
        public string Encrypt(string str, EncryptInfos encryptInfos)
        {
            Check(encryptInfos);
            var cryptoTransform = GetCryptoTransform(encryptInfos.Key.SafeString(), encryptInfos.Iv.SafeString(),
                CipherMode.CBC,
                PaddingMode.PKCS7, encryptInfos.Encoding, true);
            var toEncryptArray = str.ConvertToByteArray(encryptInfos.Encoding);
            return GetResult(str, cryptoTransform, toEncryptArray).ConvertToBase64();
        }

        /// <summary>
        /// Des解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encryptInfos"></param>
        /// <returns></returns>
        public string Decrypt(string str, EncryptInfos encryptInfos)
        {
            Check(encryptInfos);
            var cryptoTransform = GetCryptoTransform(encryptInfos.Key.SafeString(), encryptInfos.Iv.SafeString(),
                CipherMode.CBC,
                PaddingMode.PKCS7, encryptInfos.Encoding, false);
            var toEncryptArray = str.ConvertToBase64ByteArray();
            return GetResult(str, cryptoTransform, toEncryptArray).ConvertToString(encryptInfos.Encoding);
        }


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
                throw new BusinessException("The Des secret key cannot be empty");
            }

            if (encryptInfos.Key.SafeString().Length != 8)
            {
                throw new BusinessException("Des secret key length must be 8 bits");
            }

            if (!encryptInfos.Iv.IsNullOrWhiteSpace() && encryptInfos.Iv.SafeString().Length != 8)
            {
                throw new BusinessException("Des Iv Is Empty Or Iv length must be 8 bits");
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
            DESCryptoServiceProvider des = new DESCryptoServiceProvider()
            {
                Key = key.ConvertToByteArray(encoding),
                IV = iv.IsNullOrWhiteSpace()?Iv:iv.ConvertToByteArray(encoding),
                Mode = cipherMode,
                Padding = paddingMode
            };
            if (!iv.IsNullOrWhiteSpace())
            {
                des.IV=iv.ConvertToByteArray(encoding);
            }

            return isEncrypt ? des.CreateEncryptor() : des.CreateDecryptor();
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
        private byte[] GetResult(string str, ICryptoTransform cryptoTransform,byte[] toEncryptArray)
        {
            if (str.IsNullOrWhiteSpace())
            {
                return null;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
                {
                    cs.Write(toEncryptArray, 0, toEncryptArray.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        #endregion

        #endregion
    }
}
