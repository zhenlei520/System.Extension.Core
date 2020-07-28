// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Extensions.Configuration;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomConfigurationProvider
    {
        /// <summary>
        /// 初始化配置
        /// </summary>
        void InitConfig();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationBuilder"></param>
        void AddFile(IConfigurationBuilder configurationBuilder);
    }
}