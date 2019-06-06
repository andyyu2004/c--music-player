using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMusicPlayer.Data.Repositories;
using CMusicPlayer.Media.Models;
using CMusicPlayer.Media.Playback;

namespace CMusicPlayer.UI.Music.ViewModelBases
{
    /**
     * Viewmodel base for track views(cloud and local)
     */
    internal abstract class TracksViewModel : MusicViewModel
    {
        protected ITrackRepository Repository { get; }
        public abstract string LibraryName { get; }

        protected TracksViewModel(IMediaPlayerController mediaController, ITrackRepository repository) : base(mediaController) => Repository = repository;

        public void PlayTrackNext(ITrack track) => Mp.PlayTrackNext(track);
        public void AddTrackToQueue(ITrack track) => Mp.AddTrackToQueue(track);

        public async void ShuffleAll(object sender, EventArgs e)
        {
            Mp.SetTrackList(this, (await GetTracks()).ToList());
            Mp.ShuffleAll();
        }

        public async Task<IEnumerable<ITrack>> GetTracks() => await Repository.GetTracks();

        public async Task<IEnumerable<IArtist>> GetArtists() => await Repository.GetArtists();

        public async Task<IEnumerable<IAlbum>> GetAlbums() => await Repository.GetAlbums();

        public async Task<IEnumerable<ITrack>> GetTracksByAlbum(IAlbum album) => await Repository.GetTracksByAlbum(album);

        public async Task<IEnumerable<IAlbum>> GetAlbumsByArtist(IArtist artist) =>
            await Repository.GetAlbumsByArtist(artist);
    }
}
