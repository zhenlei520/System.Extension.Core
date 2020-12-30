// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations.MonetaryUnit
{
    /// <summary>
    /// 中国货币单位
    /// </summary>
    public class ChinaMonetaryUnit : Enumeration
    {
        /// <summary>
        /// 元
        /// </summary>
        public static ChinaMonetaryUnit Yuan = new ChinaMonetaryUnit(1, "yuan");

        /// <summary>
        /// 角
        /// </summary>
        public static ChinaMonetaryUnit Dime = new ChinaMonetaryUnit(2, "dime");

        /// <summary>
        /// 分
        /// </summary>
        public static ChinaMonetaryUnit Cent = new ChinaMonetaryUnit(3, "cent");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public ChinaMonetaryUnit(int id, string name) : base(id, name)
        {
        }
    }
}
