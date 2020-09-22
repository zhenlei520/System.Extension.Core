// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Config.Entities.Enumerations
{
    /// <summary>
    /// 数据库操作类型
    /// </summary>
    public class DbOptionType : Enumeration
    {
        /// <summary>
        /// Write
        /// </summary>
        public static DbOptionType Write = new DbOptionType(1, "Write");

        /// <summary>
        /// Read
        /// </summary>
        public static DbOptionType Read = new DbOptionType(2, "Read");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public DbOptionType(int id, string name) : base(id, name)
        {
        }
    }
}
