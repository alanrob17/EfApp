using EfApp.Models;
using EfApp.Services;
using EfApp.Utilities;

namespace EfApp.Tests
{
    internal class ArtistRecordTest
    {
        private readonly ArtistService _artistService;
        private readonly RecordService _recordService;
        private readonly AppLogger _appLogger;

        public ArtistRecordTest(ArtistService artistService, RecordService recordService, AppLogger appLogger)
        {
            _artistService = artistService;
            _recordService = recordService;
            _appLogger = appLogger;
        }

        public async Task RunTestsAsync()
        {
            var artists = await _artistService.GetAllArtistsAsync();
            var records = await _recordService.GetAllRecordsAsync();

            //// Get a list of Artists and their Records in descening order
            var orderedArtists = artists
                    .OrderBy(a => a.LastName)
                    .ThenBy(a => a.FirstName)
                    .ToList();

            foreach (var currentArtist in orderedArtists)
            {
                _appLogger.LogInformation($"\nArtist: {currentArtist.Name}");

                var recordList = records
                    .Where(r => r.ArtistId == currentArtist.ArtistId)
                    .OrderByDescending(r => r.Recorded)
                    .ToList();

                foreach (var currentRecord in recordList)
                {
                    _appLogger.LogInformation($"{currentRecord.Recorded} - {currentRecord.Name}");
                }
            }
        }
    }
}