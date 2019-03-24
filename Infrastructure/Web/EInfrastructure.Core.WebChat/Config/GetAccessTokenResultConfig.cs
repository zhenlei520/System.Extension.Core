using Newtonsoft.Json;

namespace EInfrastructure.Core.WebChat.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAccessTokenResultConfig
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "openid")]
        public string Openid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "unionid")]
        public string Unionid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "errcode")]
        public string Errcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "errmsg")]
        public string Errmsg { get; set; }
    }
}