// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations.MonetaryUnit
{
    /// <summary>
    /// 日本货币单位
    /// </summary>
    public class JapanMonetaryUnit : Enumeration
    {
        /// <summary>
        /// 元
        /// </summary>
        public static JapanMonetaryUnit Yuan = new JapanMonetaryUnit(1, "yuan");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public JapanMonetaryUnit(int id, string name) : base(id, name)
        {
        }
    }
}
