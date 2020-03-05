// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.MySql.Common;

namespace EInfrastructure.Core.MySql.Repository
{
    /// <summary>
    ///
    /// </summary>
    public class SpatialDimensionQuery<TEntity, T>
        : SpatialDimensionBaseQuery<TEntity, T>
        where TEntity : class, IEntity<T>
        where T : IComparable
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
