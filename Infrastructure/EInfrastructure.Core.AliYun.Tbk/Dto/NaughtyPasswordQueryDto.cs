using Newtonsoft.Json;

namespace EInfrastructure.Core.AliYun.Tbk.Dto
{
    /// <summary>
    /// 淘口令解析
    /// </summary>
    public class NaughtyPasswordQueryDto
    {
        /// <summary>
        /// 淘口令-文案
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }
        
        /// <summary>
        /// 淘口令-宝贝
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        
        /// <summary>
        /// 如果是宝贝，则为宝贝价格
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public string Price { get; set; }
        
        /// <summary>
        /// 图片url
        /// </summary>
        [JsonProperty(PropertyName = "pic_url")]
        public string PicUrl { get; set; }
        
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty(PropertyName = "suc")]
        public string Suc { get; set; }
        
        /// <summary>
        /// 跳转url(长链)
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        
        /// <summary>
        /// nativeUrl
        /// </summary>
        [JsonProperty(PropertyName = "native_url")]
        public string NativeUrl { get; set; }
        
        /// <summary>
        /// thumbPicUrl
        /// </summary>
        [JsonProperty(PropertyName = "thumb_pic_url")]
        public string ThumbPicUrl { get; set; }
    }
}