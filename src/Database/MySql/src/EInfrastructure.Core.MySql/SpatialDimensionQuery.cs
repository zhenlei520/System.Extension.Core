// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Text;
using EInfrastructure.Core.Ddd;
using EInfrastructure.Core.HelpCommon;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.MySql
{
    /// <summary>
    /// 
    /// </summary>
    public class SpatialDimensionQuery : ISpatialDimensionQuery
    {
        /// <summary>
        /// 
        /// </summary>
        protected DbContext Dbcontext;

        private readonly IExecute _execute;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork">unitwork</param>
        public SpatialDimensionQuery(IUnitOfWork unitOfWork, IExecute execute)
        {
            this.Dbcontext = unitOfWork as DbContext;
            _execute = execute;
        }

        #region get list

        /// <summary>
        /// get list
        /// </summary>
        /// <param name="param"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetList<T>(SpatialDimensionParam param)
        {
            if (param == null)
            {
                return default(List<T>);
            }

            param.TableName.IsNullOrEmptyTip("The TableName cannot be empty");
            Check.True(!(param.FileKeys == null || param.FileKeys.Count == 0), "The FileKeys cannot be empty");
            Check.True(!(param.DistanceAlias == null || param.FileKeys.Count == 0), "The DistanceAlias cannot be empty");
            Check.True(!(param.Point.Equals(default(KeyValuePair<string, string>))), "The Point cannot be empty");
            Check.True(param.Distance > 0, "The distance has to be greater than 0");
            Check.True(!(param.Location.Equals(default(KeyValuePair<decimal, decimal>))), "The Location Is Error");

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ");
            string tableAlias = "epointTable";
            param.FileKeys.ForEach(fileKey =>
            {
                stringBuilder.Append($"'epointTable.{fileKey.Key}' as {fileKey.Value},");
            });
            stringBuilder.Append(
                $"((st_distance (point ({param.Point.Key}, {param.Point.Value}),point({param.Location.Key},{param.Location.Value}) ) / 0.0111)*1000) AS {param.DistanceAlias}");
            stringBuilder.Append($" FROM {param.TableName} as {tableAlias}");
            stringBuilder.Append($" HAVING {param.DistanceAlias}<{param.Distance}");
            bool isFirst = true;
            param.Sorts?.ForEach(item =>
            {
                if (isFirst)
                {
                    stringBuilder.Append(" ORDER BY ");
                }

                stringBuilder.Append($"{item.Key}");
                if (item.Value)
                {
                    stringBuilder.Append(" desc,");
                }
                else
                {
                    stringBuilder.Append(" asc,");
                }

                isFirst = false;
            });
            string sql = stringBuilder.ToString();
            if (!isFirst)
            {
                sql = sql.Substring(0, sql.Length - 1);
            }

            return _execute.SqlQuery<T>(sql);
        }

        #endregion
    }
}