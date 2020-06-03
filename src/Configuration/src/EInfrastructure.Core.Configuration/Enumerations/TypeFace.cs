// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 字体
    /// </summary>
    public class TypeFace : Enumeration
    {
        /// <summary>
        /// 简体中文
        /// </summary>
        public static TypeFace SimplifiedChinese = new TypeFace(1, "简体中文");

        /// <summary>
        /// 繁体中文
        /// </summary>
        public static TypeFace TraditionalChinese = new TypeFace(2, "繁体中文");

        /// <summary>
        /// 英语
        /// </summary>
        public static TypeFace English = new TypeFace(3, "英语");

        /// <summary>
        /// 字体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public TypeFace(int id, string name) : base(id, name)
        {
        }
    }
}
