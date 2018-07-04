using Newtonsoft.Json;

namespace EInfrastructure.Core.WebChat.Config
{
    public class GetAccessTokenResultConfig
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
        
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }
        
        [JsonProperty(PropertyName = "openid")]
        public string Openid { get; set; }
        
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }
        
        [JsonProperty(PropertyName = "unionid")]
        public string Unionid { get; set; }
        
        [JsonProperty(PropertyName = "errcode")]
        public string Errcode { get; set; }
        
        [JsonProperty(PropertyName = "errmsg")]
        public string Errmsg { get; set; }
    }
}