// Copyright (c) zhenlei520 All rights reserved.

namespace EInfrastructure.Core.Config.EnumerationExtensions
{
    /// <summary>
    /// 取整方式
    /// </summary>
    public class RectificationType : EntitiesExtensions.SeedWork.Enumeration
    {
        /// <summary>
        /// 向上取整
        /// </summary>
        public static RectificationType Celling = new RectificationType(1, "向上取整");

        /// <summary>
        /// 向下取整
        /// </summary>
        public static RectificationType Floor = new RectificationType(2, "向下取整");

        public RectificationType(int id, string name) : base(id, name)
        {
        }
    }
}
