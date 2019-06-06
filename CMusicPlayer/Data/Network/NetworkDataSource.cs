using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using CMusicPlayer.Data.Network.Types;
using CMusicPlayer.Data.Network.Types.Exceptions;
using CMusicPlayer.Internal.Types.Functional;
using Newtonsoft.Json;

namespace CMusicPlayer.Data.Network
{
    internal class NetworkDataSource
    {
        private readonly IHttpService httpService;

        public NetworkDataSource(IHttpService httpService) => this.httpService = httpService;

        /// <summary>
        /// Checks For Errors In Response And Returns <c>Either Exception / string</c>
        /// </summary>
        /// <param name="res"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Try<string> ProcessResponse(Try<string> res, string message)
            => res.Match<Try<string>>(e =>
            {
                switch (e)
                {
                    case HttpRequestException _:
                        return new Exception("Failed To Connect To API");
                    case HttpException _:
                        return new Exception(message);
                    default:
                        return new Exception("Unknown Error Occurred");
                }
            }, str => str);


        /// <summary>
        /// Retrieves user salt from server
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<Try<string>> GetSalt(string email)
        {
            var res = await httpService.GetAsync($"api/salt/{email}");
            return ProcessResponse(res, "Email does not exist");
        }

        /// <summary>
        /// Posts User object to server to attempt authentication
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Try<string>> PostLoginWithCredentials(User user)
        {
            var userStr = JsonConvert.SerializeObject(user);
            var res = await httpService.PostAsync("api/signin", userStr);
            return ProcessResponse(res, "Incorrect Password");
        }

        public async Task<Try<string>> GetTracks()
        {
            Debug.WriteLine("Fetching Tracks");
            var tracks = await httpService.GetAsync(
                $"api/protected/music/tracks");
            return ProcessResponse(tracks, "Failed To Get Tracks");
        }

        public async Task<Try<string>> GetTracksByAlbum(long albumid)
        {
            var tracks = await httpService.GetAsync(
                $"api/protected/music/albums/{albumid}");
            return ProcessResponse(tracks, "Failed To Get Tracks from Album");
        }

        public async Task<Try<string>> GetAlbums()
        {
            var albums =
                await httpService.GetAsync(
                    $"api/protected/music/albums");
            return ProcessResponse(albums, "Failed To Get Albums");
        }

        public async Task<Try<string>> GetArtists()
        {
            var artists =
                await httpService.GetAsync(
                    $"api/protected/music/artists");
            return ProcessResponse(artists, "Failed To Get Artists");
        }

        public async Task<Try<string>> GetAlbumsByArtist(long artistid)
        {
            var albums = await httpService.GetAsync(
                $"api/protected/music/artists/{artistid}");
            return ProcessResponse(albums, "Failed To Get Albums By Artist");
        }
    }
}
