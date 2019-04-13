// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc
{
    /// <summary>
    /// Identify
    /// </summary>
    public interface IIdentify
    {
        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        string GetIdentify();
    }
}
