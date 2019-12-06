// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EInfrastructure.Core.Data;
using EInfrastructure.Core.Data.EntitiesExtension;
using EInfrastructure.Core.Ddd;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace EInfrastructure.Core.MySql
{
    /// <summary>
    /// 查询扩展类
    /// </summary>
    internal static class QueryExtension
    {
        #region 得到满足条件的单个实体

        /// <summary>
        /// 得到满足条件的单个实体
        /// </summary>
        /// <param name="dbContext">上下文</param>
        /// <param name="exp">条件</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static TEntity GetOne<TEntity, T>(this DbContext dbContext, Expression<Func<TEntity, bool>> exp)
            where TEntity : class, IEntity<T>
        {
            return dbContext.Set<TEntity>()
                .FirstOrDefault(exp);
        }

        #endregion

        #region 根据条件得到分页列表

        /// <summary>
        /// 根据条件得到分页列表
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="exp">条件</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageindex">页码</param>
        /// <param name="isTotal">是否计算总数</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static PageData<TEntity> GetList<TEntity, T>(this DbContext dbContext,
            Expression<Func<TEntity, bool>> exp, int pageSize, int pageindex, bool isTotal)
            where TEntity : class, IEntity<T>
        {
            return dbContext.Set<TEntity>()
                .Where(exp)
                .OrderByDescending(t => t.Id)
                .ListPager(pageSize, pageindex, isTotal);
        }

        #endregion

        #region 得到实体列表

        /// <summary>
        /// 得到实体列表
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="exp">条件</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static List<TEntity> GetList<TEntity, T>(this DbContext dbContext, Expression<Func<TEntity, bool>> exp)
            where TEntity : class, IEntity<T>
        {
            return dbContext.Set<TEntity>()
                .Where(exp).ToList();
        }

        #endregion

        #region 根据条件查询返回值为IQueryable

        /// <summary>
        /// 根据条件查询返回值为IQueryable
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="exp">条件</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static IQueryable<TEntity> GetListQuery<TEntity, T>(this DbContext dbContext,
            Expression<Func<TEntity, bool>> exp)
            where TEntity : class, IEntity<T>
        {
            return dbContext.Set<TEntity>()
                .Where(exp);
        }

        #endregion

        #region 判断是否存在

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="exp">条件</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static bool Exists<TEntity, T>(this DbContext dbContext, Expression<Func<TEntity, bool>> exp)
            where TEntity : class, IEntity<T>
        {
            return dbContext.Set<TEntity>()
                .Any(exp);
        }

        #endregion

        #region 根据条件得到前N项实体列表

        /// <summary>
        /// 根据条件得到前N项实体列表
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="condition">条件</param>
        /// <param name="topN">前N条记录</param>
        /// <returns></returns>
        internal static List<TEntity> TopN<TEntity, T>(this DbContext dbContext,
            Expression<Func<TEntity, bool>> condition, int topN)
            where TEntity : class, IEntity<T>
        {
            return dbContext.Set<TEntity>()
                .Where(condition).TopN(topN).ToList();
        }

        #endregion

        #region 根据条件得到满足条件的数量

        /// <summary>
        /// 根据条件得到满足条件的数量
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        internal static int Count<TEntity, T>(this DbContext dbContext, Expression<Func<TEntity, bool>> condition)
            where TEntity : class, IEntity<T>
        {
            return dbContext.Set<TEntity>()
                .Count(condition);
        }

        #endregion
    }
}
