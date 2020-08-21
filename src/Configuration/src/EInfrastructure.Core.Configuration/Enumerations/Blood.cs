// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 血型
    /// </summary>
    public class Blood : Enumeration
    {
        /// <summary>
        /// 未知
        /// </summary>
        public static Blood Unknow = new Blood(1, "未知");

        /// <summary>
        /// 未知
        /// </summary>
        public static Blood A = new Blood(2, "A型");

        /// <summary>
        /// B型
        /// </summary>
        public static Blood B = new Blood(3, "B型");

        /// <summary>
        /// AB型
        /// </summary>
        public static Blood Ab = new Blood(4, "AB型");

        /// <summary>
        /// O型
        /// </summary>
        public static Blood O = new Blood(5, "O型");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">血型id</param>
        /// <param name="name">血型描述</param>
        public Blood(int id, string name) : base(id, name)
        {
        }
    }
}
