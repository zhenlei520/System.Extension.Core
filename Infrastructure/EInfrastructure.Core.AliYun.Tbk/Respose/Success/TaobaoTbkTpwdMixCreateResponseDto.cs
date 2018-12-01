using Newtonsoft.Json;

namespace EInfrastructure.Core.AliYun.Tbk.Respose.Success
{
    /// <summary>
    /// 淘宝客文本淘口令
    /// </summary>
    public class TaobaoTbkTpwdMixCreateResponseDto
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "tbk_tpwd_mix_create_response")]
        public TaobaoTbkTpwdMixCreateResponse TaobaoTbkTpwdMixCreate { get; set; }
        
        /// <summary>
        /// 响应信息
        /// </summary>
        public class TaobaoTbkTpwdMixCreateResponse
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty(PropertyName = "data")]
            public DataDto Data { get; set; }
        }

        /// <summary>
        /// 响应信息
        /// </summary>
        public class DataDto
        {
            /// <summary>
            /// 状态码
            /// </summary>
            [JsonProperty(PropertyName = "status")]
            public string Status { get; set; }

            /// <summary>
            /// 自定义口令
            /// </summary>
            [JsonProperty(PropertyName = "password")]
            public string Password { get; set; }
        }
    }
}