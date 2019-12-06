// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data.Common;
using EInfrastructure.Core.Ddd;
using EInfrastructure.Core.HelpCommon;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace EInfrastructure.Core.MySql.Repository
{
    /// <summary>
    /// 执行Sql语句
    /// </summary>
    public class ExecuteBase<TDbContext> : Common.ExecuteBase
        where TDbContext : EInfrastructure.Core.Ddd.DbContext, IUnitOfWork
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ExecuteBase(IUnitOfWork<TDbContext> unitOfWork) : base(unitOfWork as IUnitOfWork)
        {
        }
    }
}
