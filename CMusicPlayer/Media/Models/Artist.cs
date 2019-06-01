namespace CMusicPlayer.Media.Models
{
    public class ArtistModel : IArtist
    {

        public string? Artist { get; set; }
        public long? Id { get; set; }

        public ArtistModel()
        {
            
        }
        public ArtistModel(long id)
        {
            Id = id;
        }

        public override string ToString()
            => $"Artist: {Artist}";
    }
}
