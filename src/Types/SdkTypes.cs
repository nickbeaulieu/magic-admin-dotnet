using Newtonsoft.Json;

namespace Magic
{
    public class MagicAdminSDKAdditionalConfiguration
    {
        public string Endpoint { get; set; }
    }

    public class MagicUserMetadata 
    {
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("public_address")]
        public string PublicAddress { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}