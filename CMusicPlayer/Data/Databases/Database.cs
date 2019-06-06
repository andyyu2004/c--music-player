using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Threading.Tasks;
using CMusicPlayer.Internal.Types.Functional;
using CMusicPlayer.Media.Models;
using Dapper;

namespace CMusicPlayer.Data.Databases
{
    internal class Database : IDatabase
    { 
        public Database() => InitializeDatabase();

        private static void InitializeDatabase()
        {
            // Requires Explicit NOT NULL primary key?
            const string command = @"create table if not exists Tracks (
	            trackid bigint PRIMARY KEY NOT NULL,
	            artist varchar(50), 
	            album varchar(50),
	            encoding varchar(10) NOT NULL,
	            title varchar(256),
	            filename varchar(256),
                path varchar(256),
	            samplerate mediumint unsigned,
	            bitrate mediumint unsigned,
	            bitdepth tinyint unsigned,
	            duration smallint unsigned,
	            length smallint unsigned,
	            genre varchar(50),
	            trackNumber tinyint unsigned,
	            lyrics text,
                year smallint unsigned,
	            comments varchar(256)
            )";
            using var db = new SQLiteConnection(ConnectionString);
            db.Execute(command);
        }

        public async Task SaveTrack(TrackModel track)
        {
            using var db = new SQLiteConnection(ConnectionString);
            try
            {
                await db.ExecuteAsync(@"REPLACE Into Tracks
                    (trackid, artist, album, title, encoding, filename, path, duration, bitrate, samplerate, lyrics, genre, year)
                    values 
                    (@TrackId, @Artist, @Album, @Title, @Encoding, @Filename, @Path, @Duration, @Bitrate, @SampleRate, @Lyrics, @Genre, @Year)", track);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message}");
            }
        }

        public Task<IEnumerable<TrackModel>> LoadTracks()
        {
            using var db = new SQLiteConnection(ConnectionString);
            return db.QueryAsync<TrackModel>("select * from Tracks");
        }

        public Task<IEnumerable<ArtistModel>> LoadArtists()
        {
            using var db = new SQLiteConnection(ConnectionString);
            return db.QueryAsync<ArtistModel>("SELECT DISTINCT artist FROM Tracks ORDER BY artist");
        }

        public Task<IEnumerable<AlbumModel>> LoadAlbums()
        {
            using var db = new SQLiteConnection(ConnectionString);
            return db.QueryAsync<AlbumModel>("SELECT DISTINCT artist, album, genre, year FROM Tracks ORDER BY album");
        }

        public Task<IEnumerable<AlbumModel>> LoadAlbumsByArtist(IArtist artist)
        {
            using var db = new SQLiteConnection(ConnectionString);
            return db.QueryAsync<AlbumModel>(@"SELECT DISTINCT artist, album, genre, year FROM Tracks WHERE artist = @Artist ORDER BY album", artist);
        }

        public Task<IEnumerable<TrackModel>> LoadTracksByAlbum(IAlbum album)
        {
            using var db = new SQLiteConnection(ConnectionString);
            return db.QueryAsync<TrackModel>(@"SELECT DISTINCT 
                    artist, album, title, encoding, filename, path, duration, bitrate, lyrics, genre
                    FROM Tracks WHERE artist=@Artist AND album = @Album AND year = @Year", album);
        }

        public async Task<Try<IEnumerable<TrackModel>>> TryLoadTracks()
        {
            using var db = new SQLiteConnection(ConnectionString);
            try
            {
                return Try<IEnumerable<TrackModel>>.Just(await db.QueryAsync<TrackModel>("Select * From Tracks"));
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public async Task<Try<IEnumerable<T>>> ExecuteQueryAsync<T>(string query)
        {
            using var db = new SQLiteConnection(ConnectionString);
            try
            {
                Console.WriteLine($@"QUERY {query}");
                return Try<IEnumerable<T>>.Just(await db.QueryAsync<T>(query));
            }
            catch (Exception e)
            {
                return e;
            }
        }

        private static string ConnectionString => "Data Source=musicdb.sqlite";
    }
}
