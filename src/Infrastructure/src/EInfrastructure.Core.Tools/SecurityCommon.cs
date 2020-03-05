// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;

namespace EInfrastructure.Core.Tools
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
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        public static string AesEncrypt(string toEncrypt, string key, int? errCode = null)
        {
            if (string.IsNullOrWhiteSpace(toEncrypt))
                return string.Empty;
            if (key.Length != 32)
            {
                throw new BusinessException("Aes秘钥异常", errCode ?? HttpStatus.Err.Id);
            }

            // 256-AES key
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

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
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key, int? errCode = null)
        {
            if (string.IsNullOrWhiteSpace(toDecrypt))
                return string.Empty;
            if (key.Length != 32)
            {
                throw new BusinessException("Aes秘钥异常", errCode ?? HttpStatus.Err.Id);
            }

            try
            {
                // 256-AES key
                byte[] keyArray = Encoding.UTF8.GetBytes(key);
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

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
                return toDecrypt;
            }
        }

        #endregion

        #region MD5加密

        /// <summary>
        /// Md5加密，返回16位结果
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isUpper">是否转大写</param>
        public static string GetMd5HashBy16(string input, Encoding encoding = null, bool isUpper = true)
        {
            return GetMd5Hash(input, true, encoding, isUpper);
        }

        /// <summary>
        /// MD5加密(32位)
        /// </summary>
        /// <param name="input">待加密字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string GetMd5Hash(string input, Encoding encoding = null, bool isUpper = true)
        {
            return GetMd5Hash(input, false, encoding, isUpper);
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

            MD5 myMd5 = new MD5CryptoServiceProvider();
            var signed = myMd5.ComputeHash(encoding.GetBytes(input));
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

        #endregion

        #region Des加密

        /// <summary>
        /// 对字符串进行DES加密
        /// </summary>
        /// <param name="sourceString">待加密的字符串</param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>加密后的BASE64编码的字符串</returns>
        public static string DesEncrypt(string sourceString, string key, string iv)
        {
            byte[] btKey = Encoding.Default.GetBytes(key);
            byte[] btIv = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.Default.GetBytes(sourceString);
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
        /// <param name="encryptedString">待解密的字符串</param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns>解密后的字符串</returns>
        public static string DesDecrypt(string encryptedString, string key, string iv)
        {
            byte[] btKey = Encoding.Default.GetBytes(key);
            byte[] btIv = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
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
        public static string Sha1(string str, bool isUpper = true)
        {
            var enc = new ASCIIEncoding(); //将mystr转换成byte[]
            return GetSha(enc.GetBytes(str), new SHA1CryptoServiceProvider(), isUpper);
        }

        /// <summary>
        /// Sha1
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha1(Stream str, bool isUpper = true)
        {
            return GetSha(str.ConvertToByteArray(), new SHA1CryptoServiceProvider(), isUpper);
        }

        /// <summary>
        /// Sha1
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha1(byte[] str, bool isUpper = true)
        {
            return GetSha(str, new SHA1CryptoServiceProvider(), isUpper);
        }

        #endregion

        #region Sha256加密

        /// <summary>
        /// Sha256
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha256(string str, bool isUpper = true)
        {
            var enc = new ASCIIEncoding(); //将mystr转换成byte[]
            return GetSha(enc.GetBytes(str), new SHA256CryptoServiceProvider(), isUpper);
        }

        /// <summary>
        /// Sha256
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha256(Stream str, bool isUpper = true)
        {
            return GetSha(str.ConvertToByteArray(), new SHA256CryptoServiceProvider(), isUpper);
        }

        /// <summary>
        /// Sha256
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha256(byte[] str, bool isUpper = true)
        {
            return GetSha(str, new SHA256CryptoServiceProvider(), isUpper);
        }

        #endregion

        #region Sha384加密

        /// <summary>
        /// Sha384
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha384(string str, bool isUpper = true)
        {
            var enc = new ASCIIEncoding(); //将mystr转换成byte[]
            return GetSha(enc.GetBytes(str), new SHA384CryptoServiceProvider(), isUpper);
        }

        /// <summary>
        /// Sha384
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha384(Stream str, bool isUpper = true)
        {
            return GetSha(str.ConvertToByteArray(), new SHA384CryptoServiceProvider(), isUpper);
        }

        /// <summary>
        /// Sha384
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha384(byte[] str, bool isUpper = true)
        {
            return GetSha(str, new SHA384CryptoServiceProvider(), isUpper);
        }

        #endregion

        #region Sha512加密

        /// <summary>
        /// Sha512加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha512(string str, bool isUpper = true)
        {
            var enc = new ASCIIEncoding(); //将mystr转换成byte[]
            return GetSha(enc.GetBytes(str), new SHA512CryptoServiceProvider(), isUpper);
        }

        /// <summary>
        /// Sha512加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha512(Stream str, bool isUpper = true)
        {
            return GetSha(str.ConvertToByteArray(), new SHA512CryptoServiceProvider(), isUpper);
        }

        /// <summary>
        /// Sha512加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string Sha512(byte[] str, bool isUpper = true)
        {
            return GetSha(str, new SHA512CryptoServiceProvider(), isUpper);
        }

        #endregion

        #region 得到sha系列加密信息

        /// <summary>
        /// 得到sha系列加密信息
        /// </summary>
        /// <param name="retval"></param>
        /// <param name="hashAlgorithm"></param>
        /// <param name="isUpper">是否转大写</param>
        /// <returns></returns>
        public static string GetSha(byte[] retval, HashAlgorithm hashAlgorithm, bool isUpper)
        {
            var data = hashAlgorithm.ComputeHash(retval);
            StringBuilder sc = new StringBuilder();
            foreach (var t in data)
            {
                sc.Append(isUpper ? t.ToString("X2") : t.ToString("x2"));
            }

            return sc.ToString();
        }

        /// <summary>
        /// 得到sha系列加密信息
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="hashAlgorithm">加密方式</param>
        /// <param name="isUpper">是否大写</param>
        /// <returns></returns>
        public static string GetSha(Stream stream, HashAlgorithm hashAlgorithm, bool isUpper = true)
        {
            if (stream == null)
            {
                throw new BusinessException("FileStream is Null");
            }

            byte[] retval = hashAlgorithm.ComputeHash(stream);
            stream.Close();
            return SecurityCommon.GetSha(retval, hashAlgorithm, isUpper);
        }

        #endregion

        #endregion

        #region HMACSHA加密

        #region HMacSha1加密

        /// <summary>
        /// HMacSha1加密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string HMacSha1(string text, string key)
        {
            key = key ?? "";
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(text);
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
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string HMacSha256(string text, string key)
        {
            key = key ?? "";
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(text);
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
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string HMacSha384(string text, string key)
        {
            key = key ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(text);
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
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string HMacSha512(string text, string key)
        {
            key = key ?? "";
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(text);
            using (var hmacsha512 = new HMACSHA512(keyByte))
            {
                byte[] hashmessage = hmacsha512.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        #endregion

        #endregion

        #region JS Aes解密

        /// <summary>
        /// JS Aes解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string JsAesDecrypt(string toDecrypt, string key, string iv)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            byte[] ivArray = Encoding.UTF8.GetBytes(iv);
            byte[] cipherText = HexToByteArray(toDecrypt);
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("toDecrypt");
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
    }
}
