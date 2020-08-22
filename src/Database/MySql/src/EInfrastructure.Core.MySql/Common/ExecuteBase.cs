// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data.Common;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Tools;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.MySql.Common
{
    /// <summary>
    /// 执行Sql语句
    /// </summary>
    internal class ExecuteBase
    {
        /// <summary>
        ///
        /// </summary>
        private DbContext Dbcontext;

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext">上下文</param>
        public ExecuteBase(DbContext dbContext)
        {
            Dbcontext = dbContext;
        }

        #region 执行Reader

        /// <summary>
        /// 执行Reader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="action">委托</param>
        public virtual void Reader(string sql, Action<DbDataReader> action)
        {
            var conn = Dbcontext.Database.GetDbConnection();

            try
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    string query = sql;

                    command.CommandText = query;

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                action(reader);
                            }
                        }
                    }
                }
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region 执行Query

        /// <summary>
        /// 执行Query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual List<T> SqlQuery<T>(string sql)
        {
            List<T> list = new List<T>();

            var conn = Dbcontext.Database.GetDbConnection();

            try
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    string query = sql;

                    command.CommandText = query;

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            list = reader.ConvertReaderToList<T>();
                        }
                    }
                }
            }
            finally
            {
                conn.Close();
            }


            return list;
        }

        #endregion

        #region 执行Sql命令

        /// <summary>
        /// 执行Sql命令
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int ExecuteSql(string sql)
        {
            var conn = Dbcontext.Database.GetDbConnection();
            int rowAffected;
            try
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    rowAffected = command.ExecuteNonQuery();
                }
            }
            finally
            {
                conn.Close();
            }

            return rowAffected;
        }

        #endregion
    }
}
