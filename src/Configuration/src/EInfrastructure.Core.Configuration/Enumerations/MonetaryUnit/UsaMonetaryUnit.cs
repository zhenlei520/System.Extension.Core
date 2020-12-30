// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations.MonetaryUnit
{
    /// <summary>
    /// 美国货币单位
    /// </summary>
    public class UsaMonetaryUnit : Enumeration
    {
        /// <summary>
        /// 美元
        /// </summary>
        public static UsaMonetaryUnit Dollar = new UsaMonetaryUnit(1, "dollar");

        /// <summary>
        /// 美分
        /// </summary>
        public static UsaMonetaryUnit Cent = new UsaMonetaryUnit(2, "cent");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public UsaMonetaryUnit(int id, string name) : base(id, name)
        {
        }
    }
}
