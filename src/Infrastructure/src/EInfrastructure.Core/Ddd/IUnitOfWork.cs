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
        
    }
}