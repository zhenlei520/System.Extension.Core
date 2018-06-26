using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EInfrastructure.Core.Data;
using EInfrastructure.Core.Ddd;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.MySql
{
  public class QueryBase<TEntity, T>
    : IQuery<TEntity, T> where TEntity : class, IEntity<T>
    where T : struct

    //IQuery<TEntity, T> where TEntity : class, IEntity<T>
  {
    protected DbContext Dbcontext;

    public QueryBase(IUnitOfWork unitOfWork)
    {
      this.Dbcontext = unitOfWork as DbContext;
    }

    public TEntity GetOne(Expression<Func<TEntity, bool>> exp, bool isTracking = true)
    {
      if (isTracking)
      {
        return Dbcontext.GetOne<TEntity, T>(exp);
      }
      else
      {
        return Dbcontext.Set<TEntity>().AsNoTracking()
          .FirstOrDefault(exp);
      }
    }

    public PageData<TEntity> GetList(Expression<Func<TEntity, bool>> exp, int pagesize, int pageindex, bool istotal)
    {
      return Dbcontext.GetList<TEntity, T>(exp, pagesize, pageindex, istotal);
    }

    public List<TEntity> GetList(Expression<Func<TEntity, bool>> exp)
    {
      return Dbcontext.GetList<TEntity, T>(exp);
    }

    public IQueryable<TEntity> GetListQuery(Expression<Func<TEntity, bool>> exp)
    {
      return Dbcontext.GetListQuery<TEntity, T>(exp);
    }

    public bool Exists(Expression<Func<TEntity, bool>> exp)
    {
      return Dbcontext.Exists<TEntity, T>(exp);
    }

    public List<TEntity> TopN(Expression<Func<TEntity, bool>> condition, int topN)
    {
      return Dbcontext.TopN<TEntity, T>(condition, topN);
    }

    public int Count(Expression<Func<TEntity, bool>> condition)
    {
      return Dbcontext.Count<TEntity, T>(condition);
    }

    public IQueryable<TEntity> GetQueryable()
    {
      return Dbcontext.Set<TEntity>();
    }
  }
}
