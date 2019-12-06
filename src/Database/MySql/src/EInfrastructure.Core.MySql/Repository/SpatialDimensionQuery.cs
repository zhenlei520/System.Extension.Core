// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data.Common;
using EInfrastructure.Core.Ddd;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.MySql.Common;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

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
