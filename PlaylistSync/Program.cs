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

            foreach (var playlist in playlists.Where(p => p.Tracks != null && p.Tracks.Count() != 0))
            {

                Console.WriteLine(playlist.Name);

                var serviceCollection = new ServiceCollection();
                serviceCollection.AddLogging();


                var playlistOutput = new M3uPlaylist
                {
                    IsExtended = true
                };

                foreach (var track in playlist.Tracks)
                {

                    if (track.Location != null)
                    {

                        playlistOutput.PlaylistEntries.Add(new M3uPlaylistEntry()
                        {
                            Album = track.Album,
                            AlbumArtist = track.Artist,
                            Path = track.Location.Replace("file://localhost/D:/iTunesMedia/Music", "..").Replace("%20", " "),
                            Title = track.Name
                        });

                    }

                }

                var content = new M3uContent();
                string text = content.ToText(playlistOutput);

                File.WriteAllText(@"\\192.168.1.2\music\playlists\" + playlist.Name + ".m3u", text);

            }
        }

    }
}
