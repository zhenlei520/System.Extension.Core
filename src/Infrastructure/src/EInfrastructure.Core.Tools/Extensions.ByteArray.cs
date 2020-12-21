// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.IO;
using System.Text;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// Byte数组扩展
    /// </summary>
    public partial class Extensions
    {
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
    }
}
