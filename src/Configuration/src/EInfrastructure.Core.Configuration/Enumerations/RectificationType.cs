// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 取整方式
    /// </summary>
    public class RectificationType : Enumeration
    {
        /// <summary>
        /// 向上取整
        /// </summary>
        public static RectificationType Celling = new RectificationType(1, "向上取整");

        /// <summary>
        /// 向下取整
        /// </summary>
        public static RectificationType Floor = new RectificationType(2, "向下取整");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">描述</param>
        public RectificationType(int id, string name) : base(id, name)
        {
        }
    }
}
