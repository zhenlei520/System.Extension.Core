// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

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
    /// 基类查询仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class QueryBase<TEntity, T, TDbContext>
        : Common.QueryBase<TEntity, T> where TEntity : class, IEntity<T>
        where T : IComparable
        where TDbContext : EInfrastructure.Core.Ddd.DbContext, IUnitOfWork
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
