using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EInfrastructure.Core.Config.Entities.Configuration;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Tools.Systems;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.SqlServer.Repository
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public class RepositoryBase<TEntity, T, TDbContext> :
        IRepository<TEntity, T, TDbContext>
        where TEntity : Entity<T>, IAggregateRoot<T>
        where T : IComparable
        where TDbContext : IDbContext, IUnitOfWork
    {
        private readonly EInfrastructure.Core.SqlServer.Common.RepositoryBase<TEntity, T> _repositoryBase;
        protected DbContext Dbcontext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        // ReSharper disable once SuspiciousTypeConversion.Global
        public RepositoryBase(IUnitOfWork<TDbContext> unitOfWork)
        {
            Dbcontext = unitOfWork as DbContext;
            UnitOfWork = unitOfWork;
            _repositoryBase = new EInfrastructure.Core.SqlServer.Common.RepositoryBase<TEntity, T>(Dbcontext);
        }

        /// <summary>
        /// 
        /// </summary>
        public IUnitOfWork<TDbContext> UnitOfWork { get; }

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

        #region 根据id得到实体信息

        /// <summary>
        /// 根据id得到实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity FindById(T id)
        {
            return _repositoryBase.FindById(id);
        }

        /// <summary>
        /// 根据id得到实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> FindByIdAsync(T id)
        {
            return await _repositoryBase.FindByIdAsync(id);
        }

        #endregion

        #region 添加实体信息

        /// <summary>
        /// 添加单个实体信息
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            _repositoryBase.Add(entity);
        }

        /// <summary>
        /// 添加单个实体信息
        /// </summary>
        /// <param name="entity"></param>
        public async Task AddAsync(TEntity entity)
        {
            await _repositoryBase.AddAsync(entity);
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(List<TEntity> entities)
        {
            _repositoryBase.AddRange(entities);
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="entities"></param>
        public async Task AddRangeAsync(List<TEntity> entities)
        {
            await _repositoryBase.AddRangeAsync(entities);
        }

        #endregion

        #region 移除数据

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            _repositoryBase.Remove(entity);
        }

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="entitys"></param>
        public void RemoveRange(params TEntity[] entitys)
        {
            _repositoryBase.RemoveRange(entitys);
        }

        #endregion

        #region 更新实体

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            _repositoryBase.Update(entity);
        }

        /// <summary>
        /// 更新实体集合
        /// </summary>
        /// <param name="entitys"></param>
        public virtual void UpdateRange(params TEntity[] entitys)
        {
            _repositoryBase.UpdateRange(entitys);
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
            return _repositoryBase.LoadIntegrate(id);
        }

        #endregion
    }
}
