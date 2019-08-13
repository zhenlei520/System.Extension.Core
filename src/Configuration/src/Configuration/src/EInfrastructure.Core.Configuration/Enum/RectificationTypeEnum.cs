// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.Configuration.SeedWork;

namespace EInfrastructure.Core.Configuration.Enum
{
    /// <summary>
    /// 取整方式
    /// </summary>
    public class RectificationType : Enumeration
    {
        public static RectificationType Celling = new RectificationType(1, "向上取整");
        public static RectificationType Floor = new RectificationType(2, "向下取整");

        public RectificationType(int id, string name) : base(id, name)
        {
        }
    }
}
