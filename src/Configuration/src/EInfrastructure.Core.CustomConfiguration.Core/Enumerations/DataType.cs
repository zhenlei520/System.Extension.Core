// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.CustomConfiguration.Core.Enumerations
{
    /// <summary>
    /// 数据类型
    /// </summary>
    public class DataType : Enumeration
    {
        /// <summary>
        /// Json
        /// </summary>
        public static DataType Json = new DataType(1, "Json");

        /// <summary>
        /// Xml
        /// </summary>
        public static DataType Xml = new DataType(2, "Xml");

        /// <summary>
        /// Property
        /// </summary>
        public static DataType Property = new DataType(3, "Property");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public DataType(int id, string name) : base(id, name)
        {
        }
    }
}
