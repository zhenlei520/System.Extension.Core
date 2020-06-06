// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// 货币类型转换
    /// </summary>
    public class CurrencyTypeConversionCommonUnitTest : BaseUnitTest
    {
        /// <summary>
        /// 货币类型转换，目前仅支持人民币
        /// </summary>
        /// <param name="money"></param>
        [Theory]
        [InlineData(100.02)]
        public void ConvertDecimalToString(decimal money)
        {
            string str = money.ConvertToCurrency();
        }
    }
}
