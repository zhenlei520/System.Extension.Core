// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Config.EntitiesExtensions.SeedWork;

namespace EInfrastructure.Core.Tools.Enumerations
{
    /// <summary>
    /// 生肖信息
    /// </summary>
    public class Animal :Enumeration
    {
        /// <summary>
        /// 鼠
        /// </summary>
        public static Animal Rat = new Animal(0, "鼠");

        /// <summary>
        /// 牛
        /// </summary>
        public static Animal Ox = new Animal(1, "牛");

        /// <summary>
        /// 虎
        /// </summary>
        public static Animal Tiger = new Animal(2, "虎");

        /// <summary>
        /// 兔
        /// </summary>
        public static Animal Hare = new Animal(3, "兔");

        /// <summary>
        /// 龙
        /// </summary>
        public static Animal Dragon = new Animal(4, "龙");

        /// <summary>
        /// 蛇
        /// </summary>
        public static Animal Snake = new Animal(5, "蛇");

        /// <summary>
        /// 蛇
        /// </summary>
        public static Animal Horse = new Animal(6, "马");

        /// <summary>
        /// 蛇
        /// </summary>
        public static Animal Sheep = new Animal(7, "羊");

        /// <summary>
        /// 猴
        /// </summary>
        public static Animal Monkey = new Animal(8, "猴");

        /// <summary>
        /// 鸡
        /// </summary>
        public static Animal Cock = new Animal(9, "鸡");

        /// <summary>
        /// 狗
        /// </summary>
        public static Animal Dog = new Animal(10, "狗");

        /// <summary>
        /// 猪
        /// </summary>
        public static Animal Boar = new Animal(11, "猪");

        public Animal(int id, string name) : base(id, name)
        {
        }
    }
}
