// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    /// <summary>
    ///
    /// </summary>
    public class CustomConfigurationOptions
    {
        /// <summary>
        /// 多久读取一次配置去更新（单位：ms）
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        public string Appid { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public List<string> Namespaces { get; set; }

        /// <summary>
        /// 环境名称
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        ///
        /// </summary>
        internal ICustomConfigurationOptionsExtension Extensions { get; set; }

        /// <summary>
        /// Registers an extension that will be executed when building services.
        /// </summary>
        /// <param name="extension"></param>
        public void RegisterExtension(ICustomConfigurationOptionsExtension extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }

            if (Extensions != null)
            {
                Extensions = extension;
            }
        }
    }
}
