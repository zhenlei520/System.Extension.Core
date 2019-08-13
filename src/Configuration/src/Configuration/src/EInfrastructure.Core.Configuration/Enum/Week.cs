// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.Configuration.SeedWork;

namespace EInfrastructure.Core.Configuration.Enum
{
    /// <summary>
    /// 星期N
    /// </summary>
    public class Week : Enumeration
    {
        /// <summary>
        /// 周一
        /// </summary>
        public static Week Monday = new Week(1, "周一");

        /// <summary>
        /// 周二
        /// </summary>
        public static Week Tuesday = new Week(2, "周二");

        /// <summary>
        /// 周三
        /// </summary>
        public static Week Wednesday = new Week(3, "周三");

        /// <summary>
        /// 周四
        /// </summary>
        public static Week Thursday = new Week(4, "周四");

        /// <summary>
        /// 周五
        /// </summary>
        public static Week Friday = new Week(5, "周五");

        /// <summary>
        /// 周六
        /// </summary>
        public static Week Saturday = new Week(6, "周六");

        /// <summary>
        /// 周日
        /// </summary>
        public static Week Sunday = new Week(0, "周日");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public Week(int id, string name) : base(id, name)
        {
        }
    }
}
