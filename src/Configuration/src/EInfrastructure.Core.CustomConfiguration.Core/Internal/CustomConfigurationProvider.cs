// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Files;
using Microsoft.Extensions.Configuration;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    public class CustomConfigurationProvider : ICustomConfigurationProvider
    {
        private readonly CustomConfigurationOptions _customConfigurationOptions;
        private readonly ICustomConfigurationDataProvider _configurationDataProvider;
        private readonly ICacheFileProvider _cacheFileProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customConfigurationOptions"></param>
        /// <param name="configurationDataProvider"></param>
        /// <param name="cacheFileProvider"></param>
        public CustomConfigurationProvider(CustomConfigurationOptions customConfigurationOptions,
            ICustomConfigurationDataProvider configurationDataProvider,
            ICacheFileProvider cacheFileProvider)
        {
            this._customConfigurationOptions = customConfigurationOptions;
            this._configurationDataProvider = configurationDataProvider;
            this._cacheFileProvider = cacheFileProvider;
        }

        #region 初始化配置

        /// <summary>
        /// 初始化配置
        /// </summary>
        public void InitConfig()
        {
            var configurationList = this._configurationDataProvider.GetAllData(this._customConfigurationOptions.Appid,
                this._customConfigurationOptions.EnvironmentName,
                this._customConfigurationOptions.Namespaces.Distinct().ToArray());
            if (configurationList == null)
            {
                return;
            }

            _customConfigurationOptions.Namespaces.ForEach(namespaces =>
            {
                var data = configurationList.FirstOrDefault(x => x.Key == namespaces);
                if (data != null)
                {
                    this._cacheFileProvider.Save(GetFileFullPath(namespaces), data.Value);
                }
                else
                {
                    FileCommon.DeleteFile(GetFileFullPath(namespaces));
                }
            });
        }

        #endregion

        #region 添加文件

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="configurationBuilder"></param>
        public void AddFile(IConfigurationBuilder configurationBuilder)
        {
            this._customConfigurationOptions.Namespaces.ForEach(namespaces =>
            {
                configurationBuilder.AddJsonFile(
                    this._customConfigurationOptions.GetCacheFile(GetFileFullPath(namespaces)), true,
                    true);
            });
        }

        #endregion

        #region private methods

        #region 得到完整的文件地址信息

        /// <summary>
        /// 得到完整的文件地址信息
        /// </summary>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        string GetFileFullPath(string namespaces)
        {
            var ext = PathCommon.GetExtension(namespaces);
            if (ext.Equals(".json", StringComparison.InvariantCultureIgnoreCase))
            {
                return this._customConfigurationOptions.GetCacheFile(namespaces);
            }

            if (string.IsNullOrEmpty(ext))
            {
                return this._customConfigurationOptions.GetCacheFile(namespaces + ".property.json");
            }

            throw new NotImplementedException(nameof(namespaces));
        }

        #endregion

        #endregion
    }
}