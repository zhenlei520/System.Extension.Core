// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations;

namespace EInfrastructure.Core.Tools.Extensions.DecimalConversion
{
    /// <summary>
    ///
    /// </summary>
    internal interface ICurrencyProvider
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
