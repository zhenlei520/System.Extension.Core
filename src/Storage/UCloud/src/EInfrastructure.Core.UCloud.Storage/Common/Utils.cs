// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.IO;
using System.Net;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.UCloud.Storage.Config;

namespace EInfrastructure.Core.UCloud.Storage.Common
{
    internal class Utils
    {
        /// <summary>
        /// 请求类型
        /// </summary>
        public enum RequestHeadType
        {
            HEAD_FIELD_CHECK
        };

        internal static int bufferLen = 32 * 1024;

        #region 得到url地址

        /// <summary>
        /// 得到url地址
        /// </summary>
        /// <param name="uCloudConfig">配置信息</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        internal static string GetUrl(UCloudStorageConfig uCloudConfig, string key)
        {
            return @"http://" + uCloudConfig.Bucket + uCloudConfig.UcloudProxySuffix +
                   (key == string.Empty ? "" : (@"/" + key));
        }

        #endregion

        #region 复制文件

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="request"></param>
        /// <param name="sourceStream">源文件</param>
        internal static void CopyFile(HttpWebRequest request, Stream sourceStream)
        {
            Stream rs = request.GetRequestStream();
            Utils.CopyNBit(rs, sourceStream, sourceStream.Length);
            sourceStream.Close();
            rs.Close();
        }

        #endregion

        #region 设置请求头

        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ext">文件扩展名</param>
        /// <param name="uCloudConfig">配置信息</param>
        /// <param name="key">文件地址</param>
        /// <param name="httpVerb"></param>
        internal static void SetHeaders(HttpWebRequest request, string ext, UCloudStorageConfig uCloudConfig, string key,
            string httpVerb)
        {
            request.UserAgent = uCloudConfig.GetUserAgent();
            if (ext != string.Empty)
            {
                request.ContentType = MimeTypeCommon.GetMimeType(ext);
            }

            request.Method = httpVerb;
            request.Headers.Add("Authorization",
                Digest.SignRequst(request, RequestHeadType.HEAD_FIELD_CHECK, uCloudConfig, key));
        }

        #endregion

        #region 得到文件长度

        /// <summary>
        /// 得到文件长度
        /// </summary>
        /// <param name="dst">复制后文件信息</param>
        /// <param name="src">源文件信息</param>
        /// <param name="numBytesToCopy"></param>
        /// <returns></returns>
        public static long CopyNBit(Stream dst, Stream src, long numBytesToCopy)
        {
            long l = src.Position;
            byte[] buffer = new byte[bufferLen];
            long numBytesWritten = 0;
            while (numBytesWritten < numBytesToCopy)
            {
                int len = bufferLen;
                if ((numBytesToCopy - numBytesWritten) < len)
                {
                    len = (int) (numBytesToCopy - numBytesWritten);
                }

                int n = src.Read(buffer, 0, len);
                if (n == 0) break;
                dst.Write(buffer, 0, n);
                numBytesWritten += n;
            }

            src.Seek(l, SeekOrigin.Begin);
            return numBytesWritten;
        }

        #endregion
    }
}