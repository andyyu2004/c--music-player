using System;
using System.Diagnostics;
using System.Security.Cryptography;
using CMusicPlayer.Internal.Interfaces;
using TagLib;

namespace CMusicPlayer.Media.Models
{
    internal class TrackModel : ITrack
    {
        public string? Artist { get; set; }
        public long? ArtistId { get; set; }
        public string? Album { get; set; }
        public long? AlbumId { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public long? TrackId { get; set; }
        public string? Path { get; set; }
        public string? Encoding { get; set; }
        public string? Filename { get; set; }
        public int SampleRate { get; set; }
        public int BitRate { get; set; }
        public int Duration { get; set; }
        public int? BitDepth { get; set; }
        public uint? TrackNumber { get; set; }
        public string? Lyrics { get; set; }
        public uint? Year { get; set; }

        public IShallowCopyable ShallowCopy()
        {
            return (IShallowCopyable) MemberwiseClone();
        }

        /// <summary>
        ///     Create Track Object From TagLib.File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static TrackModel FromFile(File file)
        {
            // Using both libraries to get tags as some fields only work for one of the libraries
            var t = file.Tag;
            var p = file.Properties;
//            var track = new Track(file.Name);
            return new TrackModel
            {
                Artist = t.FirstPerformer,
                Album = t.Album,
                Title = t.Title,
                Genre = t.FirstGenre,
                TrackId = HashTrack(file.Name, p.AudioBitrate, p.Duration),
                Path = file.Name,
                Encoding = System.IO.Path.GetExtension(file.Name),
                Filename = file.Name,
                SampleRate = p.AudioSampleRate,
                BitRate = p.AudioBitrate,
                Duration = (int) p.Duration.TotalSeconds,
                BitDepth = p.BitsPerSample,
                TrackNumber = t.Track,
                Lyrics = t.Lyrics,
                Year = t.Year,
            };
        }

        private static long HashTrack(string path, int bitrate, TimeSpan duration)
        {
            using var hash = SHA256.Create();
            var bytes = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes($"{path}:{bitrate}:{duration.TotalMilliseconds}"));
            return BitConverter.ToInt64(bytes);
        }

        public override string ToString() => $"{Artist} -> {Album} -> {Title}";

        // ATL api
//        public static TrackModel FromFile(string filename)
//        {
//            var t = new Track(filename);
////            Debug.WriteLine(t.Album);
////            Debug.WriteLine(t.Artist);
////            Debug.WriteLine(t.Title);
////            Debug.WriteLine(t.TrackNumber);
////            Debug.WriteLine(t.SampleRate);
////            Debug.WriteLine(t.Bitrate);
//            using var mp3 = new Mp3(filename);
//            Debug.WriteLine(mp3.GetTag(Id3TagFamily.Version2X).Lyrics);
//
//
//            return new TrackModel
//            {
//                Artist = t.Artist,
//                Album = t.Album,
//                Title = t.Title,
//                Genre = t.Genre,
//                TrackId = 0,
//                Path = t.Path,
//                Encoding = System.IO.Path.GetExtension(filename),
//                Filename = filename, 
//                SampleRate = (int) t.SampleRate,
//                BitRate = t.Bitrate,
//                Duration = t.Duration,
////                BitDepth = t.
//                TrackNumber = (uint?) t.TrackNumber,
////                Lyrics = t.Lyrics,
//                Year = (uint?) t.Year,
//            };
//        }
    }
}