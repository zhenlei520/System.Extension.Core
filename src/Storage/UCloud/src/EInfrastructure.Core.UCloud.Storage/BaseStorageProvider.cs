// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.UCloud.Storage.Common;
using EInfrastructure.Core.UCloud.Storage.Config;
using EInfrastructure.Core.Validation.Common;
using Microsoft.Extensions.Logging;

namespace EInfrastructure.Core.UCloud.Storage
{
    /// <summary>
    /// 基类UCloud实现
    /// </summary>
    public abstract class BaseStorageProvider
    {
        /// <summary>
        ///
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// UCloud配置
        /// </summary>
        protected readonly UCloudStorageConfig UCloudConfig;

        /// <summary>
        /// 基类UCloud实现
        /// </summary>
        public BaseStorageProvider(UCloudStorageConfig uCloudConfig)
        {
        }

        /// <summary>
        /// 基类UCloud实现
        /// </summary>
        public BaseStorageProvider(ILogger logger, UCloudStorageConfig uCloudConfig)
        {
            _logger = logger;
            UCloudConfig = uCloudConfig;
            ValidationCommon.Check(uCloudConfig, "Uc云存储配置异常", HttpStatus.Err.Name);
        }

        #region 上传文件

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="key">文件地址</param>
        /// <param name="ext">扩展名</param>
        /// <exception cref="Exception"></exception>
        internal virtual bool UploadFile(Stream stream, string key, string ext)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest) WebRequest.Create(Utils.GetUrl(UCloudConfig, key));
                request.KeepAlive = false;
                Utils.SetHeaders(request, ext, UCloudConfig, key, "PUT");
                Utils.CopyFile(request, stream);

                response = HttpWebResponseExt.GetResponseNoException(request);
                Stream body = response.GetResponseStream();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string e = FormatString(body);
                    _logger.LogError($"{response.StatusDescription} {e}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ExtractAllStackTrace());
                return false;
            }
            finally
            {
                if (request != null) request.Abort();
                if (response != null) response.Close();
            }

            return true;
        }

        #endregion

        #region 返回权重

        /// <summary>
        /// 返回权重
        /// </summary>
        /// <returns></returns>
        public virtual int GetWeights()
        {
            return 99;
        }

        #endregion

        #region private methods

        private string FormatString(Stream resp)
        {
            UFileError obj = (UFileError) JsonObject(resp);
            if (obj == null)
            {
                return "";
            }

            return string.Format("RetCode: {0} ErrMsg: {1}", obj.GetRetCode(), obj.GetErrMsg());
        }

        private Object JsonObject(Stream resp)
        {
            if (resp == null) return null;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(UFileError));
            UFileError obj = (UFileError) ser.ReadObject(resp);
            return obj;
        }

        #endregion
    }
}
