using EfApp.Models;
using EfApp.Services;
using EfApp.Utilities;
using Microsoft.EntityFrameworkCore;

namespace EfApp.Tests
{
    public class ArtistRecordTest
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
            await GetArtistsAndRecordsAsync();
            await GetArtistAndRecordsByIdAsync();
            await GetArtistNumberOfRecordsByIdAsync();
            await GetRecordByIdAsync();
            await GetRecordHtmlByIdAsync();
            await GetArtistNameFromRecordIdAsync();
            await GetTotalArtistCostAndDiscsAsync();
            await GetEachArtistTotalDiscsAsync();
        }

        private async Task GetArtistsAndRecordsAsync()
        {
            var artists = await _artistService.GetAllArtistsAsync();
            var records = await _recordService.GetAllRecordsAsync();

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

        private async Task GetArtistAndRecordsByIdAsync()
        {
            var artistId = 114;
            var artist = await _artistService.GetArtistByIdAsync(artistId);
            var artistRecords = await _recordService.GetArtistRecordsAsync(artistId);
            _appLogger.LogInformation($"Artist: {artist.Name}");

            foreach (var currentRecord in artistRecords)
            {
                _appLogger.LogInformation(currentRecord.ToString());
            }
        }

        private async Task GetArtistNumberOfRecordsByIdAsync()
        {
            var artistId = 114;
            var artist = await _artistService.GetArtistByIdAsync(artistId);
            int total = await _recordService.GetArtistNumberOfRecordsAsync(artistId);
            _appLogger.LogInformation($"Total number of Records for Artist: {artist.Name}: {total} discs.");
        }

        private async Task GetRecordByIdAsync()
        {
            var recordId = 2196;
            var record = await _recordService.GetRecordByIdAsync(recordId);
            if (record != null)
            {
                var artist = await _artistService.GetArtistByIdAsync(record.ArtistId);
                if (artist != null)
                {
                    _appLogger.LogInformation($"Artist: {artist.Name}");
                    _appLogger.LogInformation(record.ToString());
                }
            }
            else
            {
                _appLogger.LogInformation("No Record found for this RecordId.");
            }
        }

        private async Task GetRecordHtmlByIdAsync()
        {
            var recordId = 2196;
            var record = await _recordService.GetRecordByIdAsync(recordId);
            if (record != null)
            {
                var artist = await _artistService.GetArtistByIdAsync(record.ArtistId);
                if (artist != null)
                {
                    _appLogger.LogInformation($"<p><strong>ArtistId:</strong> {artist.ArtistId}</p>\n<p><strong>Artist:</strong> {artist.Name}</p>\n<p><strong>RecordId:</strong> {record.RecordId}</p>\n<p><strong>Recorded:</strong> {record.Recorded}</p>\n<p><strong>Name:</strong> {record.Name}</p>\n<p><strong>Rating:</strong> {record.Rating}</p>\n<p><strong>Media:</strong> {record.Media}</p>\n");
                }
            }
            else
            {
                _appLogger.LogInformation("No Record found for this RecordId.");
            }
        }

        private async Task GetArtistNameFromRecordIdAsync()
        {
            var recordId = 2196;
            var record = await _recordService.GetRecordByIdAsync(recordId);
            var artist = await _artistService.GetArtistByIdAsync(record.ArtistId);
            _appLogger.LogInformation($"Artist: {artist.Name}");
        }

        private async Task GetTotalArtistCostAndDiscsAsync()
        {
            var artists = await _artistService.GetAllArtistsAsync();
            var records = await _recordService.GetAllRecordsAsync();

            var result = from a in artists
                         join r in records
                         on a.ArtistId equals r.ArtistId
                         group r by new { a.ArtistId, a.FirstName, a.LastName } into g
                         select new
                         {
                             ArtistId = g.Key.ArtistId,
                             Name = (g.Key.FirstName + " " + g.Key.LastName).Trim(),
                             TotalDiscs = g.Sum(r => r.Discs),
                             TotalCost = g.Sum(r => r.Cost)
                         } into final
                         orderby final.TotalCost descending
                         select final;

            foreach (var currentArtist in result)
            {
                _appLogger.LogInformation($"{currentArtist.Name} - {currentArtist.TotalDiscs} discs - ${currentArtist.TotalCost:F2}");
            }
        }

        private async Task GetEachArtistTotalDiscsAsync()
        {
            var artists = await _artistService.GetAllArtistsAsync();
            var records = await _recordService.GetAllRecordsAsync();

            var results = from a in artists
                          join r in records on a.ArtistId equals r.ArtistId
                          group r by new { a.ArtistId, a.FirstName, a.LastName, a.Name } into g
                          orderby g.Key.LastName, g.Key.FirstName
                          select new
                          {
                              g.Key.ArtistId,
                              g.Key.FirstName,
                              g.Key.LastName,
                              g.Key.Name,
                              Discs = g.Sum(r => r.Discs)
                          };

            foreach (var currentArtist in results)
            {
                _appLogger.LogInformation($"Id: {currentArtist.ArtistId} - {currentArtist.Name} - {currentArtist.Discs} discs");
            }
        }
    }
}