using Newtonsoft.Json;

namespace EInfrastructure.Core.AliYun.Tbk.Respose.Success
{
    /// <summary>
    /// 淘宝客淘口令
    /// </summary>
    public class TaobaoTbkTpwdCreateResponseDto
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public DataDto Data { get; set; }

        /// <summary>
        /// data数据
        /// </summary>
        public class DataDto
        {
            /// <summary>
            /// 淘口令
            /// </summary>
            [JsonProperty(PropertyName = "model")]
            public string Model { get; set; }
        }
    }
}