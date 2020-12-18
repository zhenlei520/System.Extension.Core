// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public partial class Extensions
    {
        #region 安全转换为字符串，去除两端空格，当值为null时返回空

        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回空
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="isReplaceSpace">是否移除空格（默认移除）</param>
        public static string SafeString(this object param, bool isReplaceSpace = true)
        {
            return Common.ObjectCommon.SafeObject(param != null,
                () =>
                {
                    if (isReplaceSpace)
                    {
                        return ValueTuple.Create(param?.ToString().Trim(), string.Empty);
                    }

                    return ValueTuple.Create(param?.ToString(), string.Empty);
                });
        }

        #endregion

        #region 安全获取值，当值为null时，不会抛出异常

        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }

        #endregion

        #region 值互换(左边最小值,右边最大值)

        /// <summary>
        /// 值互换(左边最小值,右边最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        /// <typeparam name="T">执行后，参数1为较小者，参数2为较大者</typeparam>
        public static void ChangeResult<T>(ref T parameter1, ref T parameter2) where T : IComparable
        {
            if (parameter2.LessThan(parameter1))
            {
                var temp = parameter2;
                parameter2 = parameter1;
                parameter1 = temp;
            }
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref ushort parameter1, ref ushort parameter2)
        {
            ChangeResult<ushort>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref short parameter1, ref short parameter2)
        {
            ChangeResult<short>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref int parameter1, ref int parameter2)
        {
            ChangeResult<int>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref long parameter1, ref long parameter2)
        {
            ChangeResult<long>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref sbyte parameter1, ref sbyte parameter2)
        {
            ChangeResult<sbyte>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref byte parameter1, ref byte parameter2)
        {
            ChangeResult<byte>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref float parameter1, ref float parameter2)
        {
            ChangeResult<float>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref double parameter1, ref double parameter2)
        {
            ChangeResult<double>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref decimal parameter1, ref decimal parameter2)
        {
            ChangeResult<decimal>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref DateTime parameter1, ref DateTime parameter2)
        {
            ChangeResult<DateTime>(ref parameter1, ref parameter2);
        }
        #endregion

        #region 文件类型转换

        #region 转换为Byte数组

        #region Stream转换为Byte数组

        /// <summary>
        /// Stream转换为Byte数组
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns></returns>
        public static byte[] ConvertToByteArrayByStream(this Stream stream)
        {
            return stream.ConvertToByteArrayAsyncByStream(true).Result;
        }

        /// <summary>
        /// 流转换为字节流
        /// </summary>
        /// <param name="stream">流</param>
        public static async Task<byte[]> ConvertToByteArrayAsyncByStream(Stream stream)
        {
            return await stream.ConvertToByteArrayAsyncByStream(false);
        }

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

        #region Base64算法加密

        /// <summary>
        /// Base64算法加密
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static byte[] ConvertToBase64ByteArray(this string str)
        {
            return Convert.FromBase64String(str);
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

        /// <summary>
        /// byte数组转换为base64
        /// </summary>
        /// <param name="inArray"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static string ConvertToBase64(this byte[] inArray, int offset)
        {
            return Convert.ToBase64String(inArray, offset,inArray.Length);
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
            return ConvertToBase64(await stream.ConvertToByteArrayAsyncByStream(false));
        }

        #endregion

        #endregion
    }
}
