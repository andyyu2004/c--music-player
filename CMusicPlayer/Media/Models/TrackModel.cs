using System.Diagnostics;
using ATL;

namespace CMusicPlayer.Media.Models
{

    public class TrackModel : ITrack
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


        /// <summary>
        /// Create Track Object From TagLib.File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static TrackModel FromFile(TagLib.File file)
        {
            var t = file.Tag;
            var p = file.Properties;
            return new TrackModel
            {
                Artist = t.FirstPerformer,
                Album = t.Album,
                Title = t.Title,
                Genre = t.FirstGenre,
                TrackId = file.Name.GetHashCode(),
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

        public override string ToString() =>
            $"{Artist} -> {Album} -> {Title}";

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
