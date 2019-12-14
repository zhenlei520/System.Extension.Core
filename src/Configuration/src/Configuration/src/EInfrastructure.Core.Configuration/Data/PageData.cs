// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
#if NETSTANDARD

#endif
using Newtonsoft.Json;

namespace EInfrastructure.Core.Configuration.Data
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
        ///
        /// </summary>
        /// <param name="rowCount">总条数</param>
        /// <param name="data">数据集合</param>
        /// <param name="extendedData">扩展信息</param>
        public PageData(int rowCount, List<T> data, object extendedData)
        {
            this.Data = data;
            this.RowCount = rowCount;
            this.ExtendedData = extendedData;
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
        public ICollection<T> Data { get; set; }

        /// <summary>
        /// 扩展Data
        /// </summary>
        [JsonProperty(PropertyName = "extend_data")]
        [JsonIgnore]
        public virtual object ExtendedData { get; set; }
    }
}
