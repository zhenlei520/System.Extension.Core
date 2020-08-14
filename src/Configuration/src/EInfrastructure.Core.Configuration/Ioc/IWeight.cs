// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc
{
    /// <summary>
    ///
    /// </summary>
    public interface IWeight
    {
        /// <summary>
        /// 权重 越高越优先选择
        /// 系统包默认权重都为99
        /// </summary>
        /// <returns></returns>
        int GetWeights();
    }
}
