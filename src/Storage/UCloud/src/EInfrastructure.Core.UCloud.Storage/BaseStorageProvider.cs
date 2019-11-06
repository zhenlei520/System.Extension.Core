// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using EInfrastructure.Core.Interface.Log;
using EInfrastructure.Core.UCloud.Storage.Common;
using EInfrastructure.Core.UCloud.Storage.Config;
using EInfrastructure.Core.UCloud.Storage.Validator;
using EInfrastructure.Core.Validation.Common;

namespace EInfrastructure.Core.UCloud.Storage
{
    /// <summary>
    /// 基类UCloud实现
    /// </summary>
    public class BaseStorageProvider
    {
        protected readonly ILogService _logService;

        /// <summary>
        /// UCloud配置
        /// </summary>
        protected readonly UCloudStorageConfig _uCloudConfig;

        /// <summary>
        /// 基类UCloud实现
        /// </summary>
        public BaseStorageProvider(ILogService logService, UCloudStorageConfig uCloudConfig)
        {
            _logService = logService;
            _uCloudConfig = uCloudConfig;
            new UCloudConfigValidator().Validate(_uCloudConfig).Check();
        }

        #region 上传文件

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="key">文件地址</param>
        /// <param name="ext">扩展名</param>
        /// <exception cref="Exception"></exception>
        internal bool UploadFile(Stream stream, string key, string ext)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest) WebRequest.Create(Utils.GetUrl(_uCloudConfig, key));
                request.KeepAlive = false;
                Utils.SetHeaders(request, ext, _uCloudConfig, key, "PUT");
                Utils.CopyFile(request, stream);

                response = HttpWebResponseExt.GetResponseNoException(request);
                Stream body = response.GetResponseStream();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string e = FormatString(body);
                    _logService.Error(string.Format("{0} {1}", response.StatusDescription, e));
                    return false;
                }
            }
            catch (System.Exception e)
            {
                _logService.Error(e.ToString());
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