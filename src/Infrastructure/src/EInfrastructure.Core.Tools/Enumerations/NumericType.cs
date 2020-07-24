// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Tools.Enumerations
{
    /// <summary>
    /// 正负数类型
    /// </summary>
    public class NumericType : Enumeration
    {
        /// <summary>
        /// 正数
        /// </summary>
        public static NumericType Plus = new NumericType(1, "正数");

        /// <summary>
        /// 负数
        /// </summary>
        public static NumericType Minus = new NumericType(2, "负数");

        /// <summary>
        /// 不限
        /// </summary>
        public static NumericType Nolimit = new NumericType(3, "不限");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public NumericType(int id, string name) : base(id, name)
        {
        }
    }
}
