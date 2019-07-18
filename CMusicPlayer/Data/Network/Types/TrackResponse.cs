using System;
using CMusicPlayer.Configuration;
using CMusicPlayer.Internal.Interfaces;
using CMusicPlayer.Media.Models;
using Newtonsoft.Json;

namespace CMusicPlayer.Data.Network.Types
{
    internal class TrackResponse : ITrack
    {
        public TrackResponse()
        {
        }

//        private TrackResponse(ITrack t)
//        {
//            Artist = t.Artist;
//            ArtistId = t.ArtistId;
//            Album = t.Album;
//            ArtistId = t.AlbumId;
//            Title = t.Title;
//            TrackId = t.TrackId;
//            Genre = t.Genre;
//            Filename = t.Filename;
//            SampleRate = t.SampleRate;
//            BitRate = t.BitRate;
//            Duration = t.Duration;
//            Encoding = t.Encoding;
//            BitDepth = t.BitDepth;
//            TrackNumber = t.TrackNumber;
//            Lyrics = t.Lyrics;
//            Year = t.Year;
//        }

        // Shouldn't really happen so throw exception 
        private static string ApiEndpoint => Config.Settings[Config.Authentication][Config.ApiEndpoint] ??
                                             throw new Exception("Api Exception was null when required");

        private static string JwtToken => Config.Settings[Config.Authentication][Config.JwtToken] ??
                                          throw new Exception("JwtToken was null when required");

        [JsonProperty("artist")] public string? Artist { get; set; }

        [JsonProperty("artistid")] public long? ArtistId { get; set; }

        [JsonProperty("album")] public string? Album { get; set; }

        [JsonProperty("albumid")] public long? AlbumId { get; set; }

        [JsonProperty("title")] public string? Title { get; set; }

        [JsonProperty("trackid")] public long? TrackId { get; set; }

        [JsonProperty("genre")] public string? Genre { get; set; }

        [JsonProperty("filename")] public string? Filename { get; set; }

        [JsonProperty("samplerate")] public int SampleRate { get; set; }

        [JsonProperty("bitrate")] public int BitRate { get; set; }

        [JsonProperty("duration")] public int Duration { get; set; }

        [JsonProperty("encoding")] public string? Encoding { get; set; }

        [JsonProperty("bitdepth")] public int? BitDepth { get; set; }

        [JsonProperty("tracknumber")] public uint? TrackNumber { get; set; }

        [JsonProperty("lyrics")] public string? Lyrics { get; set; }

        [JsonProperty("year")] public uint? Year { get; set; }

        public string? Path => $"{ApiEndpoint}/api/protected/music/tracks/{Encoding}/{TrackId}?jwt_token={JwtToken}";

        public IShallowCopyable ShallowCopy()
        {
            return (IShallowCopyable) MemberwiseClone();
        }
    }
}