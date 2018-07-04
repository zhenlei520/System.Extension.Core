using System.Collections.Generic;
using Newtonsoft.Json;

namespace EInfrastructure.Core.WebChat.Config
{
    public class WxUserInfo
    {
        /// <summary>
        /// OpenId
        /// </summary>
        [JsonProperty(PropertyName = "openid")]
        public string Openid { get; set; }
        
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }
        
        [JsonProperty(PropertyName = "sex")]
        public int Sex { get; set; }
        
        [JsonProperty(PropertyName = "province")]
        public string Province { get; set; }
        
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        
        [JsonProperty(PropertyName = "headimgurl")]
        public string Headimgurl { get; set; }
        
        [JsonProperty(PropertyName = "privilege")]
        public List<string> Privilege { get; set; }
        
        [JsonProperty(PropertyName = "unionid")]
        public string Unionid { get; set; }

        [JsonProperty(PropertyName = "errcode")]
        public string Errcode { get; set; }
        
        [JsonProperty(PropertyName = "openid")]
        public string Errmsg { get; set; }
    }
}