// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.CustomConfiguration.Core.Domain;
using EInfrastructure.Core.CustomConfiguration.Core.Internal;

namespace EInfrastructure.Core.CustomConfiguration.MySql
{
    /// <summary>
    ///
    /// </summary>
    public class CustomConfigurationDataProvider : ICustomConfigurationDataProvider
    {
        private readonly IQuery<NamespaceItems, long, CustomerConfigurationDbContext> _namespaceItemsQuery;
        private readonly CustomConfigurationOptions _configurationOptions;

        /// <summary>
        ///
        /// </summary>
        /// <param name="namespaceItemsQuery"></param>
        /// <param name="customConfigurationOptions"></param>
        public CustomConfigurationDataProvider(
            IQuery<NamespaceItems, long, CustomerConfigurationDbContext> namespaceItemsQuery,
            CustomConfigurationOptions customConfigurationOptions)
        {
            this._namespaceItemsQuery = namespaceItemsQuery;
            this._configurationOptions = customConfigurationOptions;
        }

        #region 得到指定appid下指定namespace集合下的指定环境未删除的配置信息

        /// <summary>
        /// 得到指定appid下指定namespace集合下的指定环境未删除的配置信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetAllData()
        {
            var data = _namespaceItemsQuery.GetQueryable().Where(x =>
                    !x.IsDel && x.AppNamespaces.AppId == _configurationOptions.Appid &&
                    x.EnvironmentName == _configurationOptions.EnvironmentName &&
                    _configurationOptions.Namespaces.Contains(x.AppNamespaces.Name))
                .Select(x => new
                {
                    x.Key,
                    x.Value
                }).ToDictionary(x => x.Key, x => x.Value);
            return data;
        }

        #endregion
    }
}
