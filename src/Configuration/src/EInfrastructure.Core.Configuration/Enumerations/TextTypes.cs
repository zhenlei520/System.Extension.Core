// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 文字类型
    /// </summary>
    public class TextTypes : Enumeration
    {
        /// <summary>
        ///未知
        /// </summary>
        public static TextTypes Unknow = new TextTypes(0, "未知");

        /// <summary>
        /// 简体
        /// </summary>
        public static TextTypes Simplified = new TextTypes(1, "简体");

        /// <summary>
        /// 繁体
        /// </summary>
        public static TextTypes Traditional = new TextTypes(2, "繁体");

        /// <summary>
        /// 日文
        /// </summary>
        public static TextTypes Japanese = new TextTypes(3, "日文");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">描述</param>
        public TextTypes(int id, string name) : base(id, name)
        {
        }
    }
}
