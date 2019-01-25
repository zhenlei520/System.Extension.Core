using System;
using System.Collections.Generic;

namespace EInfrastructure.Core.Ddd
{
    public interface IRepository<TEntity, T> where TEntity : IAggregateRoot<T> where T : IComparable
    {
        /// <summary>
        /// 根据id得到实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindById(T id);

        /// <summary>
        /// 添加单个实体信息
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(List<TEntity> entities);

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 加载实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity LoadIntegrate(T id);
    }
}