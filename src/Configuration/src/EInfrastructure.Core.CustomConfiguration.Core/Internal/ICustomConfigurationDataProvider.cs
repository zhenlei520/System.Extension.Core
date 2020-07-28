// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.CustomConfiguration.Core.Dto;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    /// <summary>
    /// 自定义配置
    /// </summary>
    public interface ICustomConfigurationDataProvider
    {
        /// <summary>
        /// 得到指定appid下指定namespace集合下的指定环境未删除的配置信息
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="environmentName">环境名称</param>
        /// <param name="namespaces">命名空间姐</param>
        /// <returns></returns>
        List<ConfigurationDto> GetAllData(string appid, string environmentName, params string[] namespaces);
    }
}