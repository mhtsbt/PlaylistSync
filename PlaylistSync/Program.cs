using ITunesLibraryParser;
using Microsoft.Extensions.DependencyInjection;
using PlaylistsNET.Content;
using PlaylistsNET.Models;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlaylistSync
{
    class Program
    {
        static void Main(string[] args)
        {

            string iTunesXml = @"C:\Users\matth\Music\iTunes\iTunes Music Library.xml";

            var library = new ITunesLibrary(iTunesXml);

            var tracks = library.Tracks;
            // returns all tracks in the iTunes Library

            var albums = library.Albums;
            // returns all albums in the iTunes Library

            var playlists = library.Playlists;

            var favTracks = playlists.FirstOrDefault(x => x.Name == "Favorites").Tracks;

            // returns all playlists in the iTunes Library


            Console.WriteLine("Hello World!");

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();


            M3uPlaylist playlist = new M3uPlaylist();
            playlist.IsExtended = true;

            foreach (var track in favTracks)
            {

                playlist.PlaylistEntries.Add(new M3uPlaylistEntry()
                {
                    Album = track.Album,
                    AlbumArtist = track.Artist,                    
                    Path = track.Location,
                    Title = track.Name
                });

            }

            M3uContent content = new M3uContent();
            string text = content.ToText(playlist);

            File.WriteAllText("test.m3u", text);

        }

    }
}
