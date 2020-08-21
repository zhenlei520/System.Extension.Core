// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 国家类型
    /// </summary>
    public class Nationality : Enumeration
    {
        /// <summary>
        /// 未知
        /// </summary>
        public static Nationality Unknow = new Nationality(1, "未知");

        /// <summary>
        /// 中国
        /// </summary>
        public static Nationality China = new Nationality(2, "中国");

        /// <summary>
        /// 日本
        /// </summary>
        public static Nationality Japan = new Nationality(3, "日本");

        /// <summary>
        /// 美国
        /// </summary>
        public static Nationality Usa = new Nationality(4, "美国");

        /// <summary>
        /// 韩国
        /// </summary>
        public static Nationality Korea = new Nationality(5, "韩国");

        /// <summary>
        /// 英国
        /// </summary>
        public static Nationality England = new Nationality(6, "英国");

        /// <summary>
        /// 加拿大
        /// </summary>
        public static Nationality Canada = new Nationality(7, "加拿大");

        /// <summary>
        /// 西班牙
        /// </summary>
        public static Nationality Spain = new Nationality(8, "西班牙");

        /// <summary>
        /// 法国
        /// </summary>
        public static Nationality France = new Nationality(9, "法国");

        /// <summary>
        /// 德国
        /// </summary>
        public static Nationality Germany = new Nationality(10, "德国");

        /// <summary>
        /// 巴西
        /// </summary>
        public static Nationality Brazil = new Nationality(11, "巴西");

        /// <summary>
        /// 意大利
        /// </summary>
        public static Nationality Italy = new Nationality(12, "意大利");

        /// <summary>
        /// 墨西哥
        /// </summary>
        public static Nationality Mexico = new Nationality(13, "墨西哥");

        /// <summary>
        /// 俄罗斯
        /// </summary>
        public static Nationality Russia = new Nationality(14, "俄罗斯");

        /// <summary>
        /// 土耳其
        /// </summary>
        public static Nationality Turkey = new Nationality(15, "土耳其");

        /// <summary>
        /// 印度
        /// </summary>
        public static Nationality Indian = new Nationality(16, "印度");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">描述</param>
        public Nationality(int id, string name) : base(id, name)
        {
        }
    }
}
