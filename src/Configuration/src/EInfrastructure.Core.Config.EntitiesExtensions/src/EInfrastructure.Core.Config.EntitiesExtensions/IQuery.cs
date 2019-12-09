// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EInfrastructure.Core.Configuration.Data;

namespace EInfrastructure.Core.Config.EntitiesExtensions
{
    /// <summary>
    /// 基类查询仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    public interface IQuery<TEntity, T> where TEntity : IEntity<T> where T : IComparable
    {
        /// <summary>
        /// 得到实现类唯一标示
        /// </summary>
        /// <returns></returns>
        string GetIdentify();

        /// <summary>
        /// 根据条件得到满足条件的单条信息
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="isTracking">是否跟踪</param>
        /// <returns></returns>
        TEntity GetOne(Expression<Func<TEntity, bool>> condition, bool isTracking = true);

        /// <summary>
        /// 根据条件对实体进行分页
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">页码</param>
        /// <param name="istotal">是否计算总数</param>
        /// <returns></returns>
        PageData<TEntity> GetList(Expression<Func<TEntity, bool>> condition, int pagesize, int pageindex, bool istotal);

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        List<TEntity> GetList(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// 根据条件得到前N项实体列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="topN">前N条记录</param>
        /// <returns></returns>
        List<TEntity> TopN(Expression<Func<TEntity, bool>> condition, int topN);

        /// <summary>
        /// 根据条件查询返回值为IQueryable
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        IQueryable<TEntity> GetListQuery(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// 获取IQueryable的方法
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable();

        /// <summary>
        /// 根据条件得到满足条件的数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> condition);
    }

    /// <summary>
    /// 基类查询仓储(支持多数据库)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IQuery<TEntity, T, TDbContext> : IQuery<TEntity, T>
        where TEntity : IEntity<T>
        where T : IComparable
        where TDbContext : EInfrastructure.Core.Config.EntitiesExtensions.Configuration.DbContext, IUnitOfWork
    {

    }
}
