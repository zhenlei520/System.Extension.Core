// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 默认支持的正则表达式类型
    /// </summary>
    public class RegexDefault : Enumeration
    {
        /// <summary>
        /// 邮件
        /// </summary>
        public static RegexDefault Email = new RegexDefault(1, "Email");

        /// <summary>
        /// 手机号
        /// </summary>
        public static RegexDefault Mobile = new RegexDefault(2, "Mobile");

        /// <summary>
        /// 固定电话
        /// </summary>
        public static RegexDefault Phone = new RegexDefault(3, "Phone");

        /// <summary>
        /// 邮政编码
        /// </summary>
        public static RegexDefault ZipCode = new RegexDefault(4, "ZipCode");

        /// <summary>
        /// 中文
        /// </summary>
        public static RegexDefault Chinese = new RegexDefault(5, "Chinese");

        /// <summary>
        /// Ip
        /// </summary>
        public static RegexDefault Ip = new RegexDefault(6, "Ip");

        /// <summary>
        /// 网站
        /// </summary>
        public static RegexDefault WebSite = new RegexDefault(7, "WebSite");

        /// <summary>
        /// 正整数
        /// </summary>
        public static RegexDefault PositiveInteger = new RegexDefault(8, "PositiveInteger");

        /// <summary>
        /// 负整数
        /// </summary>
        public static RegexDefault NegativeInteger = new RegexDefault(9, "NegativeInteger");

        /// <summary>
        /// 浮点数
        /// </summary>
        public static RegexDefault FloatingPointNumbers = new RegexDefault(10, "FloatingPointNumbers");

        /// <summary>
        /// 负浮点数
        /// </summary>
        public static RegexDefault NegativeFloatingPointNumbers = new RegexDefault(11, "NegativeFloatingPointNumbers");

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Regex { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public RegexDefault(int id, string name) : base(id, name)
        {
        }
    }
}
