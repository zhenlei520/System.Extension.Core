// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Config.Entities.Data
{
    /// <summary>
    /// 分页数据集合
    /// </summary>
    public class PageData<T>
    {
        /// <summary>
        ///
        /// </summary>
        public PageData()
        {
            Data = new List<T>();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="rowCount">总条数</param>
        /// <param name="data">数据集合</param>
        public PageData(int rowCount, List<T> data)
        {
            RowCount = rowCount;
            Data = data;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="data"></param>
        /// <param name="extendedData"></param>
        public PageData(int rowCount, List<T> data, object extendedData)
        {
            RowCount = rowCount;
            Data = data;
            ExtendedData = extendedData;
        }

        /// <summary>
        /// 总页数
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public virtual int RowCount { get; set; }

        /// <summary>
        /// 当前页数据集合
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public virtual ICollection<T> Data { get; set; }

        /// <summary>
        /// 扩展Data
        /// </summary>
        [JsonProperty(PropertyName = "extend_data")]
        public virtual object ExtendedData { get; set; }
    }
}
