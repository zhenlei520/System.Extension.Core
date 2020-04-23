// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EInfrastructure.Core.Config.Entities.Data;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Tools.Systems;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.MySql.Common
{
    /// <summary>
    /// 基类查询仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    internal class QueryBase<TEntity, T>
        where TEntity : class, IEntity<T>
        where T : IComparable
    {
        /// <summary>
        ///
        /// </summary>
        private DbContext Dbcontext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        internal QueryBase(IUnitOfWork unitOfWork)
        {
            this.Dbcontext = unitOfWork as DbContext;
        }

        #region 得到唯一标示

        /// <summary>
        /// 得到唯一标示
        /// </summary>
        /// <returns></returns>
        public virtual string GetIdentify()
        {
            return AssemblyCommon.GetReflectedInfo().Namespace;
        }

        #endregion

        #region 根据条件得到满足条件的单条信息

        /// <summary>
        /// 根据条件得到满足条件的单条信息
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="isTracking">是否跟踪</param>
        /// <returns></returns>
        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> condition, bool isTracking = true)
        {
            if (isTracking)
            {
                return Dbcontext.GetOne<TEntity, T>(condition);
            }

            return Dbcontext.Set<TEntity>().AsNoTracking()
                .FirstOrDefault(condition);
        }

        /// <summary>
        /// 根据条件得到满足条件的单条信息
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="isTracking">是否跟踪</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> condition, bool isTracking = true)
        {
            if (isTracking)
            {
                return await Dbcontext.GetOneAsync<TEntity, T>(condition);
            }

            return await Dbcontext.Set<TEntity>().AsNoTracking()
                .FirstOrDefaultAsync(condition);
        }

        #endregion

        #region 根据条件对实体进行分页

        /// <summary>
        /// 根据条件对实体进行分页
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="isTotal">是否计算总数</param>
        /// <returns></returns>
        public virtual PageData<TEntity> GetList(Expression<Func<TEntity, bool>> condition, int pageSize, int pageIndex,
            bool isTotal)
        {
            return Dbcontext.GetList<TEntity, T>(condition, pageSize, pageIndex, isTotal);
        }

        /// <summary>
        /// 根据条件对实体进行分页
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="isTotal">是否计算总数</param>
        /// <returns></returns>
        public virtual async Task<PageData<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition, int pageSize,
            int pageIndex, bool isTotal)
        {
            return await Dbcontext.GetListAsync<TEntity, T>(condition, pageSize, pageIndex, isTotal);
        }

        #endregion

        #region 根据条件获取实体列表

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> condition)
        {
            return Dbcontext.GetList<TEntity, T>(condition);
        }

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await Dbcontext.GetListAsync<TEntity, T>(condition);
        }

        #endregion

        #region 判断是否存在

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public virtual bool Exists(Expression<Func<TEntity, bool>> condition)
        {
            return Dbcontext.Exists<TEntity, T>(condition);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await Dbcontext.ExistsAsync<TEntity, T>(condition);
        }

        #endregion

        #region 根据条件得到前N项实体列表

        /// <summary>
        /// 根据条件得到前N项实体列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="topN">前N条记录</param>
        /// <returns></returns>
        public virtual List<TEntity> TopN(Expression<Func<TEntity, bool>> condition, int topN)
        {
            return Dbcontext.TopN<TEntity, T>(condition, topN);
        }

        /// <summary>
        /// 根据条件得到前N项实体列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="topN">前N条记录</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> TopNAsync(Expression<Func<TEntity, bool>> condition, int topN)
        {
            return await Dbcontext.TopNAsync<TEntity, T>(condition, topN);
        }

        #endregion

        #region 根据条件查询返回值为IQueryable

        /// <summary>
        /// 根据条件查询返回值为IQueryable
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetListQuery(Expression<Func<TEntity, bool>> condition)
        {
            return Dbcontext.GetListQuery<TEntity, T>(condition);
        }

        #endregion

        #region 获取IQueryable的方法

        /// <summary>
        /// 获取IQueryable的方法
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetQueryable()
        {
            return Dbcontext.Set<TEntity>();
        }

        #endregion

        #region 根据条件得到满足条件的数量

        /// <summary>
        /// 根据条件得到满足条件的数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> condition)
        {
            return Dbcontext.Count<TEntity, T>(condition);
        }

        /// <summary>
        /// 根据条件得到满足条件的数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await Dbcontext.CountAsync<TEntity, T>(condition);
        }

        #endregion
    }
}
