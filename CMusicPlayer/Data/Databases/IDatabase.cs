using System.Collections.Generic;
using System.Threading.Tasks;
using CMusicPlayer.Internal.Types.Functional;
using CMusicPlayer.Media.Models;

namespace CMusicPlayer.Data.Databases
{
    internal interface IDatabase
    {
        Task SaveTrack(TrackModel track);
        Task<IEnumerable<TrackModel>> LoadTracks();
        Task<IEnumerable<ArtistModel>> LoadArtists();
        Task<IEnumerable<AlbumModel>> LoadAlbums();
        Task<IEnumerable<AlbumModel>> LoadAlbumsByArtist(IArtist artist);
        Task<IEnumerable<TrackModel>> LoadTracksByAlbum(IAlbum album);
        Task<Try<IEnumerable<TrackModel>>> TryLoadTracks();
        Task<Try<IEnumerable<T>>> ExecuteQueryAsync<T>(string query);
    }
}