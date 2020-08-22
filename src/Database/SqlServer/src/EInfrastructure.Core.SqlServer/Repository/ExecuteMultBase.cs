// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data.Common;
using EInfrastructure.Core.Config.Entities.Configuration;
using EInfrastructure.Core.Config.Entities.Ioc;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.SqlServer.Repository
{
    /// <summary>
    /// 执行Sql语句
    /// </summary>
    public class ExecuteBase<TDbContext> : IExecute<TDbContext>
        where TDbContext : IDbContext, IUnitOfWork
    {
        private readonly EInfrastructure.Core.SqlServer.Common.ExecuteBase _executeBase;

        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ExecuteBase(IUnitOfWork<TDbContext> unitOfWork)
        {
            var dbcontext = unitOfWork as DbContext;
            _executeBase = new EInfrastructure.Core.SqlServer.Common.ExecuteBase(dbcontext);
        }

        #region 执行Reader（查询）

        /// <summary>
        /// 执行Reader（查询）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="action">委托</param>
        public void Reader(string sql, Action<DbDataReader> action)
        {
            _executeBase.Reader(sql, action);
        }

        #endregion

        #region 执行Query

        /// <summary>
        /// 执行Query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">待执行的sql</param>
        /// <returns></returns>
        public List<T> SqlQuery<T>(string sql)
        {
            return _executeBase.SqlQuery<T>(sql);
        }

        #endregion

        #region 执行Sql命令

        /// <summary>
        /// 执行Sql命令
        /// </summary>
        /// <param name="sql">待执行的sql语句</param>
        /// <returns></returns>
        public int ExecuteSql(string sql)
        {
            return _executeBase.ExecuteSql(sql);
        }

        #endregion
    }
}
