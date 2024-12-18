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
                appLogger.LogInformation("Creating a new artist.");
                var newArtist = new Artist { FirstName = "James", LastName = "Robson", Name = "James Robson", Biography = "James is a Country & Western star..." };
                await artistService.AddArtistAsync(newArtist);

                //// Get all Artists
                var artists = await artistService.GetAllArtistsAsync();
                foreach (var currentArtist in artists)
                {
                    appLogger.LogInformation(currentArtist.ToString());
                }

                //// Get Artist by Id
                artist = await artistService.GetArtistByIdAsync(114);
                if (artist != null)
                {
                    appLogger.LogInformation(artist.ToString());

                    if (artist.Records != null && artist.Records.Any())
                    {
                        artist.Records = artist.Records.OrderByDescending(r => r.Recorded).ToList();
                        foreach (var currentRecord in artist.Records)
                        {
                            appLogger.LogInformation($"Record: {currentRecord.ToString()}");
                        }
                    }
                    else
                    {
                        appLogger.LogInformation("No records found for this artist.");
                    }
                }

                //// Get Artist by name
                artist = await artistService.GetArtistByNameAsync("Alonzo Robosono");
                if (artist != null)
                {
                    appLogger.LogInformation(artist.ToString());

                    var updateArtist = artist;
                    updateArtist.FirstName = "Alan";
                    updateArtist.LastName = "Robson";
                    updateArtist.Name = "Alan Robson";
                    updateArtist.Biography = "Alan is a Country & Western singer.";
                    await artistService.UpdateArtistAsync(updateArtist);
                    appLogger.LogInformation("Artist updated.");
                }

                //// Delete an Artist
                artist = await artistService.GetArtistByNameAsync("Alan Robson");
                if (artist != null)
                {
                    await artistService.DeleteArtistAsync(artist);
                    appLogger.LogInformation("Artist deleted.");
                }

                //// Get Artist dropdown list
                var artistDictionary = new Dictionary<int, string>();

                artistDictionary = GetArtistDictionary(artistDictionary, artists);

                foreach (var item in artistDictionary)
                {
                    appLogger.LogInformation($"{item.Key} - {item.Value}");
                }

                //// Get Artist by Id
                var artistId = await artistService.GetArtistIdAsync("Bob", "Dylan");
                appLogger.LogInformation(artistId.ToString());

                //// Get Biography by artistId
                var biography = await artistService.GetBiographyByIdAsync(114);
                appLogger.LogInformation($"Biography:\n{biography}");

                //// Get a list of artists with no biography
                IEnumerable<string> names = await artistService.GetArtistsWithNoBioAsync();
                foreach (var name in names)
                {
                    appLogger.LogInformation(name);
                }

                //// Get the number of Artists with no biography
                var total = await artistService.GetNoBiographyTotal();
                appLogger.LogInformation(total.ToString());

                //*--- RECORDS ---*
                Record record = new();

                //// Get All Records
                var records = await recordService.GetAllRecordsAsync();

                foreach (var currentRecord in records)
                {
                    appLogger.LogInformation(currentRecord.ToString());
                }

                //// Add new Record
                appLogger.LogInformation("Creating a new record.");
                artistId = 861;
                string dateString = "2022-01-16";
                DateTime boughtDate = DateTime.Parse(dateString);
                
                Record newRecord = new()
                {
                    ArtistId = artistId,
                    Name = "No More Fun Allowed",
                    Field = "Rock",
                    Recorded = 1986,
                    Label = "Wobble",
                    Pressing = "Aus",
                    Rating = "***",
                    Discs = 1,
                    Media = "CD",
                    Bought = boughtDate,
                    Cost = 19.95m,
                    Review = "This is Alan's first album."
                };

                await recordService.AddRecordAsync(newRecord);


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

                //// Get an Artist and their Records by ArtistId
                var artistRecords = await recordService.GetArtistRecordsAsync(114);
                foreach (var currentRecord in artistRecords)
                {
                    appLogger.LogInformation(currentRecord.ToString());
                }

                //// Get the Record dropdown list for an Artist
                var recordDictionary = new Dictionary<int, string>();
                artistId = 114;
                records = await recordService.GetArtistRecordsAsync(artistId);
                recordDictionary = GetRecordDictionary(recordDictionary, records);

                record = records.First();
                appLogger.LogInformation(record.ArtistAsset.ToString());

                if (recordDictionary != null)
                {
                    foreach (var item in recordDictionary)
                    {
                        appLogger.LogInformation($"{item.Key} - {item.Value}");
                    }
                }

                //// Get a list of Artists and their Records
                var orderedArtists = artists
                        .OrderBy(a => a.LastName)
                        .ThenBy(a => a.FirstName)
                        .ToList();

                foreach (var currentArtist in orderedArtists)
                {
                    appLogger.LogInformation($"\nArtist: {currentArtist.Name}");

                    var recordList = records
                        .Where(r => r.ArtistId == currentArtist.ArtistId)
                        .OrderByDescending(r => r.Recorded)
                        .ToList(); // Materialize the query to avoid multiple enumerations

                    foreach (var currentRecord in recordList)
                    {
                        appLogger.LogInformation($"{currentRecord.Recorded} - {currentRecord.Name}");
                    }
                }
            }
        }

        private static Dictionary<int, string> GetArtistDictionary(Dictionary<int, string> artistDictionary, IEnumerable<Artist> artists)
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

        private static Dictionary<int, string>? GetRecordDictionary(Dictionary<int, string> recordDictionary, IEnumerable<Record> records)
        {
            recordDictionary.Clear();

            recordDictionary.Add(0, "Select a record");

            var orderedRecords = records.OrderByDescending(r => r.Recorded);

            foreach (var record in orderedRecords)
            {
                if (!recordDictionary.ContainsKey(record.RecordId))
                {
                    recordDictionary.Add(record.RecordId, record.Name);
                }
            }

            return recordDictionary;
        }
    }
}
