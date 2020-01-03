using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EInfrastructure.Core.Config.Entities.Data;
using EInfrastructure.Core.Config.EntitiesExtensions;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Tools;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.MySql.Common
{
    /// <summary>
    ///
    /// </summary>
    public class SpatialDimensionBaseQuery<TEntity, T>
        : ISpatialDimensionQuery<TEntity, T> where TEntity : class, IEntity<T>
        where T : IComparable
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
        public SpatialDimensionBaseQuery(IUnitOfWork unitOfWork, IExecute execute)
        {
            Dbcontext = unitOfWork as DbContext;
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

            CheckParam(param);
            return _execute.SqlQuery<T>(GetDataSql(param));
        }

        #endregion

        #region get list

        /// <summary>
        /// get list
        /// </summary>
        /// <param name="param"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public PageData<T> GetPageData<T>(SpatialDimensionPagingParam param)
        {
            if (param == null)
            {
                return default(PageData<T>);
            }

            CheckParam(param);
            Check.True(param.PageIndex > 0, "The Pageindex must be greater than 0");
            Check.True(param.PageSize > 0 || param.PageSize == -1, "The Pageindex must be greater than 0 or equals -1");
            PageData<T> pageData = new PageData<T>()
            {
                Data = GetList<T>(param),
                RowCount = _execute.ExecuteSql(GetCountSql(param))
            };
            return pageData;
        }

        #endregion

        #region get IQueryable

        /// <summary>
        /// get list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<TEntity> GetQueryable(SpatialDimensionParam param)
        {
            CheckParam(param);
            return Dbcontext.Set<TEntity>().FromSql(GetDataSql(param));
        }

        #endregion

        #region private methods

        #region Check Param

        /// <summary>
        /// Check Param
        /// </summary>
        /// <param name="param"></param>
        private void CheckParam(SpatialDimensionParam param)
        {
            param.TableName.IsNullOrEmptyTip("The TableName cannot be empty");
            Check.True(!(param.FileKeys == null || param.FileKeys.Count == 0), "The FileKeys cannot be empty");
            Check.True(!(string.IsNullOrEmpty(param.DistanceAlias)),
                "The DistanceAlias cannot be empty");
            Check.True(!(param.Point.Equals(default(KeyValuePair<string, string>))), "The Point cannot be empty");
            Check.True(param.Distance > 0 || param.Distance == -1, "The distance has to be greater than 0 or equal -1");
            Check.True(param.MinDistance >= 0, "The distance has to be greater than 0 or equals 0");
            Check.True(!param.Location.Equals(default(KeyValuePair<decimal, decimal>)), "The Location Is Error");
        }

        #endregion

        #region Get sql by Latitude and longitude

        /// <summary>
        /// Get sql by Latitude and longitude
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetDataSql(SpatialDimensionParam param)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select ");
            string tableAlias = "epointTable";
            param.FileKeys.ForEach(fileKey =>
            {
                stringBuilder.Append($"epointTable.{fileKey.Key} as '{fileKey.Value}',");
            });
            stringBuilder.Append(
                $"((st_distance (point (epointTable.{param.Point.Key}, epointTable.{param.Point.Value}),point({param.Location.Key},{param.Location.Value}) ) / 0.0111)*1000) AS '{param.DistanceAlias}'");
            stringBuilder.Append($" FROM {param.TableName} as {tableAlias}");
            stringBuilder.Append($" HAVING {param.DistanceAlias}>{param.MinDistance}");
            if (param.Distance != -1)
            {
                stringBuilder.Append($" And {param.DistanceAlias}<{param.Distance}");
            }

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

            return sql;
        }

        /// <summary>
        /// Get sql by Latitude and longitude
        /// </summary>
        /// <param name="param"></param>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        private string GetDataSql(SpatialDimensionParam param, int pageIndex, int pageSize)
        {
            string sql = GetDataSql(param);

            if (pageSize != -1)
            {
                sql += $" limit {(pageIndex - 1) * pageSize},{pageSize}";
            }

            return sql;
        }

        #endregion

        #region get total data

        /// <summary>
        /// get total data
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetCountSql(SpatialDimensionParam param)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"select Count(temp.{param.DistanceAlias}) from (");
            stringBuilder.Append($" {GetDataSql(param)}");
            stringBuilder.Append(" )as temp");
            return stringBuilder.ToString();
        }

        #endregion

        #endregion
    }
}
