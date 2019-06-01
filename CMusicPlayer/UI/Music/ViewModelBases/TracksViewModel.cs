using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CMusicPlayer.Data.Repositories;
using CMusicPlayer.Media.Models;
using CMusicPlayer.Media.Playback;
using CMusicPlayer.Util.Extensions;
using JetBrains.Annotations;

namespace CMusicPlayer.UI.Music.ViewModelBases
{
    /**
     * Viewmodel base for track views(cloud and local)
     */
    internal abstract class TracksViewModel : MusicViewModel
    {
        protected IRepository Repository { get; }
        public string LibraryName { get; } = "";

        protected TracksViewModel(IMediaPlayerController mediaController, IRepository repository) : base(mediaController)
        {
            Repository = repository;
        }

        public void PlayTrackNext(ITrack track) => PlayerController.PlayTrackNext(track);
        public void AddTrackToQueue(ITrack track) => PlayerController.AddTrackToQueue(track);

        public async void ShuffleAll(object sender, EventArgs e)
        {
            PlayerController.SetTrackList(this, (await GetTracks()).ToList());
            PlayerController.ShuffleAll();
        }

        public async Task<IEnumerable<ITrack>> GetTracks() => await Repository.GetTracks();

        public async Task<IEnumerable<IArtist>> GetArtists() => await Repository.GetArtists();

        public async Task<IEnumerable<IAlbum>> GetAlbums() => await Repository.GetAlbums();

        public async Task<IEnumerable<ITrack>> GetTracksByAlbum(IAlbum album) => await Repository.GetTracksByAlbum(album);

        public async Task<IEnumerable<IAlbum>> GetAlbumsByArtist(IArtist artist) =>
            await Repository.GetAlbumsByArtist(artist);
    }
}
