using Newtonsoft.Json;

namespace CMusicPlayer.Data.Network.Types
{
    internal class User
    {
        [JsonProperty("email")] public string Email { get; set; } = "";

        [JsonProperty("password")] public string Password { get; set; } = "";
    }
}