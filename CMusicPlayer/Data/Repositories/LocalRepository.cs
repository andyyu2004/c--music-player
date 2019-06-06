using System.Collections.Generic;
using System.Threading.Tasks;
using CMusicPlayer.Data.Databases;
using CMusicPlayer.Media.Models;

namespace CMusicPlayer.Data.Repositories
{
    internal class LocalRepository : ITrackRepository
    {
        private readonly IDatabase db;

        public LocalRepository(IDatabase db) => this.db = db;

//        public async Task<IEnumerable<T>> GetTracks<T>() where T : ITrack => (IEnumerable<T>) await db.LoadTracks();

        public async Task<IEnumerable<ITrack>> GetTracks() => await db.LoadTracks();
        public async Task<IEnumerable<IAlbum>> GetAlbums() => await db.LoadAlbums();
        public async Task<IEnumerable<IArtist>> GetArtists() => await db.LoadArtists();

        public async Task<IEnumerable<ITrack>> GetTracksByAlbum(IAlbum album) =>
            await db.LoadTracksByAlbum(album);

        public async Task<IEnumerable<IAlbum>> GetAlbumsByArtist(IArtist artist) => await db.LoadAlbumsByArtist(artist);
    }
}
