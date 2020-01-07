// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Config.EnumerationExtensions
{
    /// <summary>
    /// 操作系统
    /// Windows、Linux、OSX、Unknow
    /// </summary>
    public class OsPlatform : EntitiesExtensions.SeedWork.Enumeration
    {
        /// <summary>
        /// Windows
        /// </summary>
        public static OsPlatform Windows = new OsPlatform(1, "Windows");

        /// <summary>
        /// Linux
        /// </summary>
        public static OsPlatform Linux = new OsPlatform(2, "Linux");

        /// <summary>
        /// OSX
        /// </summary>
        public static OsPlatform OSX = new OsPlatform(3, "OSX");

        /// <summary>
        /// Unknow
        /// </summary>
        public static OsPlatform Unknow = new OsPlatform(99, "Unknow");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public OsPlatform(int id, string name) : base(id, name)
        {
        }
    }
}
