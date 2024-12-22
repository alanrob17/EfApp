using EfApp.Models;
using EfApp.Services;
using EfApp.Utilities;
using Microsoft.EntityFrameworkCore;

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

            //// Get an Artist and their Records by ArtistId
            var artistId = 114;
            var artist = await _artistService.GetArtistByIdAsync(artistId);
            var artistRecords = await _recordService.GetArtistRecordsAsync(artistId);
            _appLogger.LogInformation($"Artist: {artist.Name}");

            foreach (var currentRecord in artistRecords)
            {
                _appLogger.LogInformation(currentRecord.ToString());
            }

            //// Get Artist's Number of Records by artistId
            artistId = 114;
            artist = await _artistService.GetArtistByIdAsync(artistId);
            int total = await _recordService.GetArtistNumberOfRecordsAsync(artistId);
            _appLogger.LogInformation($"Total number of Records for Artist: {artist.Name}: {total} discs.");

            //// Get Record by recordId 
            var recordId = 2196;
            var record = await _recordService.GetRecordByIdAsync(recordId);
            if (record != null)
            {
                artist = await _artistService.GetArtistByIdAsync(record.ArtistId);
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

            //// Get the Html for a record found by recordId
            artistId = 2196;
            record = await _recordService.GetRecordByIdAsync(recordId);
            if (record != null)
            {
                artist = await _artistService.GetArtistByIdAsync(record.ArtistId);
                if (artist != null)
                {
                    _appLogger.LogInformation($"<p><strong>ArtistId:</strong> {artist.ArtistId}</p>\n<p><strong>Artist:</strong> {artist.Name}</p>\n<p><strong>RecordId:</strong> {record.RecordId}</p>\n<p><strong>Recorded:</strong> {record.Recorded}</p>\n<p><strong>Name:</strong> {record.Name}</p>\n<p><strong>Rating:</strong> {record.Rating}</p>\n<p><strong>Media:</strong> {record.Media}</p>\n");
                }
            }
            else
            {
                _appLogger.LogInformation("No Record found for this RecordId.");
            }

            //// Get an Artist Name From a recordId
            artistId = 2196;
            record = await _recordService.GetRecordByIdAsync(recordId);
            artist = await _artistService.GetArtistByIdAsync(record.ArtistId);
            _appLogger.LogInformation($"Artist: {artist.Name}");

            //// Get the Total Artist Cost and number of Discs for each artist
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

            //// Get each artist's total number of discs
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