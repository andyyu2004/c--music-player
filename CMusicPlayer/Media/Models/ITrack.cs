namespace CMusicPlayer.Media.Models
{
    public interface ITrack
    {
        string? Artist { get; set; }
        long? ArtistId { get; set; }
        string? Album { get; set; }
        long? AlbumId { get; set; }
        string? Title { get; set; }
        string? Genre { get; set; }
        long? TrackId { get; set; }
        string? Path { get; }
        string? Encoding { get; set; }
        string? Filename { get; set; }
        int SampleRate { get; set; }
        int BitRate { get; set; }
        int Duration { get; set; }
        int? BitDepth { get; set; }
        uint? TrackNumber { get; set; }
        string? Lyrics { get; set; }
        uint? Year { get; set; }
    }
}
