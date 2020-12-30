// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations.MonetaryUnit
{
    /// <summary>
    /// 英国货币单位
    /// </summary>
    public class EnglandMonetaryUnit : Enumeration
    {
        /// <summary>
        /// 英镑
        /// </summary>
        public static EnglandMonetaryUnit Pound = new EnglandMonetaryUnit(1, "pound");

        /// <summary>
        /// 先令
        /// </summary>
        public static EnglandMonetaryUnit Shiling = new EnglandMonetaryUnit(2, "shiling");

        /// <summary>
        /// 便士
        /// </summary>
        public static EnglandMonetaryUnit Penny = new EnglandMonetaryUnit(3, "penny");

        /// <summary>
        /// 新便士
        /// </summary>
        public static EnglandMonetaryUnit NewPenny = new EnglandMonetaryUnit(4, "NewPenny");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public EnglandMonetaryUnit(int id, string name) : base(id, name)
        {
        }
    }
}
