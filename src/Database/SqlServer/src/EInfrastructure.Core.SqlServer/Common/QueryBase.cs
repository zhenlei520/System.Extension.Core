// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EInfrastructure.Core.Data;
using EInfrastructure.Core.Ddd;
using EInfrastructure.Core.HelpCommon.Systems;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace EInfrastructure.Core.SqlServer.Common
{
    /// <summary>
    /// 基类查询仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class QueryBase<TEntity, T>
        : IQuery<TEntity, T> where TEntity : class, IEntity<T>
        where T : IComparable
    {
        /// <summary>
        ///
        /// </summary>
        protected DbContext Dbcontext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public QueryBase(IUnitOfWork unitOfWork)
        {
            this.Dbcontext = unitOfWork as DbContext;
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
            if (isTracking)
            {
                return Dbcontext.GetOne<TEntity, T>(condition);
            }

            return Dbcontext.Set<TEntity>().AsNoTracking()
                .FirstOrDefault(condition);
        }

        #endregion

        #region 根据条件对实体进行分页

        /// <summary>
        /// 根据条件对实体进行分页
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">页码</param>
        /// <param name="istotal">是否计算总数</param>
        /// <returns></returns>
        public PageData<TEntity> GetList(Expression<Func<TEntity, bool>> condition, int pagesize, int pageindex, bool istotal)
        {
            return Dbcontext.GetList<TEntity, T>(condition, pagesize, pageindex, istotal);
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
            return Dbcontext.GetList<TEntity, T>(condition);
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
            return Dbcontext.Exists<TEntity, T>(condition);
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
            return Dbcontext.TopN<TEntity, T>(condition, topN);
        }

        #endregion

        #region 根据条件查询返回值为IQueryable

        /// <summary>
        /// 根据条件查询返回值为IQueryable
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public IQueryable<TEntity> GetListQuery(Expression<Func<TEntity, bool>> condition)
        {
            return Dbcontext.GetListQuery<TEntity, T>(condition);
        }

        #endregion

        #region 获取IQueryable的方法

        /// <summary>
        /// 获取IQueryable的方法
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetQueryable()
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
        public int Count(Expression<Func<TEntity, bool>> condition)
        {
            return Dbcontext.Count<TEntity, T>(condition);
        }

        #endregion
    }
}
