// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Config.EntitiesExtensions;

namespace EInfrastructure.Core.SqlServer.Repository
{
    /// <summary>
    /// 执行Sql语句
    /// </summary>
    public class ExecuteMultBase<TDbContext> : Common.ExecuteBase
        where TDbContext : EInfrastructure.Core.Config.EntitiesExtensions.Configuration.DbContext, IUnitOfWork
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ExecuteMultBase(IUnitOfWork<TDbContext> unitOfWork) : base(unitOfWork as IUnitOfWork)
        {
        }
    }
}
