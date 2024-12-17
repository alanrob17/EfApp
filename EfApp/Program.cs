using System;
using System.IO;
using EfApp.Models;
using EfApp.Repositories;
using EfApp.Services;
using EfApp.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EfApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Set up Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddDbContext<AppDbContext>() // Register DbContext
                .AddScoped<IArtistRepository, ArtistRepository>() // Register repository
                .AddScoped<IRecordRepository, RecordRepository>()
                .AddScoped<ArtistService>() // Register ArtistService
                .AddScoped<RecordService>() // Register ArtistService
                .AddScoped<AppLogger>() // Register AppLogger
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders(); // Optional: Clears default providers
                    loggingBuilder.AddConsole();      // Add console logging
                })
                .BuildServiceProvider();

            // Use the service provider to get an instance of IProductRepository
            using (var scope = serviceProvider.CreateScope())
            {
                Artist artist = new();
                var artistService = scope.ServiceProvider.GetRequiredService<ArtistService>();
                var recordService = scope.ServiceProvider.GetRequiredService<RecordService>();
                var appLogger = scope.ServiceProvider.GetRequiredService<AppLogger>();

                //*--- ARTISTS ---*
                //// Create new Artist
                //appLogger.LogInformation("Creating a new artist.");
                //var newArtist = new Artist { FirstName = "Ethan", LastName = "Robson", Name = "Ethan Robson", Biography = "Ethan is an electronics master..." };
                //await artistService.AddArtistAsync(newArtist);

                // Get all Artists
                //var artists = await artistService.GetAllArtistsAsync();
                //foreach (var currentArtist in artists)
                //{
                //    appLogger.LogInformation(currentArtist.ToString());
                //}

                //// Get Artist by Id
                //artist = await artistService.GetArtistByIdAsync(114);
                //if (artist != null)
                //{
                //    appLogger.LogInformation(artist.ToString());

                //    if (artist.Records != null && artist.Records.Any())
                //    {
                //        foreach (var record in artist.Records)
                //        {
                //            appLogger.LogInformation($"Record: {record.ToString()}");
                //        }
                //    }
                //    else
                //    {
                //        appLogger.LogInformation("No records found for this artist.");
                //    }
                //}

                //artist = await artistService.GetArtistByNameAsync("Alonzo Robosono");
                //if (artist != null)
                //{
                //    appLogger.LogInformation(artist.ToString());

                //    var updateArtist = artist;
                //    updateArtist.FirstName = "Alan";
                //    updateArtist.LastName = "Robson";
                //    updateArtist.Name = "Alan Robson";
                //    updateArtist.Biography = "Alan is a Country & Western singer.";
                //    await artistService.UpdateArtistAsync(updateArtist);
                //    appLogger.LogInformation("Artist updated.");
                //}

                //// Delete a product
                //artist = await artistService.GetArtistByNameAsync("Charley Robson");
                //if(artist != null)
                //{
                //    await artistService.DeleteArtistAsync(artist);
                //    appLogger.LogInformation("Artist deleted.");
                //}

                //// Get Artist dropdown list
                //var artistDictionary = new Dictionary<int, string>();

                //artistDictionary = GetArtistDictionaryAsync(artistDictionary, artists);

                //foreach (var item in artistDictionary)
                //{
                //    appLogger.LogInformation($"{item.Key} - {item.Value}");
                //}

                //// Get Artist by Id
                //var artistId = await artistService.GetArtistIdAsync("Bob", "Dylan");
                //appLogger.LogInformation(artistId.ToString());

                //// Get Biography by artistId
                //var biography = await artistService.GetBiographyByIdAsync(114);
                //appLogger.LogInformation($"Biography:\n{biography}");

                //// Get a list of artists with no biography
                //IEnumerable<string> names = await artistService.GetArtistsWithNoBioAsync();
                //foreach (var name in names)
                //{
                //    appLogger.LogInformation(name);
                //}

                //// Get the number of Artists with no biography
                //var total = await artistService.GetNoBiographyTotal();
                //appLogger.LogInformation(total.ToString());

                //*--- RECORDS ---*
                Record record = new();

                //// Get All Records
                //var records = await recordService.GetAllRecordsAsync();

                //foreach (var currentRecord in records)
                //{
                //    appLogger.LogInformation(currentRecord.ToString());
                //}

                //// Get Record by Id
                record = await recordService.GetRecordByIdAsync(2164);
                if (record != null)
                {
                    if (record.ArtistAsset != null)
                    {
                        appLogger.LogInformation($"Artist: {record.ArtistAsset.Name}");
                        appLogger.LogInformation(record.ToString());
                    }
                    else
                    {
                        appLogger.LogInformation("No Record found for this RecordId.");
                    }
                }
            }
        }

        private static Dictionary<int, string> GetArtistDictionaryAsync(Dictionary<int, string> artistDictionary, IEnumerable<Artist> artists)
        {
            artistDictionary.Clear();

            artistDictionary.Add(0, "Select an artist");

            var orderedArtists = artists.OrderBy(a => a.LastName).ThenBy(a => a.FirstName);

            foreach (var artist in orderedArtists)
            {
                string displayName = string.IsNullOrEmpty(artist.FirstName)
                    ? artist.LastName
                    : $"{artist.LastName}, {artist.FirstName}";

                if (!artistDictionary.ContainsKey(artist.ArtistId))
                {
                    artistDictionary.Add(artist.ArtistId, displayName);
                }
            }

            return artistDictionary;
        }
    }
}
