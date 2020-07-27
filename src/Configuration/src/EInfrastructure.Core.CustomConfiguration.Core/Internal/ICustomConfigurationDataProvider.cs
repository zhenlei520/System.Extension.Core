// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    /// <summary>
    /// 自定义配置
    /// </summary>
    public interface ICustomConfigurationDataProvider
    {
        /// <summary>
        /// 得到全部的配置信息
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetAllData();
    }
}
