using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EInfrastructure.Core.Data;
using EInfrastructure.Core.Data.EntitiesExtension;
using EInfrastructure.Core.Ddd;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace EInfrastructure.Core.SqlServer.Repository
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public class RepositoryBase<TEntity, T, TDbContext> : Common.RepositoryBase<TEntity, T>
        where TEntity : Entity<T>, IAggregateRoot<T>
        where T : IComparable
        where TDbContext : EInfrastructure.Core.Ddd.DbContext, IUnitOfWork
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        // ReSharper disable once SuspiciousTypeConversion.Global
        public RepositoryBase(IUnitOfWork<TDbContext> unitOfWork) : base(unitOfWork as IUnitOfWork)
        {
        }
    }
}
