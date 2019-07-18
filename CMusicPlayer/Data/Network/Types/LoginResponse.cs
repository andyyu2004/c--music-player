using Newtonsoft.Json;

namespace CMusicPlayer.Data.Network.ResponseTypes
{
    internal class LoginResponse
    {
        [JsonProperty("message")] public string Message { get; set; } = "";

        [JsonProperty("user")] public string User { get; set; } = "";

        [JsonProperty("token")] public string Token { get; set; } = "";

        [JsonProperty("userid")] public string UserId { get; set; } = "";
    }
}