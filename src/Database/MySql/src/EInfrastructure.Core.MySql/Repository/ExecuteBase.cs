// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data.Common;
using EInfrastructure.Core.Config.EntitiesExtensions;
using EInfrastructure.Core.HelpCommon;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.MySql.Repository
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
