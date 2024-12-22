using EfApp.Models;
using EfApp.Services;
using EfApp.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Tests
{
    public class RecordTest
    {
        private readonly RecordService _recordService;
        private readonly AppLogger _appLogger;

        public RecordTest(RecordService recordService, AppLogger appLogger)
        {
            _recordService = recordService;
            _appLogger = appLogger;
        }

        public async Task RunTestsAsync()
        {
            await GetAllRecordsAsync();
            await AddNewRecordAsync();
            await GetRecordDropdownListForArtistAsync();
            await UpdateRecordByIdAsync();
            await DeleteRecordByIdAsync();
            await GetRecordByNameAsync();
            await GetRecordsByYearAsync();
            await GetTotalNumberOfCDsAsync();
            await GetTotalNumberOfDiscsAsync();
            await GetTotalNumberOfRecordsAsync();
            await GetTotalNumberOfBluraysAsync();
            await GetTotalNumberOfDVDsAsync();
            await GetTotalDiscsByYearAsync();
            await GetTotalDiscsByYearBoughtAsync();
            await GetNoRecordReviewCountAsync();
            await GetAllNoReviewRecordsAsync();
            await GetTotalCostByYearBoughtAsync();
        }

        private async Task GetAllRecordsAsync()
        {
            var records = await _recordService.GetAllRecordsAsync();
            foreach (var currentRecord in records)
            {
                _appLogger.LogInformation(currentRecord.ToString());
            }
        }

        private async Task AddNewRecordAsync()
        {
            _appLogger.LogInformation("Creating a new record.");
            var artistId = 864;
            string dateString = "2022-01-16";
            DateTime boughtDate = DateTime.Parse(dateString);

            Record newRecord = new()
            {
                ArtistId = artistId,
                Name = "Rockin' the Bass",
                Field = "Rock",
                Recorded = 2019,
                Label = "Wibble",
                Pressing = "Aus",
                Rating = "****",
                Discs = 1,
                Media = "CD",
                Bought = boughtDate,
                Cost = 29.95m,
                Review = "This is James's first album."
            };

            await _recordService.AddRecordAsync(newRecord);
        }

        private async Task GetRecordDropdownListForArtistAsync()
        {
            var recordDictionary = new Dictionary<int, string>();
            var artistId = 114;
            var records = await _recordService.GetArtistRecordsAsync(artistId);
            recordDictionary = GetRecordDictionary(recordDictionary, records);

            if (recordDictionary != null)
            {
                foreach (var item in recordDictionary)
                {
                    _appLogger.LogInformation($"{item.Key} - {item.Value}");
                }
            }
        }

        private async Task UpdateRecordByIdAsync()
        {
            var recordId = 5286;
            var updateRecord = await _recordService.GetRecordByIdAsync(recordId);
            if (updateRecord != null)
            {
                _appLogger.LogInformation(updateRecord.ToString());

                string dateString = "2022-01-21";
                DateTime boughtDate = DateTime.Parse(dateString);

                updateRecord.Name = "A Lot Of Fun Aloud";
                updateRecord.Field = "Jazz";
                updateRecord.Recorded = 2020;
                updateRecord.Label = "Woppo";
                updateRecord.Pressing = "Jap";
                updateRecord.Rating = "***";
                updateRecord.Discs = 1;
                updateRecord.Media = "CD";
                updateRecord.Bought = boughtDate;
                updateRecord.Cost = 23.99m;
                updateRecord.CoverName = null;
                updateRecord.Review = "This is James' second album";

                await _recordService.UpdateRecordAsync(updateRecord);
                _appLogger.LogInformation("Record updated.");
            }
        }

        private async Task DeleteRecordByIdAsync()
        {
            var recordId = 5285;
            var record = await _recordService.GetRecordByIdAsync(recordId);

            if (record != null)
            {
                await _recordService.DeleteRecordAsync(record);
                _appLogger.LogInformation("Record deleted.");
            }
        }

        private async Task GetRecordByNameAsync()
        {
            var records = await _recordService.GetRecordByNameAsync("blonde on Blonde");
            foreach (var currentRecord in records)
            {
                _appLogger.LogInformation(currentRecord.ToString());
            }
        }

        private async Task GetRecordsByYearAsync()
        {
            var year = 1974;
            var records = await _recordService.GetRecordsByYearAsync(year);
            foreach (var currentRecord in records)
            {
                _appLogger.LogInformation(currentRecord.ToString());
            }
        }

        private async Task GetTotalNumberOfCDsAsync()
        {
            int total = await _recordService.GetTotalNumberOfCDsAsync();
            _appLogger.LogInformation($"Total number of CD's: {total}.");
        }

        private async Task GetTotalNumberOfDiscsAsync()
        {
            int total = await _recordService.GetTotalNumberOfDiscsAsync();
            _appLogger.LogInformation($"Total number of Disc's: {total}.");
        }

        private async Task GetTotalNumberOfRecordsAsync()
        {
            int total = await _recordService.GetTotalNumberOfRecordsAsync();
            _appLogger.LogInformation($"Total number of Records: {total}.");
        }

        private async Task GetTotalNumberOfBluraysAsync()
        {
            int total = await _recordService.GetTotalNumberOfBluraysAsync();
            _appLogger.LogInformation($"Total number of Blu-rays: {total}.");
        }

        private async Task GetTotalNumberOfDVDsAsync()
        {
            int total = await _recordService.GetTotalNumberOfDVDsAsync();
            _appLogger.LogInformation($"Total number of DVD's: {total}.");
        }

        private async Task GetTotalDiscsByYearAsync()
        {
            var year = 1974;
            int total = await _recordService.GetTotalDiscsByYearAsync(year);
            _appLogger.LogInformation($"Total number of discs for {year}: {total}.");
        }

        private async Task GetTotalDiscsByYearBoughtAsync()
        {
            var year = 2015;
            int total = await _recordService.GetTotalDiscsByYearBoughtAsync(year);
            _appLogger.LogInformation($"Total number of discs bought in {year}: {total} discs.");
        }

        private async Task GetNoRecordReviewCountAsync()
        {
            int total = await _recordService.GetNoRecordReviewCountAsync();
            _appLogger.LogInformation($"Total number of Records with no Review: {total}.");
        }

        private async Task GetAllNoReviewRecordsAsync()
        {
            var records = await _recordService.GetAllNoReviewRecordsAsync();
            _appLogger.LogInformation("Records with no Review:");
            foreach (var currentRecord in records)
            {
                _appLogger.LogInformation(currentRecord.ToString());
            }
        }

        private async Task GetTotalCostByYearBoughtAsync()
        {
            var year = 1974;
            decimal totalCost = await _recordService.GetTotalCostByYearBoughtAsync(year);
            _appLogger.LogInformation($"Total cost for {year}: {totalCost:C}.");
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
