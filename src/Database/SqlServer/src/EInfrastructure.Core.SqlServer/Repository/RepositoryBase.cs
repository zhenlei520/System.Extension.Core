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
    /// 基类增删改仓储实现类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBase<TEntity, T> : Common.RepositoryBase<TEntity, T>
        where TEntity : Entity<T>, IAggregateRoot<T>
        where T : IComparable
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        public RepositoryBase(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
