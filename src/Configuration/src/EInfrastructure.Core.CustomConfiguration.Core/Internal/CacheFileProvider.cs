// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using EInfrastructure.Core.Configuration.Ioc.Plugs;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Files;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheFileProvider : ICacheFileProvider
    {
        private readonly IJsonProvider _jsonProvider;
        private readonly IXmlProvider _xmlProvider;

        public CacheFileProvider(IJsonProvider jsonProvider, IXmlProvider xmlProvider)
        {
            this._jsonProvider = jsonProvider;
            this._xmlProvider = xmlProvider;
        }

        #region 得到配置信息

        /// <summary>
        /// 得到配置信息
        /// </summary>
        /// <param name="configFile"></param>
        /// <returns></returns>
        public Dictionary<string, string> Get(string configFile)
        {
            var content = FileCommon.GetFileContent(configFile);
            if (string.IsNullOrEmpty(configFile))
            {
                return new Dictionary<string, string>();
            }

            if (PathCommon.GetExtension(configFile).Equals(".json"))
            {
                return this._jsonProvider.Deserialize<Dictionary<string, string>>(content);
            }

            return this._xmlProvider.Deserialize<Dictionary<string, string>>(content);
        }

        #endregion

        #region 保存文件

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="configFile"></param>
        /// <param name="content"></param>
        public void Save(string configFile, object content)
        {
            if (!FileCommon.IsExistFile(configFile))
            {
                string directory = Path.GetDirectoryName(configFile);
                FileCommon.CreateDirectory(directory);
            }

            if (content is string)
            {
                FileCommon.WriteText(configFile, content.ToString());
                return;
            }

            if (PathCommon.GetExtension(configFile).Equals(".json"))
            {
                FileCommon.WriteText(configFile, this._jsonProvider.Serializer(content));
            }
            else
            {
                FileCommon.WriteText(configFile, this._xmlProvider.Serializer(content));
            }
        }

        #endregion
    }
}