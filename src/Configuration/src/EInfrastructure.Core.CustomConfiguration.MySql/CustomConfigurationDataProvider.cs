// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.CustomConfiguration.Core.Domain;
using EInfrastructure.Core.CustomConfiguration.Core.Dto;
using EInfrastructure.Core.CustomConfiguration.Core.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.CustomConfiguration.MySql
{
    /// <summary>
    ///
    /// </summary>
    public class CustomConfigurationDataProvider : ICustomConfigurationDataProvider
    {
        #region 得到指定appid下指定namespace集合下的指定环境未删除的配置信息

        /// <summary>
        /// 得到指定appid下指定namespace集合下的指定环境未删除的配置信息
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="environmentName">环境名称</param>
        /// <param name="namespaces">命名空间姐</param>
        /// <returns></returns>
        public List<ConfigurationDto> GetAllData(string appid, string environmentName, params string[] namespaces)
        {
            return Core.CustomConfigurationExtensions.Invoke(serviceProvider =>
            {
                var namespaceItemsQuery = serviceProvider
                    .GetService<IQuery<NamespaceItems, long, CustomerConfigurationDbContext>>();
                var data = namespaceItemsQuery.GetQueryable().Where(x =>
                        !x.IsDel && x.AppNamespaces.AppId == appid &&
                        x.EnvironmentName == environmentName &&
                        namespaces.Contains(x.AppNamespaces.Name))
                    .Select(x => new ConfigurationDto()
                    {
                        Namespances = x.AppNamespaces.Name,
                        Key = x.Key,
                        Value = x.Value
                    }).ToList();
                return data;
            });
        }

        #endregion
    }
}