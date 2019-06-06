using CMusicPlayer.Media.Models;
using Newtonsoft.Json;

namespace CMusicPlayer.Data.Network.Types
{
    internal class ArtistResponse : IArtist
    {
        [JsonProperty("artist")]
        public string? Artist { get; set; }

        [JsonProperty("artistid")]
        public long? Id { get; set; }
    }
}
