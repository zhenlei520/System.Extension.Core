// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Ddd;

namespace EInfrastructure.Core.SqlServer.Repository
{
    /// <summary>
    /// 执行Sql语句
    /// </summary>
    public class ExecuteBase : Common.ExecuteBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ExecuteBase(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
