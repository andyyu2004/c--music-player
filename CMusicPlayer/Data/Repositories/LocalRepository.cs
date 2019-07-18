using System.Collections.Generic;
using System.Threading.Tasks;
using CMusicPlayer.Data.Databases;
using CMusicPlayer.Media.Models;

namespace CMusicPlayer.Data.Repositories
{
    internal class LocalRepository : ITrackRepository
    {
        private readonly IDatabase db;

        public LocalRepository(IDatabase db)
        {
            this.db = db;
        }

//        public async Task<IEnumerable<T>> GetTracks<T>() where T : ITrack => (IEnumerable<T>) await db.LoadTracks();

        public async Task<IEnumerable<ITrack>> GetTracks()
        {
            return await db.LoadTracks();
        }

        public async Task<IEnumerable<IAlbum>> GetAlbums()
        {
            return await db.LoadAlbums();
        }

        public async Task<IEnumerable<IArtist>> GetArtists()
        {
            return await db.LoadArtists();
        }

        public async Task<IEnumerable<ITrack>> GetTracksByAlbum(IAlbum album)
        {
            return await db.LoadTracksByAlbum(album);
        }

        public async Task<IEnumerable<IAlbum>> GetAlbumsByArtist(IArtist artist)
        {
            return await db.LoadAlbumsByArtist(artist);
        }
    }
}