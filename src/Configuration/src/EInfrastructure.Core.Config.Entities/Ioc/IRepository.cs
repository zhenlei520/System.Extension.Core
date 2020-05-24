// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EInfrastructure.Core.Config.Entities.Configuration;

namespace EInfrastructure.Core.Config.Entities.Ioc
{
    /// <summary>
    /// 基类增删改仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<TEntity, T> where TEntity : IAggregateRoot<T> where T : IComparable
    {
        /// <summary>
        /// 单元模式
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        string GetIdentify();

        /// <summary>
        /// 根据id得到实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindById(T id);

        /// <summary>
        /// 根据id得到实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(T id);

        /// <summary>
        /// 添加单个实体信息
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// 添加单个实体信息
        /// </summary>
        /// <param name="entity"></param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(List<TEntity> entities);

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="entities"></param>
        Task AddRangeAsync(List<TEntity> entities);

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="entitys"></param>
        void RemoveRange(params TEntity[] entitys);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 更新实体集合
        /// </summary>
        /// <param name="entitys"></param>
        void UpdateRange(params TEntity[] entitys);

        /// <summary>
        /// 根据id得到实体信息（需要重写）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity LoadIntegrate(T id);
    }

    /// <summary>
    /// 基类增删改仓储接口（支持多数据库）
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IRepository<TEntity, T, TDbContext> :
        IRepository<TEntity, T>
        where TEntity : IAggregateRoot<T>
        where T : IComparable
        where TDbContext : IDbContext, IUnitOfWork
    {
    }
}
