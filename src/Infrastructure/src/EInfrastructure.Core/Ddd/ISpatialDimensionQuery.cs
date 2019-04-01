// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.Ddd
{
    /// <summary>
    /// 空间维度查询
    /// </summary>
    public interface ISpatialDimensionQuery
    {
        /// <summary>
        /// get list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> GetList<T>(SpatialDimensionParam param);
    }

    /// <summary>
    /// Spatial param
    /// </summary>
    public class SpatialDimensionParam
    {
        /// <summary>
        /// Table name
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Query property set
        /// </summary>
        public List<KeyValuePair<string, string>> FileKeys { get; set; }

        /// <summary>
        /// Longtitude And Latitude
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
        /// Longtitude And Latitude
        /// Key:Longtitude
        /// Value:Latitude
        /// </summary>
        public KeyValuePair<decimal, decimal> Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DistanceAlias { get; set; } = "distance";
        
        /// <summary>
        /// distance unit：m
        /// the default 500 m
        /// </summary>
        public decimal Distance { get; set; } = 500;
    }
}