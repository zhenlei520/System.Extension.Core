using Newtonsoft.Json;

namespace EInfrastructure.Core.WebChat.Config
{
    public class JsSdkConfig
    {
        [JsonProperty("appId")] public string AppId { get; set; }

        [JsonProperty("timestamp")] public long TimeStamp { get; set; }

        [JsonProperty("nonceStr")] public string NonceStr { get; set; }

        [JsonProperty("signature")] public string Signature { get; set; }
    }
}