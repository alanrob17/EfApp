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
            //var orderedArtists = artists
            //        .OrderBy(a => a.LastName)
            //        .ThenBy(a => a.FirstName)
            //        .ToList();

            //foreach (var currentArtist in orderedArtists)
            //{
            //    _appLogger.LogInformation($"\nArtist: {currentArtist.Name}");

            //    var recordList = records
            //        .Where(r => r.ArtistId == currentArtist.ArtistId)
            //        .OrderByDescending(r => r.Recorded)
            //        .ToList();

            //    foreach (var currentRecord in recordList)
            //    {
            //        _appLogger.LogInformation($"{currentRecord.Recorded} - {currentRecord.Name}");
            //    }
            //}

            //// Get an Artist and their Records by ArtistId
            //var artistId = 114;
            //var artist = await _artistService.GetArtistByIdAsync(artistId);
            //var artistRecords = await _recordService.GetArtistRecordsAsync(artistId);
            //_appLogger.LogInformation($"Artist: {artist.Name}");

            //foreach (var currentRecord in artistRecords)
            //{
            //    _appLogger.LogInformation(currentRecord.ToString());
            //}

            // Get Artist's Number of Records by artistId
            //var artistId = 114;
            //var artist = await _artistService.GetArtistByIdAsync(artistId);
            //int total = await _recordService.GetArtistNumberOfRecordsAsync(artistId);
            //_appLogger.LogInformation($"Total number of Records for Artist: {artist.Name}: {total} discs.");


            //// Get Record by recordId 
            //var recordId = 2196;
            //var record = await _recordService.GetRecordByIdAsync(recordId);
            //if (record != null)
            //{
            //    var artist = await _artistService.GetArtistByIdAsync(record.ArtistId);
            //    if (artist != null)
            //    {
            //        _appLogger.LogInformation($"Artist: {artist.Name}");
            //        _appLogger.LogInformation(record.ToString());
            //    }
            //}
            //else
            //{
            //    _appLogger.LogInformation("No Record found for this RecordId.");
            //}

            //// Get an Artist Name From a recordId
            //var recordId = 2196;
            //var record = await _recordService.GetRecordByIdAsync(recordId);
            //var artist = await _artistService.GetArtistByIdAsync(record.ArtistId);
            //_appLogger.LogInformation($"Artist: {artist.Name}");
        }
    }
}