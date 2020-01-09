// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Config.EnumerationExtensions
{
    /// <summary>
    /// 血型
    /// </summary>
    public class Blood : EntitiesExtensions.SeedWork.Enumeration
    {
        /// <summary>
        /// 未知
        /// </summary>
        public static Blood Unknow = new Blood(0, "未知");

        /// <summary>
        /// 未知
        /// </summary>
        public static Blood A = new Blood(1, "A型");

        /// <summary>
        /// B型
        /// </summary>
        public static Blood B = new Blood(2, "B型");

        /// <summary>
        /// AB型
        /// </summary>
        public static Blood Ab = new Blood(3, "AB型");

        /// <summary>
        /// O型
        /// </summary>
        public static Blood O = new Blood(4, "O型");

        public Blood(int id, string name) : base(id, name)
        {
        }
    }
}
