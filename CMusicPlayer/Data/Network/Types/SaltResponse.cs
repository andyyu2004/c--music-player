using Newtonsoft.Json;

namespace CMusicPlayer.Data.Network.ResponseTypes
{
    internal class SaltResponse
    {
        [JsonProperty("salt")] public string Salt { get; set; } = "";
    }
}
