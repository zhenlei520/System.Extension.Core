// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 生肖信息
    /// </summary>
    public class Animal :Enumeration
    {
        /// <summary>
        /// 鼠
        /// </summary>
        public static Animal Rat = new Animal(1, "鼠");

        /// <summary>
        /// 牛
        /// </summary>
        public static Animal Ox = new Animal(2, "牛");

        /// <summary>
        /// 虎
        /// </summary>
        public static Animal Tiger = new Animal(3, "虎");

        /// <summary>
        /// 兔
        /// </summary>
        public static Animal Hare = new Animal(4, "兔");

        /// <summary>
        /// 龙
        /// </summary>
        public static Animal Dragon = new Animal(5, "龙");

        /// <summary>
        /// 蛇
        /// </summary>
        public static Animal Snake = new Animal(6, "蛇");

        /// <summary>
        /// 蛇
        /// </summary>
        public static Animal Horse = new Animal(7, "马");

        /// <summary>
        /// 蛇
        /// </summary>
        public static Animal Sheep = new Animal(8, "羊");

        /// <summary>
        /// 猴
        /// </summary>
        public static Animal Monkey = new Animal(9, "猴");

        /// <summary>
        /// 鸡
        /// </summary>
        public static Animal Cock = new Animal(10, "鸡");

        /// <summary>
        /// 狗
        /// </summary>
        public static Animal Dog = new Animal(11, "狗");

        /// <summary>
        /// 猪
        /// </summary>
        public static Animal Boar = new Animal(12, "猪");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">生肖id</param>
        /// <param name="name">生肖描述</param>
        public Animal(int id, string name) : base(id, name)
        {
        }
    }
}
