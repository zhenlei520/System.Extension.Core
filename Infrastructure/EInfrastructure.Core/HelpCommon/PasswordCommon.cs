using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 加密方法帮助类
    /// 此类严格执行(方法设置>初始化预设>系统预设)的规则
    /// </summary>
    public static class PasswordCommon
    {

        #region 私有属性

        /// <summary>
        /// 是否返回为加密后字符的Byte代码
        /// </summary>
        private static bool IsReturnNum => true;

        #region AES

        /// <summary>
        /// 密钥 
        /// </summary>
        private static string AesEncryptionKey { get; set; }

        #endregion

        #region DES

        /// <summary>
        ///预设Des密钥向量
        /// </summary>
        private static readonly byte[] DesEncryptionKeyPresent = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        #endregion

        #endregion

        #region 设置aes秘钥的值
        /// <summary>
        /// 设置aes秘钥的值
        /// </summary>
        /// <param name="aesEncryptionKey">密钥</param>
        public static void SetAesEncryptionKey(string aesEncryptionKey)
        {
            AesEncryptionKey = aesEncryptionKey;
        }
        #endregion

        #region 得到字符串

        /// <summary>
        /// 得到字符串
        /// </summary>
        /// <param name="Byte"></param>
        /// <param name="isReturnNum">是否返回为加密后字符的Byte代码</param>
        /// <returns></returns>
        private static string GetStringValue(byte[] Byte, bool isReturnNum = false)
        {
            string tmpString = "";
            if (isReturnNum == false)
            {
                ASCIIEncoding asc = new ASCIIEncoding();
                tmpString = asc.GetString(Byte);
            }
            else
            {
                int iCounter;
                for (iCounter = 0; iCounter < Byte.Length; iCounter++)
                {
                    tmpString = tmpString + Byte[iCounter];
                }
            }
            return tmpString;
        }
        #endregion

        #region 得到字节数组
        /// <summary>
        /// 得到字节数组
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        private static byte[] GetKeyByteArray(string strKey)
        {
            ASCIIEncoding asc = new ASCIIEncoding();
            var tmpByte = asc.GetBytes(strKey);
            return tmpByte;
        }
        #endregion

        #region 是否返回为加密后字符的Byte代码
        /// <summary>
        /// 是否返回为加密后字符的Byte代码
        /// </summary>
        public static bool FindIsReturnNum(bool? isReturnNum = null)
        {
            if (isReturnNum != null)
                return (bool)isReturnNum;
            return IsReturnNum;
        }

        #endregion

        #region Aes加密

        #region 得到密钥
        /// <summary>
        /// 得到密钥
        /// </summary>
        /// <param name="key">用户输入密钥</param>
        /// <returns></returns>
        private static string FindEncryptKey(string key)
        {
            if (!string.IsNullOrEmpty(key))
                return key;//用户输入秘钥
            if (!string.IsNullOrEmpty(AesEncryptionKey))
                return AesEncryptionKey;
            throw new System.Exception("未配置aes秘钥AesEncryptionKey的值");
        }
        #endregion

        #region 加密，需要关键字

        /// <summary>
        /// 加密，需要关键字
        /// </summary>
        /// <param name="toEncrypt">待加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string ToEncryptAes(this string toEncrypt, string key = "")
        {
            if (String.IsNullOrWhiteSpace(toEncrypt))
                return "";
            string encryptionKey = FindEncryptKey(key);//密钥
            try
            {
                byte[] keyArray = Encoding.UTF8.GetBytes(encryptionKey);
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
                RijndaelManaged rDel = new RijndaelManaged
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                if (resultArray.Length % 8 != 0)
                {
                    return toEncrypt;
                }

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return toEncrypt;
            }
        }
        #endregion

        #region 解密，需要AES关键字
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ToDecryptAes(this string toDecrypt, string key = "")
        {
            if (String.IsNullOrWhiteSpace(toDecrypt))
                return "";
            string encryptionKey = FindEncryptKey(key);//密钥
            try
            {
                // 256-AES key    
                byte[] keyArray = Encoding.UTF8.GetBytes(encryptionKey);
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

        #endregion

        #region Des加密
        /// <summary>
        /// 得到Des密钥向量
        /// </summary>
        /// <param name="encryptionKey">用户输入密钥向量</param>
        /// <returns></returns>
        private static byte[] FindDesEncryptionKey(byte[] encryptionKey)
        {
            if (encryptionKey != null)
                return encryptionKey;
            return DesEncryptionKeyPresent;
        }

        #region DES加密字符串

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <param name="key">加密密钥(8位)</param>
        /// <param name="encryptionKey">密钥向量</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string ToEncryptDes(this string str, string key, byte[] encryptionKey = null)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] rgbIv = FindDesEncryptionKey(encryptionKey);
                byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
                DESCryptoServiceProvider dCsp = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());

            }
            catch
            {
                return str;
            }

        }
        #endregion

        #region DES解密字符串
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="str">待解密的字符串</param>
        /// <param name="key"> 解密密钥(8位),和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        /// <param name="encryptionKey">密钥向量</param>
        public static string ToDecryptDes(this string str, string key, byte[] encryptionKey = null)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));

                byte[] rgbIv = FindDesEncryptionKey(encryptionKey);

                byte[] inputByteArray = Convert.FromBase64String(str);

                DESCryptoServiceProvider dcsp = new DESCryptoServiceProvider();

                MemoryStream mStream = new MemoryStream();

                CryptoStream cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write);

                cStream.Write(inputByteArray, 0, inputByteArray.Length);

                cStream.FlushFinalBlock();

                return Encoding.UTF8.GetString(mStream.ToArray());

            }
            catch
            {
                return str;
            }

        }
        #endregion

        #endregion

        #region MD5加密

        /// <summary>
        ///  MD5加密
        /// </summary>
        /// <param name="pPassStr"></param>
        /// <param name="isLower">是否转为小写</param>
        /// <returns></returns>
        public static string ToMd5Math(this string pPassStr, bool isLower = true)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashByte = md5.ComputeHash(Encoding.UTF8.GetBytes(pPassStr));
            StringBuilder sb = new StringBuilder();
            if (isLower)
                foreach (byte item in hashByte)
                    sb.Append(item.ToString("x").PadLeft(2, '0'));
            else
                foreach (byte item in hashByte)
                    sb.Append(item.ToString("X").PadLeft(2, '0'));
            return sb.ToString();
        }
        #endregion

        #region SHA1加密
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string ToSha1(this string parameter)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytesOldString = Encoding.Default.GetBytes(parameter);
            byte[] bytesNewString = sha1.ComputeHash(bytesOldString);
            string newString = BitConverter.ToString(bytesNewString);
            newString = newString.Replace("-", "").ToUpper();
            return newString;
            //return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(parameter, "SHA1");//过时
        }

        #endregion

        #region Sha256加密

        /// <summary>
        /// Sha256加密
        /// </summary>
        /// <param name="parameter">待加密参数</param>
        /// <param name="isReturnNum">是否返回为加密后字符的Byte代码</param>
        /// <returns></returns>
        public static string Sha256Encrypt(this string parameter, bool? isReturnNum = null)
        {
            SHA256 sha256 = new SHA256Managed();
            var tmpByte = sha256.ComputeHash(GetKeyByteArray(parameter));
            sha256.Clear();
            return GetStringValue(tmpByte, FindIsReturnNum(isReturnNum));
        }
        #endregion

        #region Sha512加密

        /// <summary>
        /// Sha512加密
        /// </summary>
        /// <param name="parameter">待加密参数</param>
        /// <param name="isReturnNum">是否返回为加密后字符的Byte代码</param>
        /// <returns></returns>
        public static string Sha512Encrypt(this string parameter, bool? isReturnNum = null)
        {
            SHA512 sha512 = new SHA512Managed();
            var tmpByte = sha512.ComputeHash(GetKeyByteArray(parameter));
            sha512.Clear();
            return GetStringValue(tmpByte, FindIsReturnNum(isReturnNum));
        }
        #endregion

        #region Base64编码

        #region Base64编码
        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="code">需要编码的字符串</param>
        /// <returns></returns>
        public static string EncodeBase64(this string code)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(code));
        }
        #endregion

        #region  Base64解码
        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="code">需要解码的字符串</param>
        /// <returns></returns>
        public static string DecodeBase64(this string code)
        {
            try
            {
                return Encoding.Default.GetString(Convert.FromBase64String(code));
            }
            catch (System.Exception exp)
            {
                return exp.Message;
            }
        }
        #endregion

        #endregion

        #region Url编码

        /// <summary>
        /// 返回 html 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(this string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 返回 html 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(this string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        #endregion

    }
}
