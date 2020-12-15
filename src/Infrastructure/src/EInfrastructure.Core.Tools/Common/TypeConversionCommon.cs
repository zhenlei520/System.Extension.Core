// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// 类型转换
    /// </summary>
    public static class TypeConversionCommon
    {
        #region 文件类型转换

        #region 转换为Byte数组

        #region Stream转换为Byte数组

        /// <summary>
        /// Stream转换为Byte数组
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(this Stream stream)
        {
            return stream.ConvertToByteArrayAsync(true).Result;
        }

        /// <summary>
        /// 流转换为字节流
        /// </summary>
        /// <param name="stream">流</param>
        public static async Task<byte[]> ConvertToByteArrayAsync(Stream stream)
        {
            return await stream.ConvertToByteArrayAsync(false);
        }

        /// <summary>
        /// 流转换为字节流
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="isSync"></param>
        /// <returns></returns>
        private static async Task<byte[]> ConvertToByteArrayAsync(this Stream stream, bool isSync)
        {
            if (stream == null || !stream.CanRead)
            {
                return new byte[] { };
            }

            if (!stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            var bytes = new byte[stream.Length];
            if (isSync)
            {
                stream.Read(bytes, 0, bytes.Length);
            }
            else
            {
                await stream.ReadAsync(bytes, 0, bytes.Length);
            }

            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        #endregion

        #region String转换为Byte数组

        /// <summary>
        /// String转换为Byte数组
        /// </summary>
        /// <param name="para">待转换参数</param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(this string para)
        {
            return para.ConvertToByteArray(Encoding.UTF8);
        }

        /// <summary>
        /// String转换为Byte数组
        /// </summary>
        /// <param name="para">待转换参数</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(this string para, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(para))
                return new byte[] { };
            return encoding.GetBytes(para);
        }

        #endregion

        #endregion

        #region 转换为Stream

        #region 将 byte[] 转成 Stream

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns></returns>
        public static Stream ConvertToStream(this byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        #endregion

        #endregion

        #region 转换为String

        #region 文件流转字符串

        #region 复制流并转换成字符串

        /// <summary>
        /// 复制流并转换成字符串
        /// </summary>
        /// <param name="stream">流</param>
        public static async Task<string> CopyToStringAsync(this Stream stream)
        {
            return await CopyToStringAsync(stream, Encoding.UTF8);
        }

        /// <summary>
        /// 复制流并转换成字符串
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">字符编码</param>
        public static async Task<string> CopyToStringAsync(this Stream stream, Encoding encoding)
        {
            if (stream == null)
                return string.Empty;
            if (stream.CanRead == false)
                return string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                using (var reader = new StreamReader(memoryStream, encoding))
                {
                    if (stream.CanSeek)
                        stream.Seek(0, SeekOrigin.Begin);
                    await stream.CopyToAsync(memoryStream);
                    if (memoryStream.CanSeek)
                        memoryStream.Seek(0, SeekOrigin.Begin);
                    var result = await reader.ReadToEndAsync();
                    if (stream.CanSeek)
                        stream.Seek(0, SeekOrigin.Begin);
                    return result;
                }
            }
        }

        #endregion

        /// <summary>
        /// 文件流转字符串
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public static string ConvertToString(this Stream stream)
        {
            return stream.ConvertToString(Encoding.UTF8);
        }

        /// <summary>
        /// 文件流转字符串
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="isCloseStream">是否自动释放文件</param>
        /// <returns></returns>
        public static string ConvertToString(this Stream stream, Encoding encoding, int bufferSize = 1024 * 2,
            bool isCloseStream = true)
        {
            return stream.ConvertToStringAsync(encoding, true, bufferSize, isCloseStream).Result;
        }

        /// <summary>
        /// 文件流转字符串
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public static async Task<string> ConvertToStringAsync(this Stream stream)
        {
            return await stream.ConvertToStringAsync(Encoding.UTF8, false);
        }

        /// <summary>
        /// 文件流转字符串
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="isCloseStream">是否自动释放文件</param>
        /// <returns></returns>
        public static async Task<string> ConvertToStringAsync(this Stream stream, Encoding encoding,
            int bufferSize = 1024 * 2,
            bool isCloseStream = true)
        {
            return await stream.ConvertToStringAsync(encoding, false, bufferSize, isCloseStream);
        }

        /// <summary>
        /// 文件流转字符串
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="isSync">是否同步</param>
        /// <param name="bufferSize">缓冲区大小</param>
        /// <param name="isCloseStream">是否自动释放文件</param>
        /// <returns></returns>
        private static async Task<string> ConvertToStringAsync(this Stream stream, Encoding encoding, bool isSync,
            int bufferSize = 1024 * 2,
            bool isCloseStream = true)
        {
            if (stream == null || encoding == null || stream.CanRead == false)
                return string.Empty;
            using (var reader = new StreamReader(stream, encoding, true, bufferSize, !isCloseStream))
            {
                if (stream.CanSeek)
                    stream.Seek(0, SeekOrigin.Begin);
                var result = isSync ? reader.ReadToEnd() : await reader.ReadToEndAsync();
                if (stream.CanSeek)
                    stream.Seek(0, SeekOrigin.Begin);
                return result;
            }
        }

        #endregion

        #region byte数组转换为string

        /// <summary>
        /// byte数组转换为string
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <returns></returns>
        public static string ConvertToString(this byte[] bytes)
        {
            return ConvertToString(bytes, Encoding.UTF8);
        }

        /// <summary>
        /// byte数组转换为string
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ConvertToString(this byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        #endregion

        #endregion

        #region base64字符串转byte数组

        /// <summary>
        /// 文件base64转byte数组
        /// </summary>
        /// <param name="base64">文件base64</param>
        /// <returns></returns>
        public static byte[] ConvertToByte(this string base64)
        {
            return Convert.FromBase64String(base64);
        }

        #endregion

        #region byte数组转换为base64

        /// <summary>
        /// byte数组转换为base64
        /// </summary>
        /// <param name="param">byte数组</param>
        /// <returns></returns>
        public static string ConvertToBase64(this byte[] param)
        {
            return Convert.ToBase64String(param);
        }

        #endregion

        #region 文件流转换为base64

        /// <summary>
        /// 文件流转换为base64
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public static string ConvertToBase64(this Stream stream)
        {
            return ConvertToBase64(stream.ConvertToByteArray());
        }

        /// <summary>
        /// 文件流转换为base64
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns></returns>
        public static async Task<string> ConvertToBase64Async(this Stream stream)
        {
            return ConvertToBase64(await stream.ConvertToByteArrayAsync(false));
        }

        #endregion

        #endregion

        #region 清空小数点后0

        /// <summary>
        /// 保留两位小数并对其四舍五入，如果最后的两位小数为*.00则去除小数位，否则保留两位小数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearDecimal(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return "0";
            str = float.Parse(str).ToString("0.00");
            if (Int32.Parse(str.Substring(str.IndexOf(".", StringComparison.Ordinal) + 1)) == 0)
            {
                return str.Substring(0, str.IndexOf(".", StringComparison.Ordinal));
            }

            return str;
        }

        #endregion

        #region 加密显示以*表示

        /// <summary>
        /// 加密显示以*表示
        /// </summary>
        /// <param name="number">显示N位*,-1默认显示6位</param>
        /// <param name="symbol">特殊符号，默认为*</param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        public static string GetContentByEncryption(this char? symbol, int number = 6,
            int? errCode = null)
        {
            if (symbol == null)
            {
                symbol = '*';
            }

            string result = ""; //结果
            if (number < 0)
            {
                throw new BusinessException("number必须为正整数", HttpStatus.Err.Id);
            }

            for (int i = 0; i < number; i++)
            {
                result += symbol;
            }

            return result;
        }

        /// <summary>
        /// 加密显示以*表示
        /// </summary>
        /// <param name="symbol">特殊符号，默认为*</param>
        /// <param name="number">显示N次*,-1默认显示6位</param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        public static string GetContentByEncryption(this string symbol, int number = 6,
            int? errCode = null)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                symbol = "*";
            }

            string result = ""; //结果
            if (number < 0)
            {
                throw new BusinessException("number必须为正整数", HttpStatus.Err.Id);
            }

            for (int i = 0; i < number; i++)
            {
                result += symbol;
            }

            return result;
        }

        #endregion

        #region 编码

        #region Unicode编码

        /// <summary>
        /// 汉字转换为Unicode编码
        /// </summary>
        /// <param name="str">要编码的汉字字符串</param>
        /// <returns>Unicode编码的的字符串</returns>
        public static string ConvertStringToUnicode(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            byte[] bts = Encoding.Unicode.GetBytes(str);
            string r = "";
            for (int i = 0; i < bts.Length; i += 2)
                r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
            return r;
        }

        #endregion

        #region 将Unicode编码转换为汉字字符串

        /// <summary>
        /// 将Unicode编码转换为汉字字符串
        /// </summary>
        /// <param name="str">Unicode编码字符串</param>
        /// <returns>汉字字符串</returns>
        public static string ConvertUnicodeToString(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            string r = "";
            MatchCollection mc = Regex.Matches(str, @"\\u([\w]{2})([\w]{2})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            foreach (Match m in mc)
            {
                bts[0] = (byte) int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                bts[1] = (byte) int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }

            return r;
        }

        #endregion

        #endregion
        
    }
}
