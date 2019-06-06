﻿namespace CMusicPlayer.Media.Models
{
    internal class AlbumModel : IAlbum
    {
        // Not really in use for local tracks currently
        public long? Id { get; }
        public long? ArtistId { get; set; }
        public string? Album { get; set; }
        public string? Artist { get; set; }
        public string? Genre { get; set; }
        public uint? Year { get; set; }

        // Required for dapper
        public AlbumModel()
        {
            
        }

        public AlbumModel(long? id)
        {
            Id = id;
        }

        public override string ToString()
            => $"Album: {Album} - {Artist}";
    }
}
