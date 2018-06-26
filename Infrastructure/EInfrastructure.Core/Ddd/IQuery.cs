using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EInfrastructure.Core.Data;

namespace EInfrastructure.Core.Ddd
{
  public interface IQuery<TEntity, T> where TEntity : IEntity<T>
  {
    TEntity GetOne(Expression<Func<TEntity, bool>> condition, bool isTracking = true);

    PageData<TEntity> GetList(Expression<Func<TEntity, bool>> condition, int pagesize, int pageindex, bool istotal);

    List<TEntity> GetList(Expression<Func<TEntity, bool>> condition);

    bool Exists(Expression<Func<TEntity, bool>> condition);

    List<TEntity> TopN(Expression<Func<TEntity, bool>> condition, int topN);

    IQueryable<TEntity> GetListQuery(Expression<Func<TEntity, bool>> condition);

    IQueryable<TEntity> GetQueryable();
  }
}
