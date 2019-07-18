using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMusicPlayer.Data.Network;
using CMusicPlayer.Data.Network.Types;
using CMusicPlayer.Internal.Types.Functional;
using CMusicPlayer.Media.Models;
using Newtonsoft.Json;

namespace CMusicPlayer.Data.Repositories
{
    internal class CloudRepository : ITrackRepository
    {
        private readonly NetworkDataSource source;

        public CloudRepository(NetworkDataSource source)
        {
            this.source = source;
        }

        public async Task<IEnumerable<ITrack>> GetTracks()
        {
            return (await source.GetTracks())
                .Bind(TryDeserialize<IEnumerable<TrackResponse>>)
                .Match(err => Enumerable.Empty<ITrack>(), t => t);
        }

        public async Task<IEnumerable<IAlbum>> GetAlbums()
        {
            return (await source.GetAlbums())
                .Bind(TryDeserialize<IEnumerable<AlbumResponse>>)
                .Match(err => Enumerable.Empty<IAlbum>(), x => x);
        }


        public async Task<IEnumerable<IArtist>> GetArtists()
        {
            return (await source.GetArtists())
                .Bind(TryDeserialize<IEnumerable<ArtistResponse>>)
                .Match(err => Enumerable.Empty<IArtist>(), x => x);
        }

        public async Task<IEnumerable<ITrack>> GetTracksByAlbum(IAlbum album)
        {
            return album.Id.HasValue
                ? (await source.GetTracksByAlbum(album.Id.Value))
                .Bind(TryDeserialize<IEnumerable<TrackResponse>>)
                .Match(err => Enumerable.Empty<ITrack>(), x => x)
                : Enumerable.Empty<ITrack>();
        }

        public async Task<IEnumerable<IAlbum>> GetAlbumsByArtist(IArtist artist)
        {
            return artist.Id.HasValue
                ? (await source.GetAlbumsByArtist(artist.Id.Value))
                .Bind(TryDeserialize<IEnumerable<AlbumResponse>>)
                .Match(err => Enumerable.Empty<IAlbum>(), x => x)
                : Enumerable.Empty<IAlbum>();
        }

        // Requires this intermediary step for implicit conversion T -> Try<T>. JsonConvert cannot deserialize to a Try directly
        private static Try<T> TryDeserialize<T>(string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }
    }
}