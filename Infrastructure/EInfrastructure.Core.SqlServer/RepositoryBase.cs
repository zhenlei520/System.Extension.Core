using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EInfrastructure.Core.Ddd;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.SqlServer
{
    public class RepositoryBase<TEntity, T> : IRepository<TEntity, T> where TEntity : Entity<T>, IAggregateRoot<T>
        where T : IComparable
    {
        protected DbContext Dbcontext;

        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            Dbcontext = unitOfWork as DbContext;
        }

        #region 根据id得到实体信息

        /// <summary>
        /// 根据id得到实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity FindById(T id)
        {
            return Dbcontext.Set<TEntity>().Find(id);
        }

        #endregion

        #region 添加单个实体信息

        /// <summary>
        /// 添加单个实体信息
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            Dbcontext.Set<TEntity>().Add(entity);
        }

        #endregion

        #region 添加集合

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(List<TEntity> entities)
        {
            Dbcontext.Set<TEntity>().AddRange(entities);
        }

        #endregion

        #region 移除数据

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            Dbcontext.Set<TEntity>().Remove(entity);
        }

        #endregion

        #region 批量删除实体

        /// <summary>
        /// 批量删除实体
        /// </summary>
        public void Removes(Expression<Func<TEntity, bool>> condition)
        {
            var query = Dbcontext.Set<TEntity>().Where(condition);
            foreach (var q in query)
            {
                Dbcontext.Set<TEntity>().Remove(q);
            }
        }

        #endregion

        #region 更新实体

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            Dbcontext.Set<TEntity>().Update(entity);
        }

        #endregion

        public virtual TEntity LoadIntegrate(T id)
        {
            throw new NotImplementedException();
        }
    }
}