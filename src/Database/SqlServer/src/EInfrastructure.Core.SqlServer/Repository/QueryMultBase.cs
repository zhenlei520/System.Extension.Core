// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EInfrastructure.Core.Config.Entities.Configuration;
using EInfrastructure.Core.Config.Entities.Data;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Tools.Systems;

namespace EInfrastructure.Core.SqlServer.Repository
{
    /// <summary>
    /// 基类查询仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public class QueryBase<TEntity, T, TDbContext> :
        IQuery<TEntity, T, TDbContext> where TEntity : class, IEntity<T>
        where T : IComparable
        where TDbContext : IDbContext, IUnitOfWork
    {
        private EInfrastructure.Core.SqlServer.Common.QueryBase<TEntity, T> _queryBase;

        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        public QueryBase(IUnitOfWork<TDbContext> unitOfWork)
        {
            _queryBase = new EInfrastructure.Core.SqlServer.Common.QueryBase<TEntity, T>(unitOfWork);
        }

        #region 得到唯一标示

        /// <summary>
        /// 得到唯一标示
        /// </summary>
        /// <returns></returns>
        public string GetIdentify()
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
        public TEntity GetOne(Expression<Func<TEntity, bool>> condition, bool isTracking = true)
        {
            return _queryBase.GetOne(condition, isTracking);
        }

        /// <summary>
        /// 根据条件得到满足条件的单条信息
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="isTracking">是否跟踪</param>
        /// <returns></returns>
        public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> condition, bool isTracking = true)
        {
            return await _queryBase.GetOneAsync(condition, isTracking);
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
        public PageData<TEntity> GetList(Expression<Func<TEntity, bool>> condition, int pageSize, int pageIndex,
            bool isTotal)
        {
            return _queryBase.GetList(condition, pageSize, pageIndex, isTotal);
        }

        /// <summary>
        /// 根据条件对实体进行分页
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="isTotal">是否计算总数</param>
        /// <returns></returns>
        public async Task<PageData<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition, int pageSize,
            int pageIndex, bool isTotal)
        {
            return await _queryBase.GetListAsync(condition, pageSize, pageIndex, isTotal);
        }

        #endregion

        #region 根据条件获取实体列表

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> condition)
        {
            return _queryBase.GetList(condition);
        }

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _queryBase.GetListAsync(condition);
        }

        #endregion

        #region 判断是否存在

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public bool Exists(Expression<Func<TEntity, bool>> condition)
        {
            return _queryBase.Exists(condition);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _queryBase.ExistsAsync(condition);
        }

        #endregion

        #region 根据条件得到前N项实体列表

        /// <summary>
        /// 根据条件得到前N项实体列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="topN">前N条记录</param>
        /// <returns></returns>
        public List<TEntity> TopN(Expression<Func<TEntity, bool>> condition, int topN)
        {
            return _queryBase.TopN(condition, topN);
        }

        /// <summary>
        /// 根据条件得到前N项实体列表
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="topN">前N条记录</param>
        /// <returns></returns>
        public async Task<List<TEntity>> TopNAsync(Expression<Func<TEntity, bool>> condition, int topN)
        {
            return await _queryBase.TopNAsync(condition, topN);
        }

        #endregion

        #region 根据条件查询返回值为IQueryable

        /// <summary>
        /// 根据条件查询返回值为IQueryable
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetListQuery(Expression<Func<TEntity, bool>> condition)
        {
            return _queryBase.GetListQuery(condition);
        }

        #endregion

        #region 获取IQueryable的方法

        /// <summary>
        /// 获取IQueryable的方法
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetQueryable()
        {
            return _queryBase.GetQueryable();
        }

        #endregion

        #region 根据条件得到满足条件的数量

        /// <summary>
        /// 根据条件得到满足条件的数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> condition)
        {
            return _queryBase.Count(condition);
        }

        /// <summary>
        /// 根据条件得到满足条件的数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _queryBase.CountAsync(condition);
        }

        #endregion
    }
}
