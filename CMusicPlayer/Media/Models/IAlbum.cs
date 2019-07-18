namespace CMusicPlayer.Media.Models
{
    public interface IAlbum
    {
        long? Id { get; }
        long? ArtistId { get; }
        string? Album { get; set; }
        string? Artist { get; set; }
        string? Genre { get; set; }
        uint? Year { get; set; }
    }
}