// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 语言
    /// </summary>
    public class Language : Enumeration
    {
        /// <summary>
        /// 未知
        /// </summary>
        public static Language Unknow = new Language(1, "未知");

        /// <summary>
        /// 汉语
        /// </summary>
        public static Language Chinese = new Language(2, "汉语");

        /// <summary>
        /// 粤语
        /// </summary>
        public static Language Cantonese = new Language(3, "粤语");

        /// <summary>
        /// 日语
        /// </summary>
        public static Language Japanese = new Language(4, "日语");

        /// <summary>
        /// 英语
        /// </summary>
        public static Language English = new Language(5, "英语");

        /// <summary>
        /// 韩语
        /// </summary>
        public static Language Korean = new Language(6, "韩语");

        /// <summary>
        /// 法语
        /// </summary>
        public static Language French = new Language(7, "法语");

        /// <summary>
        /// 印度语
        /// </summary>
        public static Language Hindi = new Language(8, "印度语");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">描述</param>
        public Language(int id, string name) : base(id, name)
        {
        }
    }
}
