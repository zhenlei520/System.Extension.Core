// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
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

        /// <summary>
        /// 
        /// </summary>
        public void InitConfig()
        {
            var configurationList = this._configurationDataProvider.GetAllData(this._customConfigurationOptions.Appid,
                this._customConfigurationOptions.EnvironmentName,
                this._customConfigurationOptions.Namespaces.ToArray());
            if (configurationList == null)
            {
                return;
            }

            _customConfigurationOptions.Namespaces.ForEach(namespaces =>
            {
                var data = configurationList.FirstOrDefault(x => x.Key == namespaces);
                if (data != null)
                {
                    this._cacheFileProvider.Save(_customConfigurationOptions.GetCacheFile(namespaces), data.Value);
                }
                else
                {
                    FileCommon.DeleteFile(_customConfigurationOptions.GetCacheFile(namespaces));
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationBuilder"></param>
        public void AddFile(IConfigurationBuilder configurationBuilder)
        {
            this._customConfigurationOptions.Namespaces.ForEach(namespaces =>
            {
                if (PathCommon.GetExtension(namespaces).Equals(".json", StringComparison.InvariantCultureIgnoreCase))
                {
                    configurationBuilder.AddJsonFile(this._customConfigurationOptions.GetCacheFile(namespaces), true,
                        true);
                }
                else
                {
                    throw new NotImplementedException("暂不支持xml配置");
                }
            });
        }
    }
}