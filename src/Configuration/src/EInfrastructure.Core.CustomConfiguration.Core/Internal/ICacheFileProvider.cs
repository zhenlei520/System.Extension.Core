// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICacheFileProvider
    {
        /// <summary>
        /// 得到配置信息
        /// </summary>
        /// <param name="configFile"></param>
        /// <returns></returns>
        Dictionary<string, string> Get(string configFile);

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="configFile"></param>
        /// <param name="content"></param>
        void Save(string configFile, object content);
    }
}