using ITunesLibraryParser;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlaylistSync
{
    class Program
    {
        static async Task Main(string[] args)
        {

            string iTunesXml = @"C:\Users\matth\Music\iTunes\iTunes Music Library.xml";

            var library = new ITunesLibrary(iTunesXml);

            var tracks = library.Tracks;
            // returns all tracks in the iTunes Library

            var albums = library.Albums;
            // returns all albums in the iTunes Library

            var playlists = library.Playlists;
            // returns all playlists in the iTunes Library

            var client = new HttpClient();
       

            Console.WriteLine("Hello World!");

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();

        }

    }
}
