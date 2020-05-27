// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EInfrastructure.Core.Config.Entities.Configuration;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Tools.Systems;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.MySql.Common
{
    /// <summary>
    /// 基类增删改仓储实现类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    internal class RepositoryBase<TEntity, T>
        where TEntity : Entity<T>, IAggregateRoot<T>
        where T : IComparable
    {
        /// <summary>
        ///
        /// </summary>
        private DbContext Dbcontext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            Dbcontext = unitOfWork as DbContext;
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

        #region 根据id得到实体信息

        /// <summary>
        /// 根据id得到实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity FindById(T id)
        {
            return Dbcontext.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// 根据id得到实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindByIdAsync(T id)
        {
            return await Dbcontext.Set<TEntity>().FindAsync(id);
        }

        #endregion

        #region 添加单个实体信息

        /// <summary>
        /// 添加单个实体信息
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(TEntity entity)
        {
            Dbcontext.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// 添加单个实体信息
        /// </summary>
        /// <param name="entity"></param>
        public virtual async Task AddAsync(TEntity entity)
        {
            await Dbcontext.Set<TEntity>().AddAsync(entity);
        }

        #endregion

        #region 添加集合

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AddRange(List<TEntity> entities)
        {
            Dbcontext.Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="entities"></param>
        public virtual async Task AddRangeAsync(List<TEntity> entities)
        {
            await Dbcontext.Set<TEntity>().AddRangeAsync(entities);
        }

        #endregion

        #region 移除数据

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Remove(TEntity entity)
        {
            Dbcontext.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="entitys"></param>
        public virtual void RemoveRange(params TEntity[] entitys)
        {
            Dbcontext.Set<TEntity>().RemoveRange(entitys);
        }
        #endregion

        #region 更新实体

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            Dbcontext.Set<TEntity>().Update(entity);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entitys"></param>
        public virtual void UpdateRange(params TEntity[] entitys)
        {
            Dbcontext.Set<TEntity>().UpdateRange(entitys);
        }

        #endregion

        #region 根据id得到实体信息（需要重写）

        /// <summary>
        /// 根据id得到实体信息（需要重写）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual TEntity LoadIntegrate(T id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
