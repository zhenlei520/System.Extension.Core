// Copyright (c) zhenlei520 All rights reserved.

using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// Stream扩展
    /// </summary>
    public partial class Extensions
    {
        #region Stream转换为Byte数组

        /// <summary>
        /// Stream转换为Byte数组
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(this Stream stream)
        {
            return stream.ConvertToByteArrayAsyncByStream(true).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 流转换为字节流
        /// </summary>
        /// <param name="stream">流</param>
        public static async Task<byte[]> ConvertToByteArrayAsync(this Stream stream)
        {
            return await stream.ConvertToByteArrayAsyncByStream(false);
        }

        #region 流转换为字节流

        /// <summary>
        /// 流转换为字节流
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="isSync"></param>
        /// <returns></returns>
        private static async Task<byte[]> ConvertToByteArrayAsyncByStream(this Stream stream, bool isSync)
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

        #endregion

        #region 文件流转字符串

        #region 复制流并转换成字符串（流会重置）

        /// <summary>
        /// 复制流并转换成字符串
        /// </summary>
        /// <param name="stream">流</param>
        public static async Task<string> CopyToStringAsync(this Stream stream)
        {
            return await CopyToStringAsync(stream, Encoding.UTF8);
        }

        /// <summary>
        /// 复制流并转换成字符串（流会重置）
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

        #region 文件流转字符串

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

        #endregion

        #region 文件流转字符串(流会重置，不影响下次读取)

        /// <summary>
        /// 文件流转字符串(流会重置，不影响下次读取)
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
            return ConvertToBase64(await stream.ConvertToByteArrayAsyncByStream(false));
        }

        #endregion

        #region 得到哈希值

        #region md5加密得到哈希值

        /// <summary>
        /// md5加密得到哈希值
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="isUpper">是否大写，默认大写</param>
        /// <returns></returns>
        public static string GetHashByMd5(this Stream stream, bool isUpper = true)
        {
            return stream.GetSha(new MD5CryptoServiceProvider(), isUpper);
        }

        #endregion

        #region Sha1加密得到哈希值

        /// <summary>
        /// Sha1加密得到哈希值
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="isUpper">是否大写</param>
        /// <returns></returns>
        public static string GetHashBySha1(this Stream stream, bool isUpper = true)
        {
            return stream.GetSha(new SHA1CryptoServiceProvider(), isUpper);
        }

        #endregion

        #region Sha256加密得到哈希值

        /// <summary>
        /// Sha256加密得到哈希值
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="isUpper">是否大写，默认大写</param>
        /// <returns></returns>
        public static string GetHashBySha256(this Stream stream, bool isUpper = true)
        {
            return stream.GetSha(new SHA256CryptoServiceProvider(), isUpper);
        }

        #endregion

        #region Sha384加密得到哈希值

        /// <summary>
        /// Sha384加密得到哈希值
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="isUpper">是否大写，默认大写</param>
        /// <returns></returns>
        public static string GetHashBySha384(this Stream stream, bool isUpper = true)
        {
            return stream.GetSha(new SHA384CryptoServiceProvider(), isUpper);
        }

        #endregion

        #region Sha512加密得到哈希值

        /// <summary>
        /// Sha512加密得到哈希值
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="isUpper">是否大写，默认大写</param>
        /// <returns></returns>
        public static string GetHashBySha512(this Stream stream, bool isUpper = true)
        {
            return stream.GetSha(new SHA512CryptoServiceProvider(), isUpper);
        }

        #endregion

        #endregion

        #region 将流转换为内存流

        /// <summary>
        /// 将流转换为内存流
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static MemoryStream ConvertToMemoryStream(this Stream stream)
        {
            stream.Position = 0;
            return new MemoryStream(stream.ConvertToByteArray());
        }

        #endregion
    }
}
