// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Ddd
{
    /// <summary>
    /// 单元模式
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 提交保存
        /// </summary>
        /// <returns></returns>
        bool Commit();
    }

    /// <summary>
    /// 单元模式，可以指定数据库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUnitOfWork<T> where T : DbContext, IUnitOfWork
    {
    }

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
        public class DbContextType 
        {
            /// <summary>
            /// 
            /// </summary>
            public string Name { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public int Id { get; private set; }
            
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
            public DbContextType(int id, string name) 
            {
                Id = id;
                Name = name;
            }
        }
    }
}