// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using EInfrastructure.Core.UCloud.Storage.Config;

namespace EInfrastructure.Core.UCloud.Storage.Common
{
    internal class Digest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        private static string CanonicalizedUCloudHeaders(WebHeaderCollection header)
        {
            string canoncializedUCloudHeaders = string.Empty;
            SortedDictionary<string, string> headerMap = new SortedDictionary<string, string>();
            for (int i = 0; i < header.Count; ++i)
            {
                string headerKey = header.GetKey(i);
                if (headerKey.ToLower().StartsWith("x-ucloud-"))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    foreach (string value in header.GetValues(i))
                    {
                        if (headerMap.ContainsKey(headerKey))
                        {
                            headerMap[headerKey] += value;
                            headerMap[headerKey] += @",";
                        }
                        else
                        {
                            headerMap.Add(headerKey, value);
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, string> item in headerMap)
            {
                canoncializedUCloudHeaders += (item.Key + @":" + item.Value + @"\n");
            }

            return canoncializedUCloudHeaders;
        }


        #region sign请求

        /// <summary>
        /// sign请求
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type"></param>
        /// <param name="uCloudConfig"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static string SignRequst(HttpWebRequest request, Utils.RequestHeadType type, UCloudStorageConfig uCloudConfig,
            string key)
        {
            string authorization = string.Empty;
            string stringToSign = string.Empty;
            switch (type)
            {
                case Utils.RequestHeadType.HEAD_FIELD_CHECK:
                    authorization += "UCloud ";
                    authorization += uCloudConfig.UCloudPublicKey;
                    authorization += ":";
                    stringToSign = request.Method + "\n" + request.Headers.Get("Content-MD5") + "\n";
                    stringToSign += request.ContentType;
                    stringToSign += "\n";
                    stringToSign += "\n";
                    stringToSign += CanonicalizedUCloudHeaders(request.Headers);
                    stringToSign += CanonicalizedResource(uCloudConfig.Bucket, key);
                    break;
                default:
                    break;
            }

            HMACSHA1 hmac = new HMACSHA1(Encoding.ASCII.GetBytes(uCloudConfig.UCloudPrivateKey));
            Byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
            string signature = Convert.ToBase64String(hashValue);
            return authorization + signature;
        }

        #endregion

        #region private methods

        private static string CanonicalizedResource(string bucket, string key)
        {
            return "/" + bucket + "/" + key;
        }

        #endregion
    }
}