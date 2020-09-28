// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.Entities.Configuration;
using EInfrastructure.Core.Config.Entities.Data;

namespace EInfrastructure.Core.Config.Entities.Ioc
{
    /// <summary>
    /// 空间维度查询
    /// </summary>
    public interface ISpatialDimensionQuery<TEntity, T> where TEntity : IEntity<T> where T : IComparable
    {
        /// <summary>
        /// get list
        /// </summary>
        /// <typeparam name="TOpt"></typeparam>
        /// <returns></returns>
        List<TOpt> GetList<TOpt>(SpatialDimensionParam param);

        /// <summary>
        /// get list
        /// </summary>
        /// <param name="param"></param>
        /// <typeparam name="TOpt"></typeparam>
        /// <returns></returns>
        PageData<TOpt> GetPageData<TOpt>(SpatialDimensionPagingParam param);

        /// <summary>
        /// get list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable(SpatialDimensionParam param);
    }

    /// <summary>
    /// 空间维度查询（支持多数据库）
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public interface ISpatialDimensionQuery<TEntity, T, TDbContext>
        : ISpatialDimensionQuery<TEntity, T>
        where TEntity : IEntity<T>
        where T : IComparable
        where TDbContext : IDbContext
    {
    }

    /// <summary>
    /// Spatial param
    /// </summary>
    public class SpatialDimensionParam
    {
        /// <summary>
        /// Table name *
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Query property set *
        /// </summary>
        public List<KeyValuePair<string, string>> FileKeys { get; set; }

        /// <summary>
        /// Longtitude And Latitude *
        /// Key:Longtitude
        /// Value:Latitude
        /// </summary>
        public KeyValuePair<string, string> Point { get; set; }

        /// <summary>
        /// The sorting
        /// ascending:false
        /// descending:true
        /// </summary>
        public List<KeyValuePair<string, bool>> Sorts { get; set; }

        /// <summary>
        /// The current position
        /// Longtitude And Latitude *
        /// Key:Longtitude
        /// Value:Latitude
        /// </summary>
        public KeyValuePair<decimal, decimal> Location { get; set; }

        /// <summary>
        /// The DistanceAlias cannot be empty
        /// </summary>
        public string DistanceAlias { get; set; } = "distance";

        /// <summary>
        /// max distance,unit：m
        /// the default 500 m,
        /// </summary>
        public decimal Distance { get; set; } = 500;

        /// <summary>
        /// min distance,unit：m
        /// the default 0 m
        /// </summary>
        public decimal MinDistance { get; set; } = 0;
    }

    /// <summary>
    ///
    /// </summary>
    public class SpatialDimensionPagingParam : SpatialDimensionParam
    {
        /// <summary>
        /// page index
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// page size
        /// The default unlimited:-1
        /// </summary>
        public int PageSize { get; set; } = -1;

        /// <summary>
        /// Do I need the total
        /// </summary>
        public bool IsTotal { get; set; } = true;
    }
}
