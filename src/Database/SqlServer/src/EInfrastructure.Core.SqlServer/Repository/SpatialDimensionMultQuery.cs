// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Config.Entities.Configuration;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.SqlServer.Common;

namespace EInfrastructure.Core.SqlServer.Repository
{
    /// <summary>
    ///
    /// </summary>
    public class SpatialDimensionQuery<TEntity, T, TDbContext>
        : SpatialDimensionBaseQuery<TEntity, T>, ISpatialDimensionQuery<TEntity, T, TDbContext>
        where TEntity : class, IEntity<T>
        where T : IComparable
        where TDbContext : IDbContext, IUnitOfWork
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="execute"></param>
        public SpatialDimensionQuery(IUnitOfWork unitOfWork, IExecute execute) : base(unitOfWork, execute)
        {
        }
    }
}
