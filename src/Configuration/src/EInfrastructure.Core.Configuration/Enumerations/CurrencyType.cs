// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 货币类型
    /// </summary>
    public class CurrencyType : Enumeration
    {
        /// <summary>
        /// 人民币
        /// </summary>
        public static CurrencyType Cny = new CurrencyType(1, "CNY");

        /// <summary>
        /// 美元
        /// </summary>
        public static CurrencyType Dollar = new CurrencyType(2, "Dollar");

        /// <summary>
        /// 日元
        /// </summary>
        public static CurrencyType Yen = new CurrencyType(3, "Yen");

        /// <summary>
        /// 欧元
        /// </summary>
        public static CurrencyType Euro = new CurrencyType(4, "Euro");

        /// <summary>
        /// 韩币
        /// </summary>
        public static CurrencyType Krw = new CurrencyType(5, "Krw");

        /// <summary>
        /// 法郎
        /// </summary>
        public static CurrencyType Franc = new CurrencyType(6, "Franc");

        /// <summary>
        /// 港元
        /// </summary>
        public static CurrencyType Hkd = new CurrencyType(7, "Hkd");

        /// <summary>
        /// 加元 加拿大货币
        /// </summary>
        public static CurrencyType Cad = new CurrencyType(8, "Cad");

        /// <summary>
        /// 英镑
        /// </summary>
        public static CurrencyType Pounds = new CurrencyType(9, "Pounds");

        /// <summary>
        /// 泰铢 泰国货币
        /// </summary>
        public static CurrencyType Baht = new CurrencyType(10, "Baht");

        /// <summary>
        /// 卢布 俄罗斯货币
        /// </summary>
        public static CurrencyType Rouble = new CurrencyType(11, "Rouble");

        /// <summary>
        /// 越南盾 越南货币
        /// </summary>
        public static CurrencyType Dong = new CurrencyType(12, "Dong");

        /// <summary>
        /// 缅元 缅甸货币
        /// </summary>
        public static CurrencyType Kyat = new CurrencyType(13, "Kyat");

        /// <summary>
        /// 台币 台湾货币
        /// </summary>
        public static CurrencyType Twd = new CurrencyType(14, "Twd");

        /// <summary>
        /// 英镑 英国货币
        /// </summary>
        public static CurrencyType Pound = new CurrencyType(15, "Pound");

        /// <summary>
        /// 德国马克 德国货币
        /// </summary>
        public static CurrencyType GermanCurrency = new CurrencyType(16, "GermanCurrency");



        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public CurrencyType(int id, string name) : base(id, name)
        {
        }
    }
}
