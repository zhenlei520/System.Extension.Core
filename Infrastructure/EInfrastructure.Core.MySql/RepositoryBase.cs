using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EInfrastructure.Core.Ddd;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.MySql
{
  public class RepositoryBase<TEntity, T> : IRepository<TEntity, T> where TEntity : Entity<T>, IAggregateRoot<T>
  {
    protected DbContext Dbcontext;

    public RepositoryBase(IUnitOfWork unitOfWork)
    {
      Dbcontext = unitOfWork as DbContext;
    }

    public void Add(TEntity entity)
    {
      Dbcontext.Set<TEntity>().Add(entity);
    }

    public void Remove(TEntity entity)
    {
      Dbcontext.Set<TEntity>().Remove(entity);
    }

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

    public TEntity FindById(T id)
    {
      return Dbcontext.Set<TEntity>().Find(id);
    }

    public void Update(TEntity entity)
    {
      Dbcontext.Set<TEntity>().Update(entity);
    }

    public virtual TEntity LoadIntegrate(T id)
    {
      throw new NotImplementedException();
    }

    public TEntity FindEntity(Expression<Func<TEntity, bool>> condition)
    {
      return Dbcontext.Set<TEntity>().FirstOrDefault(condition);
    }

    public List<TEntity> GetList(Expression<Func<TEntity, bool>> condition)
    {
      return Dbcontext.Set<TEntity>().Where(condition).ToList();
    }
  }
}