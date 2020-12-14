// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Tools.Common.Systems;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// 加密帮助类
    /// </summary>
    public class SecurityCommon
    {
        #region AES加密

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.AesEncrypt(key)")]
        public static string AesEncrypt(string str, string key, int? errCode = null)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;
            if (key.Length != 32)
            {
                throw new BusinessException("Aes秘钥异常", errCode ?? HttpStatus.Err.Id);
            }

            // 256-AES key
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        #endregion

        #region AES解密

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.AesDecrypt(key)")]
        public static string AesDecrypt(string str, string key, int? errCode = null)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;
            if (key.Length != 32)
            {
                throw new BusinessException("Aes秘钥异常", errCode ?? HttpStatus.Err.Id);
            }

            try
            {
                // 256-AES key
                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                byte[] toEncryptArray = Convert.FromBase64String(str);

                RijndaelManaged rDel = new RijndaelManaged
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                ICryptoTransform cTransform = rDel.CreateDecryptor();


                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return str;
            }
        }

        #endregion

        #region MD5加密

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="str">待加密字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isUpper">是否转大写</param>
        [Obsolete("方法已过时，建议使用：str.GetMd5HashBy16(key)")]
        public static string GetMd5HashBy16(string str, Encoding encoding = null, bool isUpper = true)
        {
            return GetMd5Hash(str, true, encoding, isUpper);
        }

        /// <summary>
        /// MD5加密(32位)
        /// </summary>
        /// <param name="str">待加密字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.GetMd5Hash(key)")]
        public static string GetMd5Hash(string str, Encoding encoding = null, bool isUpper = true)
        {
            return GetMd5Hash(str, false, encoding, isUpper);
        }

        /// <summary>
        /// 得到md5加密结果
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <param name="is16">是否16位加密，是否32位加密</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        private static string GetMd5Hash(string input, bool is16, Encoding encoding = null, bool isUpper = true)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var signed = GetMd5Provider().ComputeHash(encoding.GetBytes(input));
            string signResult = is16 ? GetSignResult(signed, 4, 8) : GetSignResult(signed);
            return isUpper ? signResult.ToUpper() : signResult.ToLower();
        }

        /// <summary>
        /// MD5加密方法
        /// startIndex为空为32位加密
        /// </summary>
        /// <param name="signed"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string GetSignResult(byte[] signed, int? startIndex = null, int? length = null)
        {
            return (startIndex == null
                ? BitConverter.ToString(signed)
                : BitConverter.ToString(signed, (int) startIndex, length ?? default(int))).Replace("-", "");
        }

        /// <summary>
        ///
        /// </summary>
        private static MD5 _md5CryptoServiceProvider;

        static SecurityCommon()
        {
            _md5CryptoServiceProvider = MD5.Create();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private static MD5 GetMd5Provider()
        {
            return _md5CryptoServiceProvider ?? (_md5CryptoServiceProvider = MD5.Create());
        }

        #endregion

        #region Des加密

        /// <summary>
        /// 对字符串进行DES加密
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>加密后的BASE64编码的字符串</returns>
        [Obsolete("方法已过时，建议使用：str.DesEncrypt(key,iv)")]
        public static string DesEncrypt(string str, string key, string iv)
        {
            byte[] btKey = Encoding.Default.GetBytes(key);
            byte[] btIv = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.Default.GetBytes(str);
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIv), CryptoStreamMode.Write))
                {
                    cs.Write(inData, 0, inData.Length);
                    cs.FlushFinalBlock();
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// 对DES加密后的字符串进行解密
        /// </summary>
        /// <param name="str">待解密的字符串</param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>解密后的字符串</returns>
        [Obsolete("方法已过时，建议使用：str.DesDecrypt(key,iv)")]
        public static string DesDecrypt(string str, string key, string iv)
        {
            byte[] btKey = Encoding.Default.GetBytes(key);
            byte[] btIv = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(str);
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIv), CryptoStreamMode.Write))
                {
                    cs.Write(inData, 0, inData.Length);
                    cs.FlushFinalBlock();
                }

                return Encoding.Default.GetString(ms.ToArray());
            }
        }

        #endregion

        #region Sha加密

        #region Sha1加密

        /// <summary>
        /// Sha1
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.Sha1(key)")]
        public static string Sha1(string str, bool isUpper = true)
        {
            var enc = new ASCIIEncoding(); //将mystr转换成byte[]
            using (var hashAlgorithm = new SHA1Managed())
            {
                return enc.GetBytes(str).GetSha(hashAlgorithm, isUpper);
            }
        }

        /// <summary>
        /// Sha1
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha1(Stream str, bool isUpper = true)
        {
            using (var hashAlgorithm = new SHA1Managed())
            {
                return str.ConvertToByteArray().GetSha(hashAlgorithm, isUpper);
            }
        }

        /// <summary>
        /// Sha1
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha1(byte[] str, bool isUpper = true)
        {
            using (var hashAlgorithm = new SHA1Managed())
            {
                return str.GetSha(hashAlgorithm, isUpper);
            }
        }

        #endregion

        #region Sha256加密

        /// <summary>
        /// Sha256
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.Sha256(key)")]
        public static string Sha256(string str, bool isUpper = true)
        {
            var enc = new ASCIIEncoding(); //将mystr转换成byte[]
            using (var hashAlgorithm = new SHA256Managed())
            {
                return enc.GetBytes(str).GetSha(hashAlgorithm, isUpper);
            }
        }

        /// <summary>
        /// Sha256
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha256(Stream str, bool isUpper = true)
        {
            using (var hashAlgorithm = new SHA256Managed())
            {
                return str.ConvertToByteArray().GetSha(hashAlgorithm, isUpper);
            }
        }

        /// <summary>
        /// Sha256
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha256(byte[] str, bool isUpper = true)
        {
            using (var hashAlgorithm = new SHA256Managed())
            {
                return str.GetSha(hashAlgorithm, isUpper);
            }
        }

        #endregion

        #region Sha384加密

        /// <summary>
        /// Sha384
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.Sha384(key)")]
        public static string Sha384(string str, bool isUpper = true)
        {
            var enc = new ASCIIEncoding(); //将mystr转换成byte[]
            using (var hashAlgorithm = new SHA384Managed())
            {
                return enc.GetBytes(str).GetSha(hashAlgorithm, isUpper);
            }
        }

        /// <summary>
        /// Sha384
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha384(Stream str, bool isUpper = true)
        {
            using (var hashAlgorithm = new SHA384Managed())
            {
                return str.ConvertToByteArray().GetSha(hashAlgorithm, isUpper);
            }
        }

        /// <summary>
        /// Sha384
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha384(byte[] str, bool isUpper = true)
        {
            using (var hashAlgorithm = new SHA384Managed())
            {
                return str.GetSha(hashAlgorithm, isUpper);
            }
        }

        #endregion

        #region Sha512加密

        /// <summary>
        /// Sha512加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.Sha512(key)")]
        public static string Sha512(string str, bool isUpper = true)
        {
            var enc = new ASCIIEncoding(); //将mystr转换成byte[]
            using (var hashAlgorithm = new SHA512Managed())
            {
                return enc.GetBytes(str).GetSha(hashAlgorithm, isUpper);
            }
        }

        /// <summary>
        /// Sha512加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha512(Stream str, bool isUpper = true)
        {
            using (var hashAlgorithm = new SHA512Managed())
            {
                return str.ConvertToByteArray().GetSha(hashAlgorithm, isUpper);
            }
        }

        /// <summary>
        /// Sha512加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha512(byte[] str, bool isUpper = true)
        {
            using (var hashAlgorithm = new SHA512Managed())
            {
                return str.GetSha(hashAlgorithm, isUpper);
            }
        }

        #endregion

        #endregion

        #region HMACSHA加密

        #region HMacSha1加密

        /// <summary>
        /// HMacSha1加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.HMacSha1(key)")]
        public static string HMacSha1(string str, string key)
        {
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key.SafeString());
            byte[] messageBytes = encoding.GetBytes(str);
            using (var hmacsha1 = new HMACSHA1(keyByte))
            {
                byte[] hashmessage = hmacsha1.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        #endregion

        #region HMacSha256

        /// <summary>
        /// HMacSha256
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.HMacSha256(key)")]
        public static string HMacSha256(string str, string key)
        {
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key.SafeString());
            byte[] messageBytes = encoding.GetBytes(str);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        #endregion

        #region HMacSha384

        /// <summary>
        /// HMacSha384
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.HMacSha384(key)")]
        public static string HMacSha384(string str, string key)
        {
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key.SafeString());
            byte[] messageBytes = encoding.GetBytes(str);
            using (var hmacsha384 = new HMACSHA384(keyByte))
            {
                byte[] hashmessage = hmacsha384.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        #endregion

        #region HMacSha512

        /// <summary>
        /// HMacSha512
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.HMacSha512(key)")]
        public static string HMacSha512(string str, string key)
        {
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key.SafeString());
            byte[] messageBytes = encoding.GetBytes(str);
            using (var hmacsha512 = new HMACSHA512(keyByte))
            {
                byte[] hashmessage = hmacsha512.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        #endregion

        #endregion

        #region Js Aes 加解密

        #region JS Aes解密

        /// <summary>
        /// JS Aes解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.JsAesDecrypt(key,iv)")]
        public static string JsAesDecrypt(string str, string key, string iv)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] ivArray = Encoding.UTF8.GetBytes(iv);
            byte[] cipherText = HexToByteArray(str);
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("str");
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            string plaintext = null;
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = keyArray;
                rijAlg.IV = ivArray;

                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        private static byte[] HexToByteArray(string hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        #endregion

        #region JS Aes 加密

        /// <summary>
        /// JsAesEncrypt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，建议使用：str.JsAesEncrypt(key,iv)")]
        public static string JsAesEncrypt(string plainText, string key, string iv)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] ivArray = Encoding.UTF8.GetBytes(iv);

            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }

            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            byte[] encrypted;
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = keyArray;
                rijAlg.IV = ivArray;

                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }

                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return ByteArrayToHex(encrypted);
        }

        private static string ByteArrayToHex(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        #endregion

        #endregion
    }
}
