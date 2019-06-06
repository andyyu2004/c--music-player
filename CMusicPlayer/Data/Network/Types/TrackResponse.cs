using System;
using CMusicPlayer.Configuration;
using CMusicPlayer.Media.Models;
using CMusicPlayer.Util;
using Newtonsoft.Json;

namespace CMusicPlayer.Data.Network.Types
{
    internal class TrackResponse : ITrack
    {
        [JsonProperty("artist")]
        public string? Artist { get; set; }

        [JsonProperty("artistid")]
        public long? ArtistId { get; set; }

        [JsonProperty("album")]
        public string? Album { get; set; }

        [JsonProperty("albumid")]
        public long? AlbumId { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("trackid")]
        public long? TrackId { get; set; }

        [JsonProperty("genre")]
        public string? Genre { get; set; }

        [JsonProperty("filename")]
        public string? Filename { get; set; }

        [JsonProperty("samplerate")]
        public int SampleRate { get; set; }

        [JsonProperty("bitrate")]
        public int BitRate { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("encoding")]
        public string? Encoding { get; set; }

        [JsonProperty("bitdepth")]
        public int? BitDepth { get; set; }

        [JsonProperty("tracknumber")]
        public uint? TrackNumber { get; set; }

        [JsonProperty("lyrics")]
        public string? Lyrics { get; set; }

        [JsonProperty("year")]
        public uint? Year { get; set; }

        // Shouldn't really happen so throw exception 
        private static string ApiEndpoint => Config.Settings[Constants.Authentication][Constants.ApiEndpoint] ?? throw new Exception("Api Exception was null when required");
        private static string JwtToken => Config.Settings[Constants.Authentication][Constants.JwtToken] ?? throw new Exception("JwtToken was null when required");
        public string? Path => $"{ApiEndpoint}/api/protected/music/tracks/{Encoding}/{TrackId}?jwt_token={JwtToken}";
    }

}
