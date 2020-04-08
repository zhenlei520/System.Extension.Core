// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.AutoFac
{
    /// <summary>
    ///
    /// </summary>
    public class ServiceComponent
    {
        /// <summary>
        ///
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// 设置容器
        /// </summary>
        /// <param name="serviceProvider"></param>
        internal static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
