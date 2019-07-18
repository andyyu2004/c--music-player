namespace CMusicPlayer.Media.Models
{
    public class ArtistModel : IArtist
    {
        // Required for dapper
        public ArtistModel(string? artist)
        {
            Artist = artist;
        }

        public ArtistModel(long? id)
        {
            Id = id;
        }

        public string? Artist { get; set; }
        public long? Id { get; set; }

        public override string ToString()
        {
            return $"Artist: {Artist}";
        }
    }
}