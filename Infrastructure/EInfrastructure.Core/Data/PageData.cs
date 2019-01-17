using System.Collections.Generic;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Data
{
    /// <summary>
    /// 分页数据集合
    /// </summary>
    public class PageData<T>
    {
        public PageData()
        {
            Data = new List<T>();
        }

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
        public List<T> Data { get; set; }

        /// <summary>
        /// 扩展Data
        /// </summary>
        [JsonProperty(PropertyName = "extend_data")]
        [JsonIgnore]
        public virtual object ExtendedData { get; set; }
    }
}