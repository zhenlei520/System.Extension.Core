using EInfrastructure.Core.Configuration.SeedWork;

namespace EInfrastructure.Core.Config.EntitiesExtensions.Configuration
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbContext
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public virtual DbContextType ContextType { get; set; }

        /// <summary>
        /// 调用名
        /// </summary>
        /// <returns></returns>
        public virtual string CallIndex { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public class DbContextType : Enumeration
        {
            /// <summary>
            /// 读写
            /// </summary>
            public static DbContextType ReadAndWrite = new DbContextType(0, "读写");

            /// <summary>
            /// 读
            /// </summary>
            public static DbContextType Read = new DbContextType(1, "读");

            /// <summary>
            /// 写
            /// </summary>
            public static DbContextType Write = new DbContextType(2, "写");

            /// <summary>
            ///
            /// </summary>
            /// <param name="id"></param>
            /// <param name="name"></param>
            public DbContextType(int id, string name) : base(id, name)
            {
            }
        }
    }
}
