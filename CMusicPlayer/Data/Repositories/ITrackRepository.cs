using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMusicPlayer.Media.Models;

namespace CMusicPlayer.Data.Repositories
{
    internal interface ITrackRepository
    {
        Task<IEnumerable<ITrack>> GetTracks();
        Task<IEnumerable<IAlbum>> GetAlbums();
        Task<IEnumerable<IArtist>> GetArtists();
        Task<IEnumerable<ITrack>> GetTracksByAlbum(IAlbum album);
        Task<IEnumerable<IAlbum>> GetAlbumsByArtist(IArtist artist);
    }

}
