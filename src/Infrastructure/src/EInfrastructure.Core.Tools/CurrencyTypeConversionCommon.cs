// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Tools.Component;
using EInfrastructure.Core.Tools.DecimalConversion;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 货币类型转换
    /// </summary>
    public static class CurrencyTypeConversionCommon
    {
        #region 转换为货币

        /// <summary>
        /// 转换为货币
        /// </summary>
        /// <param name="param">带转换的金额</param>
        /// <param name="currencyType">货币类型，默认人民币</param>
        /// <returns></returns>
        public static string ConvertToCurrency(this decimal param, CurrencyType currencyType = null)
        {
            if (currencyType == null)
            {
                currencyType = CurrencyType.Cny;
            }

            IEnumerable<IDecimalConversionToCurrency> list =
                new ServiceProvider().GetServices<IDecimalConversionToCurrency>();
            var provider = list.FirstOrDefault(x => x.GetCurrencyType.Equals(currencyType));
            if (provider == null)
            {
                throw new BusinessException("不支持的货币类型");
            }

            return provider.ConvertToCurrency(param);
        }

        #endregion
    }
}
