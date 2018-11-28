using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EInfrastructure.Core.Data;
using EInfrastructure.Core.Data.EntitiesExtension;
using EInfrastructure.Core.Ddd;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.SqlServer
{
  public static class QueryExtension
  {
    public static TEntity GetOne<TEntity,T>(this DbContext dbcontext, Expression<Func<TEntity, bool>> exp) where TEntity : class, IEntity<T>
    {
      return dbcontext.Set<TEntity>()
          .FirstOrDefault(exp);
    }

    public static PageData<TEntity> GetList<TEntity, T>(this DbContext dbcontext, Expression<Func<TEntity, bool>> exp, int pagesize, int pageindex, bool istotal)
        where TEntity : class, IEntity<T>
    {
      return dbcontext.Set<TEntity>()
            .Where(exp)
            .OrderByDescending(t => t.Id)
            .ListPager(pagesize, pageindex, istotal);
    }

    public static List<TEntity> GetList<TEntity, T>(this DbContext dbcontext, Expression<Func<TEntity, bool>> exp)
        where TEntity : class, IEntity<T>
    {
      return dbcontext.Set<TEntity>()
          .Where(exp).ToList();
    }

    public static IQueryable<TEntity> GetListQuery<TEntity, T>(this DbContext dbcontext, Expression<Func<TEntity, bool>> exp)
        where TEntity : class, IEntity<T>
    {
      return dbcontext.Set<TEntity>()
          .Where(exp);
    }

    public static bool Exists<TEntity, T>(this DbContext dbcontext, Expression<Func<TEntity, bool>> exp)
        where TEntity : class, IEntity<T>
    {
      return dbcontext.Set<TEntity>()
          .Any(exp);
    }

    public static List<TEntity> TopN<TEntity, T>(this DbContext dbcontext, Expression<Func<TEntity, bool>> condition, int topN)
        where TEntity : class, IEntity<T>
    {
      return dbcontext.Set<TEntity>()
          .Where(condition).TopN(topN).ToList();
    }

    public static int Count<TEntity,T>(this DbContext dbcontext, Expression<Func<TEntity, bool>> condition) where TEntity : class, IEntity<T>
    {
      return dbcontext.Set<TEntity>()
          .Count(condition);
    }
  }
}
