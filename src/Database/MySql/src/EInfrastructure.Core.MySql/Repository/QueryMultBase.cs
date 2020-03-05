// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Config.Entities.Configuration;
using EInfrastructure.Core.Config.Entities.Ioc;

namespace EInfrastructure.Core.MySql.Repository
{
    /// <summary>
    /// 基类查询仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public class QueryBase<TEntity, T, TDbContext> :
        Common.QueryBase<TEntity, T>,
        IQuery<TEntity, T, TDbContext> where TEntity : class, IEntity<T>
        where T : IComparable
        where TDbContext : IDbContext, IUnitOfWork
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        public QueryBase(IUnitOfWork<TDbContext> unitOfWork) : base(unitOfWork as IUnitOfWork)
        {
        }
    }
}
