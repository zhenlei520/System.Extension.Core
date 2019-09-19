// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data.Common;
using EInfrastructure.Core.Config.EntitiesExtensions;
using EInfrastructure.Core.HelpCommon;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.MySql
{
    /// <summary>
    /// 执行Sql语句
    /// </summary>
    public class ExecuteBase : IExecute
    {
        /// <summary>
        ///
        /// </summary>
        protected DbContext Dbcontext;

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        public ExecuteBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Dbcontext = unitOfWork as DbContext;
        }

        /// <summary>
        /// 单元模式
        /// </summary>
        public IUnitOfWork UnitOfWork => _unitOfWork;

        #region 执行Reader

        /// <summary>
        /// 执行Reader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="action">委托</param>
        public void Reader(string sql, Action<DbDataReader> action)
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
        public List<T> SqlQuery<T>(string sql)
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
        public int ExecuteSql(string sql)
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
