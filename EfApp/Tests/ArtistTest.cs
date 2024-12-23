using EfApp.Models;
using EfApp.Services;
using EfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Tests
{
    public class ArtistTest
    {
        private readonly ArtistService _artistService;
        private readonly AppLogger _appLogger;

        public ArtistTest(ArtistService artistService, AppLogger appLogger)
        {
            _artistService = artistService;
            _appLogger = appLogger;
        }

        public async Task RunTestsAsync()
        {
            await GetAllArtistsAsync();
            await CreateNewArtistAsync();
            await GetArtistByIdAsync();
            await GetArtistByNameAsync();
            await DeleteArtistAsync();
            await GetArtistDropdownListAsync();
            await GetArtistIdAsync();
            await GetBiographyByIdAsync();
            await GetArtistsWithNoBioAsync();
            await GetNoBiographyTotalAsync();
            await WriteArtistHtmlAsync();
        }

        private async Task GetAllArtistsAsync()
        {
            var artists = await _artistService.GetAllArtistsAsync();
            foreach (var currentArtist in artists)
            {
                _appLogger.LogInformation(currentArtist.ToString());
            }
        }

        private async Task CreateNewArtistAsync()
        {
            _appLogger.LogInformation("Creating a new artist.");
            var newArtist = new Artist
            {
                FirstName = "James",
                LastName = "Robson",
                Name = "James Robson",
                Biography = "James is a bass player extroadinaire..."
            };
            await _artistService.AddArtistAsync(newArtist);
        }

        private async Task GetArtistByIdAsync()
        {
            var artist = await _artistService.GetArtistByIdAsync(114);
            if (artist != null)
            {
                _appLogger.LogInformation(artist.ToString());

                if (artist.Records != null && artist.Records.Any())
                {
                    artist.Records = artist.Records.OrderByDescending(r => r.Recorded).ToList();
                    foreach (var currentRecord in artist.Records)
                    {
                        _appLogger.LogInformation($"Record: {currentRecord.ToString()}");
                    }
                }
                else
                {
                    _appLogger.LogInformation("No records found for this artist.");
                }
            }
        }

        private async Task GetArtistByNameAsync()
        {
            var artist = await _artistService.GetArtistByNameAsync("Alan Robson");
            if (artist != null)
            {
                _appLogger.LogInformation(artist.ToString());

                var updateArtist = artist;
                updateArtist.FirstName = "Alonzo";
                updateArtist.LastName = "Robosono";
                updateArtist.Name = "Alonzo Robosono";
                updateArtist.Biography = "Alonzo is an Italian Country & Western singer.";
                await _artistService.UpdateArtistAsync(updateArtist);
                _appLogger.LogInformation("Artist updated.");
            }
        }

        private async Task DeleteArtistAsync()
        {
            var artist = await _artistService.GetArtistByNameAsync("Alonzo Robosono");
            if (artist != null)
            {
                await _artistService.DeleteArtistAsync(artist);
                _appLogger.LogInformation("Artist deleted.");
            }
        }

        private async Task GetArtistDropdownListAsync()
        {
            var artists = await _artistService.GetAllArtistsAsync();
            var artistDictionary = GetArtistDictionary(new Dictionary<int, string>(), artists);

            foreach (var item in artistDictionary)
            {
                _appLogger.LogInformation($"{item.Key} - {item.Value}");
            }
        }

        private async Task GetArtistIdAsync()
        {
            var artistId = await _artistService.GetArtistIdAsync("Bob", "Dylan");
            _appLogger.LogInformation(artistId.ToString());
        }

        private async Task GetBiographyByIdAsync()
        {
            var biography = await _artistService.GetBiographyByIdAsync(114);
            _appLogger.LogInformation($"Biography:\n{biography}");
        }

        private async Task GetArtistsWithNoBioAsync()
        {
            var names = await _artistService.GetArtistsWithNoBioAsync();
            foreach (var name in names)
            {
                _appLogger.LogInformation(name);
            }
        }

        private async Task GetNoBiographyTotalAsync()
        {
            var total = await _artistService.GetNoBiographyTotal();
            _appLogger.LogInformation(total.ToString());
        }

        private async Task WriteArtistHtmlAsync()
        {
            var artistId = 114;
            var artist = await _artistService.GetArtistByIdAsync(artistId);
            var message = artist?.ArtistId > 0
                ? $"<p><strong>Id:</strong> {artist.ArtistId}</p>\n<p><strong>Name:</strong> {artist.FirstName} {artist.LastName}</p>\n<p><strong>Biography:</strong></p>\n<div>{artist.Biography}</p></div>"
                : "ERROR: Artist not found!";
            _appLogger.LogInformation(message);
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
    }
}
