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
    internal class Program
    {
        static void Main(string[] args)
        {

            var iTunesXml = @"C:\Users\matth\Music\iTunes\iTunes Music Library.xml";

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

            
            var playlist = new M3uPlaylist
            {
                IsExtended = true
            };

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

            var content = new M3uContent();
            string text = content.ToText(playlist);

            // FORMAT: ../Radiohead/In Rainbows/1-01 15 Step.mp3

            File.WriteAllText("test.m3u", text);

        }

    }
}
