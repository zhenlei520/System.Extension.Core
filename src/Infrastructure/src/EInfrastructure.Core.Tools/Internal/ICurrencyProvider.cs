// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Tools.Internal
{
    /// <summary>
    ///
    /// </summary>
    internal interface ICurrencyProvider: ISingleInstance, IIdentify
    {
        /// <summary>
        /// 数值类型转货币
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string ConvertToCurrency(decimal param);

        /// <summary>
        /// 得到货币类型
        /// </summary>
        CurrencyType GetCurrencyType { get; }
    }
}
