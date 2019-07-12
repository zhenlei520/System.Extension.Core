// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data.Common;

namespace EInfrastructure.Core.Config.EntitiesExtensions
{
    /// <summary>
    /// 执行Sql语句
    /// </summary>
    public interface IExecute
    {
        /// <summary>
        /// 单元模式
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 执行Reader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="action">委托</param>
        void Reader(string sql, Action<DbDataReader> action);

        /// <summary>
        /// 执行Query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        List<T> SqlQuery<T>(string sql);

        /// <summary>
        /// 执行Sql命令
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        int ExecuteSql(string sql);
    }
}
