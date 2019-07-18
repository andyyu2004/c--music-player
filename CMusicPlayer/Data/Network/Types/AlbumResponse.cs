using CMusicPlayer.Media.Models;
using Newtonsoft.Json;

namespace CMusicPlayer.Data.Network.Types
{
    internal class AlbumResponse : IAlbum
    {
        // All require setters for newtonsoft

        [JsonProperty("albumid")] public long? Id { get; set; }

        [JsonProperty("artistid")] public long? ArtistId { get; set; }

        [JsonProperty("album")] public string? Album { get; set; }

        [JsonProperty("artist")] public string? Artist { get; set; }

        [JsonProperty("genre")] public string? Genre { get; set; }

        [JsonProperty("year")] public uint? Year { get; set; }

        public override string ToString()
        {
            return $"{Artist} -> {Album} -> {Id}";
        }
    }
}